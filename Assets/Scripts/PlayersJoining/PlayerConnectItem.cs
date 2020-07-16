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

    int currentTypeSelected;
    int verticalPosition = 0;
    public string[] truckComponentType;
    public string[] wheelComponent;
    int selectedWheel = 0;
    public string[] thrusterComponent;
    int selectedThruster = 0;
    public bool inCustomisation = false;
    public GameObject trucksForCustomisation;
    public GameObject playerTruck;
    public bool coolingDown;
    float cooldownDuration = 2f;
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

        WriteCustomisationOptions();
        ClearAllCustomisation();
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

                        if (currentTypeSelected == truckComponentType.Length)
                        {
                            currentTypeSelected = 0;
                        }
                        else
                        {
                            currentTypeSelected++;
                        }
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = truckComponentType[currentTypeSelected];
                    }

                    if (verticalPosition == 1)
                    {
                        if (currentTypeSelected == 0)
                        {
                            if (selectedWheel < wheelComponent.Length)
                            {
                                selectedWheel++;
                            }
                            else
                            {
                                selectedWheel = 0;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = wheelComponent[selectedWheel];
                        }

                        if (currentTypeSelected == 1)
                        {
                            if (selectedThruster < thrusterComponent.Length)
                            {
                                selectedThruster++;
                            }
                            else
                            {
                                selectedThruster = 0;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = thrusterComponent[selectedThruster]; ;
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
                            currentTypeSelected = truckComponentType.Length;
                        }
                        playerTruck.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = truckComponentType[currentTypeSelected];
                    }

                    if (verticalPosition == 1)
                    {
                        if (currentTypeSelected == 0)
                        {
                            if (selectedWheel == 0)
                            {
                                selectedWheel = wheelComponent.Length;
                            }
                            else
                            {
                                selectedWheel--;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = wheelComponent[selectedWheel];
                        }

                        if (currentTypeSelected == 1)
                        {
                            if (selectedThruster == 0)
                            {
                                selectedThruster = thrusterComponent.Length;
                            }
                            else
                            {
                                selectedThruster--;
                            }
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = thrusterComponent[selectedThruster];
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
            if (currentCooldown < cooldownDuration) 
            {
                currentCooldown += Time.deltaTime;
            }
            else 
            {
                coolingDown = false;
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
        while (currentTypeSelected < truckComponentType.Length)
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
        print("Loaded: Wheel" + wheelComponent[selectedWheel].ToString());
        print("Loaded: Thruster" + thrusterComponent[selectedThruster].ToString());
    }
}
