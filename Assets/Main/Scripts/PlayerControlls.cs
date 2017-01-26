using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {
    public int PlayerNumber;
    KeyCode controllerA;
    KeyCode controllerB;
    KeyCode controllerX;
    KeyCode controllerY;
    KeyCode controllerRB;
    KeyCode controllerLB;
    KeyCode controllerL3;
    KeyCode controllerR3;



    // Use this for initialization
    void Start () {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
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
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
