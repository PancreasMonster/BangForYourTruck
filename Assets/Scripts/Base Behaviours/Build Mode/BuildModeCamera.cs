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
    public float maxVerticalHeight = 5f, minVerticalHeight = -5f;
    public float mainCamFoVBaseValue, mainCamFoVScaleValue;
    public float baseTargetRange, scaleTargetRange;

    private float verticalHeight;
    private float minYOffset, maxYOffset;
    private float minMaxOffset;

    // Start is called before the first frame update
    void Start()
    {
        verticalHeight = offset.y;
        minYOffset = verticalHeight + minVerticalHeight;
        maxYOffset = verticalHeight + maxVerticalHeight;
        minMaxOffset = Mathf.Abs(maxVerticalHeight) + Mathf.Abs(minVerticalHeight);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offset.x, verticalHeight, offset.z);
        transform.position = player.position + offset;
        offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed * Time.deltaTime, Vector3.up) * offset;       
        transform.LookAt(target.position);
        mainCam.fieldOfView = mainCamFoVBaseValue + (mainCamFoVScaleValue * (minMaxOffset / verticalHeight));
        verticalHeight = Mathf.Clamp(verticalHeight, minYOffset, maxYOffset);
        if(verticalHeight >= minYOffset && verticalHeight <= maxYOffset)
        verticalHeight += -Input.GetAxisRaw("RVertical" + playerNum.ToString()) * verticalSpeed * Time.deltaTime;
        target.localPosition = new Vector3(0, 0, 15 + (12.5f * (verticalHeight / minMaxOffset))); 
    }
}
