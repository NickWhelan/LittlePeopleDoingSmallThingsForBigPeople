using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {
    public Animator PlayerAnimations;
    public Player playerInfo;
    public float Speed = 1;
    public float MaxVel;
    public int PlayerNum;
    public bool isOnMenu,Frozen;
    public bool AllowJump = false;
    public bool isJumping = false;
    public bool invertXZ = false;
    public Vector3 movement;

    Rigidbody rigidbody;

    KeyCode controllerA;
    KeyCode controllerB;
    KeyCode controllerX;
    KeyCode controllerY;
    KeyCode controllerRB;
    KeyCode controllerLB;
    KeyCode controllerL3;
    KeyCode controllerR3;
    KeyCode controllerStart;
    KeyCode controllerSelect;

    [Header("Buttons")]
    public bool ButtonAPressed;
    public bool ButtonBPressed;
    public bool ButtonXPressed;
    public bool ButtonYPressed;
    public bool ButtonRBPressed;
    public bool ButtonLBPressed;
    public bool ButtonL3Pressed;
    public bool ButtonR3Pressed;
    public bool ButtonStartPressed;
    public bool ButtonSelectPressed;


    // Use this for initialization
    public void Start () {
        playerInfo = new Player(PlayerNum);
        rigidbody = gameObject.GetComponent<Rigidbody>();
        switch (playerInfo.PlayerNum)
        {
            case 1:
                controllerA = KeyCode.Joystick1Button0;
                controllerB = KeyCode.Joystick1Button1;
                controllerX = KeyCode.Joystick1Button2;
                controllerY = KeyCode.Joystick1Button3;
                controllerLB = KeyCode.Joystick1Button4;
                controllerRB = KeyCode.Joystick1Button5;
                controllerSelect = KeyCode.Joystick1Button6;
                controllerStart = KeyCode.Joystick1Button7;
                controllerL3 = KeyCode.Joystick1Button8;
                controllerR3 = KeyCode.Joystick1Button9;
                break;
            case 2:
                controllerA = KeyCode.Joystick2Button0;
                controllerB = KeyCode.Joystick2Button1;
                controllerX = KeyCode.Joystick2Button2;
                controllerY = KeyCode.Joystick2Button3;
                controllerLB = KeyCode.Joystick2Button4;
                controllerRB = KeyCode.Joystick2Button5;
                controllerSelect = KeyCode.Joystick2Button6;
                controllerStart = KeyCode.Joystick2Button7;
                controllerL3 = KeyCode.Joystick2Button8;
                controllerR3 = KeyCode.Joystick2Button9;
                break;
            case 3:
                controllerA = KeyCode.Joystick3Button0;
                controllerB = KeyCode.Joystick3Button1;
                controllerX = KeyCode.Joystick3Button2;
                controllerY = KeyCode.Joystick3Button3;
                controllerLB = KeyCode.Joystick3Button4;
                controllerRB = KeyCode.Joystick3Button5;
                controllerSelect = KeyCode.Joystick3Button6;
                controllerStart = KeyCode.Joystick3Button7;
                controllerL3 = KeyCode.Joystick3Button8;
                controllerR3 = KeyCode.Joystick3Button9;
                break;
            case 4:
                controllerA = KeyCode.Joystick4Button0;
                controllerB = KeyCode.Joystick4Button1;
                controllerX = KeyCode.Joystick4Button2;
                controllerY = KeyCode.Joystick4Button3;
                controllerLB = KeyCode.Joystick4Button4;
                controllerRB = KeyCode.Joystick4Button5;
                controllerSelect = KeyCode.Joystick4Button6;
                controllerStart = KeyCode.Joystick4Button7;
                controllerL3 = KeyCode.Joystick4Button8;
                controllerR3 = KeyCode.Joystick4Button9;

                break;
            case 5:
                controllerA = KeyCode.Space;
                controllerB = KeyCode.LeftControl;
                controllerX = KeyCode.E;
                controllerY = KeyCode.Y;
                controllerRB = KeyCode.F;
                controllerLB = KeyCode.G;
                controllerR3 = KeyCode.R;
                controllerL3 = KeyCode.LeftShift;
                controllerSelect = KeyCode.RightShift;
                controllerStart = KeyCode.Return;
                break;
        }
        if (gameObject.GetComponent<Renderer>() != null)
        {
            switch (playerInfo.CurrentCharacter)
            {

                case Player.Character.Astronaut:
                    gameObject.GetComponent<Renderer>().material.color = Color.black;
                    break;
                case Player.Character.BigBusinessOwner:
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    break;
                case Player.Character.Cowboy:
                    gameObject.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case Player.Character.Ninja:
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case Player.Character.Mafioso:
                    gameObject.GetComponent<Renderer>().material.color = Color.black;
                    break;
                case Player.Character.Mathematician:
                    break;
                case Player.Character.RockSinger:
                    break;
                case Player.Character.StrangeDoctor:
                    break;
                case Player.Character.Survivalist:
                    break;
                case Player.Character.WaitStaff:
                    break;
                case Player.Character.Budgie:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!Frozen)
        {
            rigidbody.AddForce(movement * Speed);
            rigidbody.velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -MaxVel, MaxVel), Mathf.Clamp(GetComponent<Rigidbody>().velocity.y, -MaxVel, MaxVel), Mathf.Clamp(GetComponent<Rigidbody>().velocity.z, -MaxVel, MaxVel));
            if (AllowJump) {
                if (ButtonAPressed)
                {
                    rigidbody.AddForce(new Vector3(0, 20, 0));
                    isJumping = true;
                } else if (!ButtonAPressed) {
                    isJumping = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleControllerInput();
        HandleControllerAxisInput();
        if (PlayerAnimations != null)
        {
            if (movement.z > 0.5)
            {
                PlayerAnimations.SetFloat("MoveSpeed", 1);
            }
            else if (movement.z < -0.5)
            {
                PlayerAnimations.SetFloat("MoveSpeed", -1);
            }
        }

    }
    void HandleControllerAxisInput()
    {
        if (PlayerNum == 5)
        {
            if (!isOnMenu)
            {
                if (invertXZ)
                {
                    movement = new Vector3(Input.GetAxis("Horizontal KeyBaord")*-1, 0.01f, Input.GetAxis("Vertical KeyBaord") * -1);
                }
                else
                {
                    movement = new Vector3(Input.GetAxis("Horizontal KeyBaord"), 0.01f, Input.GetAxis("Vertical KeyBaord"));
                }
            }
            else
            {
                movement = new Vector3(Input.GetAxis("Horizontal KeyBaord"), Input.GetAxis("Vertical KeyBaord"), 0.0f);
            }
        }
        else
        {
            if (!isOnMenu)
            {
                movement = new Vector3(Input.GetAxis("Left Horizontal Player " + playerInfo.PlayerNum), 0.01f, Input.GetAxis("Left Vertical Player " + playerInfo.PlayerNum));
            }
            else
            {
                movement = new Vector3(Input.GetAxis("Left Horizontal Player " + playerInfo.PlayerNum), Input.GetAxis("Left Vertical Player " + playerInfo.PlayerNum), 0.01f);

            }
        }
    }
    void HandleControllerInput()
    {

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

        if (Input.GetKey(controllerStart))
        {
            ButtonStartPressed = true;
        }
        else if (!Input.GetKey(controllerStart))
        {
            ButtonStartPressed = false;
        }
        if (Input.GetKey(controllerSelect))
        {
            ButtonSelectPressed = true;
        }
        else if (!Input.GetKey(controllerSelect))
        {
            ButtonSelectPressed = false;
        }
    }
}
