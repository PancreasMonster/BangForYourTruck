using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeCamera : MonoBehaviour
{
    public float turnSpeed;
    public int playerNum;
    public Vector3 offset = new Vector3(0, 10, -26);
    public Camera mainCam;
    public float verticalSpeed;
    public Transform player;
    public Transform target;
    public Transform hoverBox;
    public float maxVerticalHeight = 5f, minVerticalHeight = -5f;
    public float mainCamFoVBaseValue, mainCamFoVScaleValue;
    public float baseTargetRange, scaleTargetRange;
    public float setDrag = 0;
    public float animSpeed;
    public float cooldownTimer;
    public List<Transform> wheelVisuals = new List<Transform>();
    public WheelCollider[] wheels;
    private float verticalHeight;
    private bool Cooldown;
    private float minYOffset, maxYOffset;
    private float minMaxOffset;
    private float origCamFOV = 40;
    public Vector3 origOffset = new Vector3(0, 10, -26);

    public GameObject driveModeWeapons;
    public GameObject buildModeDpad;
    bool buildMode;
    public Vector3 origOffset_;

    // Start is called before the first frame update
    void Start()
    {
        origOffset_ = offset;
        buildMode = false;
        verticalHeight = offset.y;
        minYOffset = verticalHeight + minVerticalHeight;
        maxYOffset = verticalHeight + maxVerticalHeight;
        minMaxOffset = Mathf.Abs(maxVerticalHeight) + Mathf.Abs(minVerticalHeight);
    }

    private void Update()
    {
        if (Input.GetButtonDown("PadY" + playerNum.ToString()) && !Cooldown)
        {
            SwapMode();
        }

        foreach (Transform t in wheelVisuals)
        {

            
            t.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0,0,90),
                animSpeed * Time.deltaTime);
        }

    }

    public void SwapMode()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.drag = setDrag;
        
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        changeFromThis();
        player.GetComponent<RearWheelDrive>().enabled = true;
        player.GetComponent<BuildModeProtoMovement>().enabled = false;
        player.GetComponent<BuildModeFire>().enabled = false;

        

        foreach (WheelCollider w in wheels)
        {
            w.gameObject.SetActive(true);
        }


        player.GetComponent<FireDisk>().enabled = true;
        player.GetComponent<LineRenderer>().enabled = false;
        hoverBox.GetComponent<BoxCollider>().enabled = false;
        GetComponent<BuildModeCamera>().enabled = false;
        GetComponent<Orbit>().enabled = true;
    }

    public void changeToThis(WheelCollider[] w)
    {
        offset = origOffset_;
        offset = Quaternion.AngleAxis(player.transform.rotation.eulerAngles.y, Vector3.up) * offset;
        wheels = w;      
        foreach (Transform t in wheelVisuals)
        {
            t.gameObject.SetActive(true);
            
        }
        driveModeWeapons.SetActive(false);
        buildModeDpad.SetActive(true);
    }

    private void changeFromThis()
    {
        

        foreach (Transform t in wheelVisuals)
        {
            t.gameObject.SetActive(false);           
        }

        offset = origOffset;
        mainCam.fieldOfView = origCamFOV;
        driveModeWeapons.SetActive(true);
        buildModeDpad.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset = new Vector3(offset.x, verticalHeight, offset.z);
        transform.position = player.position + offset;
        offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed * Time.deltaTime, Vector3.up) * offset;       
        transform.LookAt(new Vector3(target.position.x, target.position.y - 30, target.position.z));
        mainCam.fieldOfView = mainCamFoVBaseValue + (mainCamFoVScaleValue * (1.0f - ((verticalHeight-minYOffset)/minMaxOffset)));
        verticalHeight = Mathf.Clamp(verticalHeight, minYOffset, maxYOffset);
        if(verticalHeight >= minYOffset && verticalHeight <= maxYOffset)
        verticalHeight += -Input.GetAxisRaw("RVertical" + playerNum.ToString()) * verticalSpeed * Time.deltaTime;
        target.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, baseTargetRange - (scaleTargetRange * (1.0f - ((verticalHeight - minYOffset) / minMaxOffset)))); 
    }

    public void ToggleUIElements() {
        if (buildMode) {
            
            buildMode = false;
        } else {
           
            buildMode = true;
        }
    }
}
