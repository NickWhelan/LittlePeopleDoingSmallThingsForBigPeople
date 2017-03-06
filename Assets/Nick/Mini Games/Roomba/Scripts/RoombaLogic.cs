using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaLogic : MonoBehaviour
{
    public int TeamNumber,score;
    public GameObject RoombaGround, Camera;
    public List<RoombaMovementBox> MovementBoxs;
    public SuckUp vacuum;
    public Text UIScore;
    public Rigidbody rigidbody;
    public bool EndOfRound = false;
    GameObject[] PlayersOnTeam;
    List<Vector3> LastPos;
   

    //Movment
    bool Forward,Back,Left,Right;

    float MoveX, SpinY;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
      LastPos = new List<Vector3>();
    }

    public void SetupTeam() {
        PlayersOnTeam = GameObject.FindGameObjectsWithTag("Player Team " + TeamNumber);


        foreach (GameObject Player in PlayersOnTeam)
        {
           //Physics.IgnoreCollision(GetComponent<SphereCollider>(), Player.GetComponent<CapsuleCollider>())
            LastPos.Add(Player.transform.position);
        }
        foreach (RoombaMovementBox mat in MovementBoxs)
        {
            foreach (GameObject Player in PlayersOnTeam)
            {
                mat.PlayersOnTeam.Add(Player);
            }
        }
    }

    void FixedUpdate() {
        if (PlayersOnTeam.Length > 0)
        {
            RoombaBoundCheck();
            MoveRoomba();
        }
        if (vacuum.isTriggered) {
            score++;
            UIScore.text = score.ToString();
            vacuum.isTriggered = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!EndOfRound) {
            if (Forward && !Back)
            {
                MoveX = 10;
            }
            else if (Back && !Forward)
            {
                MoveX = -10;
            }
            else if ((Forward && Back) || (!Forward && !Back))
            {
                MoveX = 0;
                rigidbody.velocity = Vector3.zero;
            }
            if (Left && !Right)
            {
                SpinY = -5;
            }
            else if (Right && !Left)
            {
                SpinY = 5;
            }
            else if ((Right && Left) || (!Right && !Left))
            {
                SpinY = 0;
                rigidbody.angularVelocity = Vector3.zero;
            }
            rigidbody.AddRelativeForce(new Vector3(0, 0, MoveX));
            rigidbody.AddRelativeTorque(new Vector3(0, SpinY, 0));
            Camera.transform.position = new Vector3(transform.position.x, Camera.transform.position.y, transform.position.z);
        }
    }
    void MoveRoomba() {
        foreach (RoombaMovementBox mat in MovementBoxs)
        {
            if (mat.name == "Forward")
            {
                Forward = mat.isTriggered;
            }
            else if (mat.name == "Back")
            {
                Back = mat.isTriggered;
            }
            else if (mat.name == "Left")
            {
                Left = mat.isTriggered;
            }
            else if (mat.name == "Right")
            {
                Right = mat.isTriggered;
            }
            
        }
    }
    void RoombaBoundCheck()
    {
        for (int i =0; i < PlayersOnTeam.Length; i++) { 
            if (Vector3.Distance(PlayersOnTeam[i].transform.position, transform.position) > transform.lossyScale.x / 2)
            {
                PlayersOnTeam[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                PlayersOnTeam[i].transform.localPosition = LastPos[i];
            }
            else {
                LastPos[i] = PlayersOnTeam[i].transform.localPosition;
            }
        }
    }
}
