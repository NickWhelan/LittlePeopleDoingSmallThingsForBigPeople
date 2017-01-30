using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {
    public int PlayerNumber;
    public float Speed = 1;
    public int MaxVel;
    public bool inputPressed;
    KeyCode controllerA;
    KeyCode controllerB;
    KeyCode controllerX;
    KeyCode controllerY;
    KeyCode controllerRB;
    KeyCode controllerLB;
    KeyCode controllerL3;
    KeyCode controllerR3;
    Vector3 movement;
    Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            switch (PlayerNumber)
            {
                case 0:
                    controllerA = KeyCode.Joystick1Button0;
                    controllerB = KeyCode.Joystick1Button1;
                    controllerX = KeyCode.Joystick1Button2;
                    controllerY = KeyCode.Joystick1Button3;
                    controllerRB = KeyCode.Joystick1Button5;
                    controllerLB = KeyCode.Joystick1Button4;
                    controllerR3 = KeyCode.Joystick1Button9;
                    controllerL3 = KeyCode.Joystick1Button8;
                    break;
                case 1:
                    controllerA = KeyCode.Joystick2Button0;
                    controllerB = KeyCode.Joystick2Button1;
                    controllerX = KeyCode.Joystick2Button2;
                    controllerY = KeyCode.Joystick2Button3;
                    controllerRB = KeyCode.Joystick2Button5;
                    controllerLB = KeyCode.Joystick2Button4;
                    controllerR3 = KeyCode.Joystick2Button9;
                    controllerL3 = KeyCode.Joystick2Button8;
                    break;
                case 2:
                    controllerA = KeyCode.Joystick3Button0;
                    controllerB = KeyCode.Joystick3Button1;
                    controllerX = KeyCode.Joystick3Button2;
                    controllerY = KeyCode.Joystick3Button3;
                    controllerRB = KeyCode.Joystick3Button5;
                    controllerLB = KeyCode.Joystick3Button4;
                    controllerR3 = KeyCode.Joystick3Button9;
                    controllerL3 = KeyCode.Joystick3Button8;
                    break;
                case 3:
                    controllerA = KeyCode.Joystick4Button0;
                    controllerB = KeyCode.Joystick4Button1;
                    controllerX = KeyCode.Joystick4Button2;
                    controllerY = KeyCode.Joystick4Button3;
                    controllerRB = KeyCode.Joystick4Button5;
                    controllerLB = KeyCode.Joystick4Button4;
                    controllerR3 = KeyCode.Joystick4Button9;
                    controllerL3 = KeyCode.Joystick4Button8;
                    break;
                case 4:
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
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerNumber != 4)
        {
            movement = new Vector3(Input.GetAxis("Left Horizontal Player " + PlayerNumber), 0.0f, Input.GetAxis("Left Vertical Player " + PlayerNumber));
        }
        else 
        {
            movement = new Vector3(Input.GetAxis("Horizontal KeyBaord"), 0.0f, Input.GetAxis("Vertical KeyBaord"));
        }
        rigidbody.AddForce(movement * Speed);
        if (rigidbody.velocity.x > MaxVel) {
            rigidbody.velocity = new Vector3(MaxVel, 0, rigidbody.velocity.z);
        }
        else if (rigidbody.velocity.x < -MaxVel)
        {
            rigidbody.velocity = new Vector3(-MaxVel, 0, rigidbody.velocity.z);
        }
        if (rigidbody.velocity.z > MaxVel)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, MaxVel);
        }else if (rigidbody.velocity.z < -MaxVel)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, -MaxVel);
        }
        if(Input.GetKeyDown(controllerA))
        {
            Debug.Log("Input Down");
            inputPressed = true;
        }
        else if(Input.GetKeyUp(controllerA))
        {
            inputPressed = false;
        }
    }
}
