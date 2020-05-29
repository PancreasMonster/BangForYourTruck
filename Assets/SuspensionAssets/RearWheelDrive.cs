using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class RearWheelDrive : MonoBehaviour {

	private WheelCollider[] wheels;

	public float maxAngle = 25;
	public float maxTorque = 300;
	public GameObject wheelShape;
    public float sumTorque, forwardTorque, backwardTorque, breakForce;
    public float forwardMomentum, backwardsMomentum, currBreakForce;
    float startingMaxSpeed;
    public float maxSpeed = 800; // max speed of the vehicle, warning: raising this too high will cause problems
    bool accelerating, decelerating;   
    Rigidbody rigidbody;
    WheelHit hit, hit2, hit3, hit4;
    public AudioSource aud;
    public float sidewaySlipEx, sidewaySlipAS;
    public bool trainingMode;
    public TrainingManager tm;
    PlayerPause pp;


    // here we find all the WheelColliders down in the hierarchy
    public void Start()
	{
        startingMaxSpeed = maxSpeed;
        rigidbody = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
                if (wheel.transform.localPosition.x > 0)
                    ws.transform.localScale = new Vector3(ws.transform.localScale.x * -1f, ws.transform.localScale.y, ws.transform.localScale.z);
			}
		}
        pp = GetComponent<PlayerPause>();
	}

    Vector2 leftStick;
    float rightTrigger;
    float leftTrigger;
    float XButton;

    private void OnLeftStick (InputValue value)
    {
        if (!pp.noPlayerInput)
        {
            leftStick = value.Get<Vector2>();
        }
        else
        {
            leftStick = Vector2.zero;
        }
    }

    private void OnRightTrigger (InputValue value)
    {
        if (!pp.noPlayerInput)
        {
            rightTrigger = value.Get<float>();
            if (trainingMode && tm.canProceed)
                tm.pressedRT = true;
        }
        else
        {
            rightTrigger = 0;
        }
    }

    private void OnLeftTrigger (InputValue value)
    {
        if (!pp.noPlayerInput)
        {
            leftTrigger = value.Get<float>();
            if (trainingMode && tm.canProceed)
                tm.pressedLT = true;
        }
        else
        {
            leftTrigger = 0;
        }
    }

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

    public void FixedUpdate()
	{
        Debug.DrawRay(transform.position, rigidbody.velocity * 100, Color.blue);

		float angle = maxAngle * leftStick.x;
        float driftAmount = Mathf.Abs(leftStick.x);
        forwardTorque = maxTorque * rightTrigger;
        backwardTorque = maxTorque * -leftTrigger;
        sumTorque = forwardTorque + backwardTorque;

        if (forwardTorque > 0 && backwardTorque == 0)
        {
            if (forwardMomentum < 2)
            {
                forwardMomentum += 3 * Time.deltaTime;
            }
        }
        else
        {
            if (forwardMomentum > 0)
            {
                forwardMomentum -= 6 * Time.deltaTime;
            }
        }

        if (backwardTorque < 0 && forwardTorque == 0)
        {
            if (backwardsMomentum < 2)
            {
                backwardsMomentum += 3 * Time.deltaTime;
            }
        }
        else
        {
            if (backwardsMomentum > 0)
            {
                backwardsMomentum -= 6 * Time.deltaTime;
            }
        }


        foreach (WheelCollider wheel in wheels)
		{
            if (wheel.transform.localPosition.z > 0) {

                if (XButton > 0 && wheel.rpm / maxSpeed > .3f) //this if statement reduces the steering angle when the vehicle approachs max speed and the drift button hasn't been used
                {
                    wheel.steerAngle = angle;
                }
                else
                {
                    wheel.steerAngle = angle - ((angle/2) * (wheel.rpm / maxSpeed));
                    
                }

            }

            WheelFrictionCurve curve = new WheelFrictionCurve();
            if (XButton > 0 && wheel.rpm / maxSpeed > .3f)
            {
               
            curve.extremumSlip = 30f;
            curve.extremumValue = 10;
            curve.asymptoteSlip = 30f;
            curve.asymptoteValue = 10;
            curve.stiffness = 1f;
                maxAngle = 30;
                sidewaySlipAS = 6;
                sidewaySlipEx = 6; 
            }
            else
            {
                if (sidewaySlipAS > 3)
                {
                    sidewaySlipAS -= 1 * Time.deltaTime;
                    sidewaySlipEx -= 1 * Time.deltaTime;
                }
                curve.extremumSlip = sidewaySlipAS;
                curve.extremumValue = 10;
                curve.asymptoteSlip = sidewaySlipEx;
                curve.asymptoteValue = 10;                
                curve.stiffness = 1f;
                maxAngle = 15;
            }
           


            if (wheel.transform.localPosition.z < 0) {

                if (GetComponent<FlipOver>().timer <= .15f)
                {
                    wheel.motorTorque = sumTorque;
                } else
                {
                    wheel.motorTorque = 0;
                    wheel.brakeTorque = breakForce;
                }
                if (XButton > 0 && wheel.rpm / maxSpeed > .3f)
                {

                    
                    curve.stiffness = .55f + (.4f * driftAmount);
                }
                else
                {
                   
                    curve.stiffness = 1.25f;
                }

                if (Mathf.Abs(wheel.rpm) > maxSpeed)
                    wheel.motorTorque = 0;
                WheelHit wh;
                if (wheel.GetGroundHit(out wh))
                {
                    float audPitch = wheel.rpm / maxSpeed;
                    audPitch = Mathf.Clamp(audPitch, -2f, 1.5f);
                    aud.pitch = audPitch;
                } else
                {
                    float audPitch = (wheel.rpm / 2) / maxSpeed;
                    audPitch = Mathf.Clamp(audPitch, -2f, 1.5f);
                    aud.pitch = audPitch;
                }
                
                    }

            wheel.sidewaysFriction = curve;

            //  wheel.

            if (rightTrigger == 0 && leftTrigger == 0)
            {
                wheel.brakeTorque = breakForce / 20;
               // aud.Stop();
                aud.volume = Mathf.Lerp(aud.volume, 0, .2f * Time.deltaTime);
            } else
            {
               // if (!aud.isPlaying)
              //      aud.Play();
                aud.volume = Mathf.Lerp(aud.volume, .65f, .4f * Time.deltaTime);
                if (backwardTorque < 0 && forwardTorque == 0 && forwardMomentum > 0)
                {
                    wheel.brakeTorque = breakForce;
                    currBreakForce = breakForce;
                }
                else if ((backwardTorque == 0 && forwardTorque > 0 && backwardsMomentum > 0))
                {
                    wheel.brakeTorque = breakForce;
                }
                else
                {
                    wheel.brakeTorque = 0;
                }
            }
            
            if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}

        
    }

    public void EMPDuration(float f)
    {
        Invoke("ReturnToMaxSpeed",f);
    }

    void ReturnToMaxSpeed()
    {
        maxSpeed = startingMaxSpeed;
    }
}
