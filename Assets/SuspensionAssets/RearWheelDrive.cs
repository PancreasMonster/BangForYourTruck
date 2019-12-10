using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour {

	private WheelCollider[] wheels;

	public float maxAngle = 25;
	public float maxTorque = 300;
	public GameObject wheelShape;
    public float sumTorque, forwardTorque, backwardTorque, breakForce;
    public float maxSpeed = 800; // max speed of the vehicle, warning: raising this too high will cause problems
    bool accelerating, decelerating;   
    Rigidbody rigidbody;
    WheelHit hit, hit2, hit3, hit4;
    public AudioSource aud;
  

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
	{

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
	}

	// this is a really simple approach to updating wheels
	// here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
	// this helps us to figure our which wheels are front ones and which are rear
	public void FixedUpdate()
	{
        Debug.DrawRay(transform.position, rigidbody.velocity * 100, Color.blue);

		float angle = maxAngle * Input.GetAxisRaw("Horizontal" + GetComponent<Health>().playerNum.ToString());
        float driftAmount = Mathf.Abs(Input.GetAxisRaw("Horizontal" + GetComponent<Health>().playerNum.ToString()));
        forwardTorque = maxTorque * Input.GetAxisRaw("RightTrigger" + GetComponent<Health>().playerNum.ToString());
        backwardTorque = maxTorque * -Input.GetAxisRaw("LeftTrigger" + GetComponent<Health>().playerNum.ToString());
        sumTorque = forwardTorque + backwardTorque;
      

        foreach (WheelCollider wheel in wheels)
		{
			// a simple car where front wheels steer while rear ones drive
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

            if (wheel.transform.localPosition.z < 0) {
                wheel.motorTorque = sumTorque;
                WheelFrictionCurve curve = new WheelFrictionCurve();
                curve.extremumSlip = 1.125f;
                curve.extremumValue = .83333333f;
                curve.asymptoteSlip = .5f;
                curve.asymptoteValue = .75f;
                curve.stiffness = .55f - (.2f * driftAmount);
                wheel.sidewaysFriction = curve;
                if (wheel.rpm > maxSpeed)
                    wheel.motorTorque = 0;
                WheelHit wh;
                if (wheel.GetGroundHit(out wh))
                {
                    aud.pitch = wheel.rpm / maxSpeed;
                } else
                {
                    aud.pitch = (wheel.rpm / 2) / maxSpeed;
                }
                
                    }

           //  wheel.

            if (Input.GetAxis("RightTrigger" + GetComponent<Health>().playerNum.ToString()) == 0 && Input.GetAxis("LeftTrigger" + GetComponent<Health>().playerNum.ToString()) == 0)
            {
                wheel.brakeTorque = breakForce;
                aud.Stop();
            } else
            {
                if(!aud.isPlaying)
                aud.Play();
                wheel.brakeTorque = 0;
            }

            if(Input.GetButton("PadB" + GetComponent<Health>().playerNum.ToString())) //handbrake, trying to get it to work
            {
              //  if(wheel.motorTorque > 0)
                //    wheel.motorTorque =  -wheel.motorTorque;
                //if (wheel.motorTorque < 0)
                 //   wheel.motorTorque = 0;
            }
            
            if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// assume that the only child of the wheelcollider is the wheel shape
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}

        
    }
}
