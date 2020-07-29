using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerConnectItem : MonoBehaviour
{
    public bool player1;

    public int playerNum;

    public string connectState;

    public string connectName;

    bool connected;

    public int currentTypeSelected = 0;
    public int verticalPosition = 0;
    public string[] truckComponentType;
    public GameObject[] wheelComponents;
    int selectedWheel = 0;
    public GameObject[] thrusterComponents;
    int selectedThruster = 0;
    public bool inCustomisation = false;
    public GameObject trucksForCustomisation;
    public GameObject playerTruck;
    public bool coolingDown;
    float cooldownDuration = .5f;
    float currentCooldown;

    Vector2 leftStick;

    private void OnDeviceLost(InputValue value)
    {
        if (playerNum != 1)
            connectState = "Press X to Join";
    }

    private void OnFaceButtonWest(InputValue value)
    {
        if (playerNum != 1)
        {
            connectState = connectName;
            connected = true;
            playerTruck.SetActive(true);
        }
        else 
        {
            playerTruck.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerNum == 1) 
        {
            connected = true;
            connectState = "Player 1";
        }

        coolingDown = false;
        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.white;

        //WriteCustomisationOptions();
        //ClearAllCustomisation();
        LoadCustomisationOptions();
        ApplyAllCustomisations();
    }

    private void OnLeftStick(InputValue value)
    {
        
            leftStick = value.Get<Vector2>();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        

        if (inCustomisation) 
        {

            if (!coolingDown) {

                if (leftStick.x > 0.5)
                {
                    //change currently selected option
                    if (verticalPosition == 0)
                    {

                        if (currentTypeSelected == truckComponentType.Length - 1)
                        {
                            currentTypeSelected = 0;
                        }
                        else
                        {
                            currentTypeSelected++;
                        }
                        Debug.Log(truckComponentType[currentTypeSelected]);
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = 
                            truckComponentType[currentTypeSelected];
                    }

                    if (verticalPosition == 1)
                    {
                        if (currentTypeSelected == 0)
                        {
                            if (selectedWheel < wheelComponents.Length - 1)
                            {
                                selectedWheel++;
                            }
                            else
                            {
                                selectedWheel = 0;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 
                                wheelComponents[selectedWheel].gameObject.transform.name.ToString();

                            foreach (GameObject wheel in wheelComponents) 
                            {
                                if (wheel != wheelComponents[selectedWheel])
                                {
                                    wheel.SetActive(false);
                                }
                                else 
                                {
                                    wheel.SetActive(true);
                                }
                            }
                        }

                        if (currentTypeSelected == 1)
                        {
                            if (selectedThruster < thrusterComponents.Length - 1)
                            {
                                selectedThruster++;
                            }
                            else
                            {
                                selectedThruster = 0;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 
                                thrusterComponents[selectedThruster].gameObject.transform.name.ToString();

                            foreach (GameObject thruster in thrusterComponents)
                            {
                                if (thruster != thrusterComponents[selectedThruster])
                                {
                                    thruster.SetActive(false);
                                }
                                else
                                {
                                    thruster.SetActive(true);
                                }
                            }
                        }

                    }
                }

                if (leftStick.x < -0.5)
                {
                    //change currently selected option
                    if (verticalPosition == 0)
                    {
                        if (currentTypeSelected > 0)
                        {
                            currentTypeSelected--;
                        }
                        else
                        {
                            currentTypeSelected = truckComponentType.Length - 1;
                        }
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = 
                            truckComponentType[currentTypeSelected];
                    }

                    if (verticalPosition == 1)
                    {
                        if (currentTypeSelected == 0)
                        {
                            if (selectedWheel == 0)
                            {
                                selectedWheel = wheelComponents.Length - 1;
                            }
                            else
                            {
                                selectedWheel--;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 
                                wheelComponents[selectedWheel].gameObject.transform.name.ToString();

                            foreach (GameObject wheel in wheelComponents)
                            {
                                if (wheel != wheelComponents[selectedWheel])
                                {
                                    wheel.SetActive(false);
                                }
                                else
                                {
                                    wheel.SetActive(true);
                                }
                            }
                        }

                        if (currentTypeSelected == 1)
                        {
                            if (selectedThruster == 0)
                            {
                                selectedThruster = thrusterComponents.Length - 1;
                            }
                            else
                            {
                                selectedThruster--;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 
                                thrusterComponents[selectedThruster].gameObject.transform.name.ToString();

                            foreach (GameObject thruster in thrusterComponents)
                            {
                                if (thruster != thrusterComponents[selectedThruster])
                                {
                                    thruster.SetActive(false);
                                }
                                else
                                {
                                    thruster.SetActive(true);
                                }
                            }
                        }
                    }
                }

                if (leftStick.y > 0.5)
                {
                    //change option to customise
                    if (verticalPosition == 0)
                    {
                        verticalPosition = 1;
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.gray;
                        playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().color = Color.white;

                    }
                    else
                    {
                        verticalPosition = 0;
                        playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().color = Color.gray;
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    }
                }

                if (leftStick.y < -0.5)
                {
                    //change option to customise
                    if (verticalPosition == 0)
                    {
                        verticalPosition = 1;
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.gray;
                        playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().color = Color.white;
                    }
                    else
                    {
                        verticalPosition = 0;
                        playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().color = Color.gray;
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    }
                }

                coolingDown = true;

            }
        }



        if (coolingDown) 
        {
            if (currentCooldown < cooldownDuration && leftStick.x != 0) 
            {
                currentCooldown += Time.deltaTime;
            }
            else 
            {
                coolingDown = false;
                currentCooldown = 0;
            }
        }
    }

    public void ToggleCustomisation() 
    {
        if (!inCustomisation)
        {
            inCustomisation = true;

            if (player1)
                trucksForCustomisation.SetActive(true);

        }
        else 
        {
            inCustomisation = false;

            if (player1)
                trucksForCustomisation.SetActive(false);
        }
    }

    public void LoadCustomisationOptions()
    {
        selectedWheel = PlayerPrefs.GetInt("WheelSelection", 0);
        selectedThruster = PlayerPrefs.GetInt("ThrusterSelection", 0);
        print("Loaded customisations");
    }

    public void WriteCustomisationOptions() 
    {
        PlayerPrefs.SetInt("Player " + playerNum + " WheelSelection", selectedWheel);
        PlayerPrefs.SetInt("Player " + playerNum + " ThrusterSelection", selectedThruster);
        
    }

    void ClearAllCustomisation()
    {
        currentTypeSelected = 0;
        while (currentTypeSelected < truckComponentType.Length - 1)
        {
            if (currentTypeSelected == 0)
            {
                //foreach (GameObject wheel in wheelComponent)
                //{
                    //wheel.SetActive(false);
                //}
            }

            if (currentTypeSelected == 1)
            {
                //foreach (GameObject thruster in thrusterComponent)
                //{
                    //thruster.SetActive(false);

                //}
            }
            currentTypeSelected++;
        }
        print("Cleared customisation");
    }

    void ApplyAllCustomisations()
    {
        //wheelComponent[selectedWheel].gameObject.SetActive(true);
        //thrusterComponent[selectedThruster].gameObject.SetActive(true);
        print("Loaded: Wheel" + wheelComponents[selectedWheel].ToString());
        print("Loaded: Thruster" + thrusterComponents[selectedThruster].ToString());
    }
}
