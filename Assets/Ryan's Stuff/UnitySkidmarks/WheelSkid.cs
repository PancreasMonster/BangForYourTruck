using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;


// Example skid script. Put this on a WheelCollider.
// Copyright 2017 Nition, BSD licence (see LICENCE file). http://nition.co
[RequireComponent(typeof(WheelCollider))]
public class WheelSkid : MonoBehaviour {

	// INSPECTOR SETTINGS

	[SerializeField]
	Rigidbody rb;
	[SerializeField]
	Skidmarks skidmarksController;

	// END INSPECTOR SETTINGS

	WheelCollider wheelCollider;
	WheelHit wheelHitInfo;

	const float SKID_FX_SPEED = 0.5f; // Min side slip speed in m/s to start showing a skid
	const float MAX_SKID_INTENSITY = 20.0f; // m/s where skid opacity is at full intensity
	const float WHEEL_SLIP_MULTIPLIER = 10.0f; // For wheelspin. Adjust how much skids show
	int lastSkid = -1; // Array index for the skidmarks controller. Index of last skidmark piece this wheel used
	float lastFixedUpdateTime;
    public bool sound;
    public AudioSource aud;
    public float timer;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    float driftTimer;
    public PostProcessVolume PPV;
    ChromaticAberration ChromAberr = null;
    public float intensity;
    public int playerNum;

    public bool firstWheel;
    public MobilityCharges mobCharges;
    public float driftMultiplierOffset;

    float XButton;
    PlayerPause pp;

    // #### UNITY INTERNAL METHODS ####

    private void OnFaceButtonWest(InputValue value)
    {
        if (!pp.noPlayerInput)
        {
            XButton = 1;
        }
        else
        {
            XButton = 0;
        }
    }

    private void OnFaceButtonWestRelease(InputValue value)
    {

        XButton = 0;

    }

    protected void Awake() {
		wheelCollider = GetComponent<WheelCollider>();
        if(sound)
        PPV.profile.TryGetSettings(out ChromAberr);
        lastFixedUpdateTime = Time.time;
        pp = GetComponent<PlayerPause>();
	}

	protected void FixedUpdate() {
		lastFixedUpdateTime = Time.time;
	}

	protected void LateUpdate()
    {
		if (wheelCollider.GetGroundHit(out wheelHitInfo))
		{
			// Check sideways speed

			// Gives velocity with +z being the car's forward axis
			Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
			float skidTotal = Mathf.Abs(localVelocity.x);

			// Check wheel spin as well

			float wheelAngularVelocity = wheelCollider.radius * ((2 * Mathf.PI * wheelCollider.rpm) / 60);
			float carForwardVel = Vector3.Dot(rb.velocity, transform.forward);
			float wheelSpin = Mathf.Abs(carForwardVel - wheelAngularVelocity) * WHEEL_SLIP_MULTIPLIER;

			// NOTE: This extra line should not be needed and you can take it out if you have decent wheel physics
			// The built-in Unity demo car is actually skidding its wheels the ENTIRE time you're accelerating,
			// so this fades out the wheelspin-based skid as speed increases to make it look almost OK
			wheelSpin = Mathf.Max(0, wheelSpin * (10 - Mathf.Abs(carForwardVel)));

			skidTotal += wheelSpin;

            if (firstWheel)
            {
                if (skidTotal / driftMultiplierOffset > 1)
                {
                    mobCharges.currentDriftMultiplier = 1;
                }

                mobCharges.currentDriftMultiplier = skidTotal / driftMultiplierOffset;
            }


            // Skid if we should
            if (skidTotal >= SKID_FX_SPEED) {
				intensity = Mathf.Clamp01(skidTotal / MAX_SKID_INTENSITY);

                

                if (sound)
                {
                    if (intensity > .4f && rb.velocity.sqrMagnitude > 200)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = 0;
                    }

                    if (timer > 0.1f && intensity > .4f && rb.velocity.sqrMagnitude > 200 && !aud.isPlaying)
                    {
                        //aud.Play();
                        foreach(ParticleSystem ps in particleSystems)
                        {
                            if (XButton == 1)
                            {
                                if (!ps.isPlaying)
                                    ps.Play();
                            }
                            else
                                ps.Stop();
                        }
                     //   aud.volume = Mathf.Lerp(aud.volume, 0, 1.5f * Time.deltaTime);
                        driftTimer += Time.deltaTime;
                    }

                    if (intensity < .4f || rb.velocity.sqrMagnitude < 200 && aud.isPlaying)
                    {
                        foreach (ParticleSystem ps in particleSystems)
                        {
                            ps.Stop();
                        }
                       // aud.volume = Mathf.Lerp(aud.volume, 0, 1.5f * Time.deltaTime);
                        StartCoroutine(audioStop());
                        driftTimer = 0;
                    }

                    
                    
                    DepthOfField dop = null;
                    PPV.profile.TryGetSettings(out dop);

                    /*  if (timer > .2f)
                     {
                         ChromAberr.intensity.value = Mathf.Clamp(timer - .2f, 0, .6f);
                         dop.focalLength.value = Mathf.Clamp((timer * 12) + 260, 260, 280);
                     } else if (ChromAberr.intensity.value > 0)
                     {
                         ChromAberr.intensity.value = Mathf.Lerp(ChromAberr.intensity.value, 0, 2 * Time.deltaTime);
                         dop.focalLength.value = Mathf.Lerp(dop.focalLength.value, 260, 2 * Time.deltaTime);
                     }
                    /* while (ChromAberr.intensity.value <= 1)
                     {
                         ChromAberr.intensity.value += (10f * Time.deltaTime);
                         dop.focalLength.value += (40f * Time.deltaTime);
                         yield return null;
                     }
                     yield return new WaitForSeconds(.4f);


                     while (ChromAberr.intensity.value >= 0)
                     {
                         ChromAberr.intensity.value -= (5 * Time.deltaTime);
                         dop.focalLength.value -= (40f * Time.deltaTime);
                         yield return null;
                     }
                     dop.focalLength.value = 260f; */





                }
                // Account for further movement since the last FixedUpdate
                Vector3 skidPoint = wheelHitInfo.point + (rb.velocity * (Time.time - lastFixedUpdateTime));
				lastSkid = skidmarksController.AddSkidMark(skidPoint, wheelHitInfo.normal, intensity, lastSkid);
			}
			else {
				lastSkid = -1;
			}
		}
		else {
			lastSkid = -1;
            if (sound)
            {
               // aud.Stop();
                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Stop();
                }
            }

            }
	}

    IEnumerator audioStop ()
    {
        yield return new WaitForSeconds(.33f);
      //  aud.Stop();
    }

	// #### PUBLIC METHODS ####

	// #### PROTECTED/PRIVATE METHODS ####


}
