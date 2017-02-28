﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {
    public Player playerInfo;
    public float Speed = 1;
    public int MaxVel;
    public int PlayerNum;
    public bool isOnMenu;
    Vector3 movement;
    Rigidbody rigidbody;

    KeyCode controllerA;
    KeyCode controllerB;
    KeyCode controllerX;
    KeyCode controllerY;
    KeyCode controllerRB;
    KeyCode controllerLB;
    KeyCode controllerL3;
    KeyCode controllerR3;

    [Header("Buttons")]
    public bool ButtonAPressed;
    public bool ButtonBPressed;
    public bool ButtonXPressed;
    public bool ButtonYPressed;
    public bool ButtonRBPressed;
    public bool ButtonLBPressed;
    public bool ButtonL3Pressed;
    public bool ButtonR3Pressed;

    
    

    // Use this for initialization
    void Start () {
        playerInfo = new Player(PlayerNum);
        rigidbody = GetComponent<Rigidbody>();
            switch (playerInfo.PlayerNum)
            {
                case 1:
                    controllerA = KeyCode.Joystick1Button0;
                    controllerB = KeyCode.Joystick1Button1;
                    controllerX = KeyCode.Joystick1Button2;
                    controllerY = KeyCode.Joystick1Button3;
                    controllerRB = KeyCode.Joystick1Button5;
                    controllerLB = KeyCode.Joystick1Button4;
                    controllerR3 = KeyCode.Joystick1Button9;
                    controllerL3 = KeyCode.Joystick1Button8;
                    break;
                case 2:
                    controllerA = KeyCode.Joystick2Button0;
                    controllerB = KeyCode.Joystick2Button1;
                    controllerX = KeyCode.Joystick2Button2;
                    controllerY = KeyCode.Joystick2Button3;
                    controllerRB = KeyCode.Joystick2Button5;
                    controllerLB = KeyCode.Joystick2Button4;
                    controllerR3 = KeyCode.Joystick2Button9;
                    controllerL3 = KeyCode.Joystick2Button8;
                    break;
                case 3:
                    controllerA = KeyCode.Joystick3Button0;
                    controllerB = KeyCode.Joystick3Button1;
                    controllerX = KeyCode.Joystick3Button2;
                    controllerY = KeyCode.Joystick3Button3;
                    controllerRB = KeyCode.Joystick3Button5;
                    controllerLB = KeyCode.Joystick3Button4;
                    controllerR3 = KeyCode.Joystick3Button9;
                    controllerL3 = KeyCode.Joystick3Button8;
                    break;
                case 4:
                    controllerA = KeyCode.Joystick4Button0;
                    controllerB = KeyCode.Joystick4Button1;
                    controllerX = KeyCode.Joystick4Button2;
                    controllerY = KeyCode.Joystick4Button3;
                    controllerRB = KeyCode.Joystick4Button5;
                    controllerLB = KeyCode.Joystick4Button4;
                    controllerR3 = KeyCode.Joystick4Button9;
                    controllerL3 = KeyCode.Joystick4Button8;
                    break;
                case 5:
                    controllerA = KeyCode.Space;
                    controllerB = KeyCode.LeftControl;
                    controllerX = KeyCode.E;
                    controllerY = KeyCode.Q;
                    controllerRB = KeyCode.F;
                    controllerLB = KeyCode.G;
                    controllerR3 = KeyCode.R;
                    controllerL3 = KeyCode.LeftShift;
                    break;
            }
    }
    // Update is called once per frame
    void Update()
    {
        HandleControllerInput();
        rigidbody.AddForce(movement * Speed);
        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -MaxVel, MaxVel), Mathf.Clamp(rigidbody.velocity.y, -MaxVel, MaxVel), Mathf.Clamp(rigidbody.velocity.z, -MaxVel, MaxVel));
    }
    void HandleControllerInput() {

        if (PlayerNum == 5)
        {
            if (!isOnMenu)
            {
                movement = new Vector3(Input.GetAxis("Horizontal KeyBaord"), 0.0f, Input.GetAxis("Vertical KeyBaord"));
            }
            else {
                movement = new Vector3(Input.GetAxis("Horizontal KeyBaord"), Input.GetAxis("Vertical KeyBaord"),0.0f);

            }
        }
        else
        {
            if (!isOnMenu)
            {
                movement = new Vector3(Input.GetAxis("Left Horizontal Player " + playerInfo.PlayerNum), 0.0f, Input.GetAxis("Left Vertical Player " + playerInfo.PlayerNum));
            }
            else {
                movement = new Vector3(Input.GetAxis("Left Horizontal Player " + playerInfo.PlayerNum), Input.GetAxis("Left Vertical Player " + playerInfo.PlayerNum),0.0f);

            }
        }

        if (Input.GetKey(controllerA))
        {
            ButtonAPressed = true;
        }
        else if (!Input.GetKey(controllerA))
        {
            ButtonAPressed = false;
        }

        if (Input.GetKey(controllerB))
        {
            ButtonBPressed = true;
        }
        else if (!Input.GetKey(controllerB))
        {
            ButtonBPressed = false;
        }

        if (Input.GetKey(controllerX))
        {
            ButtonXPressed = true;
        }
        else if (!Input.GetKey(controllerX))
        {
            ButtonXPressed = false;
        }

        if (Input.GetKey(controllerY))
        {
            ButtonYPressed = true;
        }
        else if (!Input.GetKey(controllerY))
        {
            ButtonYPressed = false;
        }

        if (Input.GetKey(controllerRB))
        {
            ButtonRBPressed = true;
        }
        else if (!Input.GetKey(controllerRB))
        {
            ButtonRBPressed = false;
        }

        if (Input.GetKey(controllerLB))
        {
            ButtonLBPressed = true;
        }
        else if (!Input.GetKey(controllerLB))
        {
            ButtonLBPressed = false;
        }

        if (Input.GetKey(controllerR3))
        {
            ButtonR3Pressed = true;
        }
        else if (!Input.GetKey(controllerR3))
        {
            ButtonR3Pressed = false;
        }

        if (Input.GetKey(controllerL3))
        {
            ButtonL3Pressed = true;
        }
        else if (!Input.GetKey(controllerL3))
        {
            ButtonL3Pressed = false;
        }
    }
}
