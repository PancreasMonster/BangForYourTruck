﻿using System;
using System.Collections;
using System.Collections.Generic;
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
            connected = true;
            connectState = "Player 1";
            //playerTruck.SetActive(true);
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
            if (playerNum == 1) 
            {
                if (leftStick.x > 0.1) 
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
                            selectedWheel++;
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = wheelComponent[selectedWheel];
                        }
                    
                        if (currentTypeSelected == 1) 
                        {
                            selectedThruster++;
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = thrusterComponent[selectedThruster]; ;
                        }

                    }
                }

                if (leftStick.x < -0.1)
                {
                    //change currently selected option
                    if (verticalPosition == 0)
                    {
                        if (currentTypeSelected == 0)
                        {
                            currentTypeSelected = truckComponentType.Length;
                        }
                        else 
                        {
                            currentTypeSelected--;
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
                            playerTruck.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = thrusterComponent[selectedThruster]; ;
                        }
                    }
                }

                if (leftStick.y > 0.1)
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

                if (leftStick.y < -0.1)
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
                playerTruck.transform.GetChild(1).transform.GetChild(0);
                playerTruck.transform.GetChild(1).transform.GetChild(1);

            }

            if (playerNum == 2)
            {
                playerTruck.transform.GetChild(1).transform.GetChild(0);
                playerTruck.transform.GetChild(1).transform.GetChild(1);
            }

            if (playerNum == 3)
            {
                playerTruck.transform.GetChild(1).transform.GetChild(0);
                playerTruck.transform.GetChild(1).transform.GetChild(1);
            }

            if (playerNum == 4)
            {
                playerTruck.transform.GetChild(1).transform.GetChild(0);
                playerTruck.transform.GetChild(1).transform.GetChild(1);
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
}
