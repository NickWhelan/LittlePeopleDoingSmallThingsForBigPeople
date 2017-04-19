using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoombaLogic : MonoBehaviour
{
    public int TeamNumber,score;
    public GameObject RoombaGround, Camera;
    public List<RoombaMovementBox> MovementBoxs;
    public SuckUp vacuum;
    public Text UIScore;
    public Rigidbody rigidbody;
    public bool EndOfRound = false;
    public Material LineMat;

    public GameObject[] PlayersOnTeam;
    int[] PlayersOnMats;
    public List<Vector3> LastPos;

    LineRenderer line;

    //Movment
    bool Forward,Back,Left,Right;

    float MoveX, SpinY;
    // Use this for initialization
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.SetWidth(0.05f, 0.05f);
        line.material = LineMat; 
        rigidbody = GetComponent<Rigidbody>();
        LastPos = new List<Vector3>();
       
        PlayersOnMats = new int[4];
        PlayersOnMats[0] = 0;
        PlayersOnMats[1] = 0;
        PlayersOnMats[2] = 0;
        PlayersOnMats[3] = 0;

    }

    public void SetupTeam() {
        foreach (GameObject Player in PlayersOnTeam)
        {
           //Physics.IgnoreCollision(GetComponent<SphereCollider>(), Player.GetComponent<CapsuleCollider>())
            LastPos.Add(Player.transform.position);
        }
        //print(this.name + "LastPos.Count: " + LastPos.Count);
        foreach (RoombaMovementBox mat in MovementBoxs)
        {
            foreach (GameObject Player in PlayersOnTeam)
            {
                mat.PlayersOnTeam.Add(Player);
            }
        }
    }

    void FixedUpdate() {
        line.SetPosition(0, this.transform.position);
        float Dist = 10000;
        Vector3 temp_Dirt = Vector3.zero;
        foreach (GameObject dirt in GameObject.FindGameObjectsWithTag("dirt"))
        {
            if (Vector3.Distance(dirt.transform.position, transform.position) < Dist)
            {
                Dist = Vector3.Distance(dirt.transform.position, transform.position);
                temp_Dirt = dirt.transform.position;
            }
        }
        line.SetPosition(1, temp_Dirt);

        if (PlayersOnTeam.Length > 0 && LastPos.Count > 0)
        {
            RoombaBoundCheck();
            MoveRoomba();
        }
        else {
            //Debug.LogError(name + " PlayersOnTeam.Length: " + PlayersOnTeam.Length);
            //Debug.LogError(name + " LastPos.Count: " + LastPos.Count);
            foreach (GameObject Player in PlayersOnTeam)
            {
                //Physics.IgnoreCollision(GetComponent<SphereCollider>(), Player.GetComponent<CapsuleCollider>())
                LastPos.Add(Player.transform.position);
            }
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
                if (PlayersOnMats[0] == 1)
                {
                    MoveX = 10;
                }
                else if (PlayersOnMats[0] > 1) {
                    MoveX = 15;
                }
                
            }
            else if (Back && !Forward)
            {
                if (PlayersOnMats[1] == 1)
                {
                    MoveX = -10;
                }
                else if (PlayersOnMats[1] > 1)
                {
                    MoveX = 15;
                }
            }
            else if ((Forward && Back) || (!Forward && !Back))
            {
                MoveX = 0;
                rigidbody.velocity = Vector3.zero;
            }
            if (Left && !Right)
            {
                if (PlayersOnMats[2] == 1)
                {
                    SpinY = -5;
                }
                else if (PlayersOnMats[2] > 1)
                {
                    SpinY = -10;
                }
            }
            else if (Right && !Left)
            {
                if (PlayersOnMats[3] == 1)
                {
                    SpinY = 5;
                }
                else if (PlayersOnMats[3] > 1)
                {
                    SpinY = 10;
                }
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
                PlayersOnMats[0] = mat.playersInside;
            }
            else if (mat.name == "Back")
            {
                Back = mat.isTriggered;
                PlayersOnMats[1] = mat.playersInside;
            }
            else if (mat.name == "Left")
            {
                Left = mat.isTriggered;
                PlayersOnMats[2] = mat.playersInside;
            }
            else if (mat.name == "Right")
            {
                Right = mat.isTriggered;
                PlayersOnMats[3] = mat.playersInside;
            }
            
        }
    }
    void RoombaBoundCheck()
    {

        for (int i = 0; i < PlayersOnTeam.Length; i++)
        {
            /*
            PlayersOnTeam[i].transform.position = new Vector3(
                                                    Mathf.Clamp(PlayersOnTeam[i].transform.position.x, transform.position.x - transform.lossyScale.x / 2.5f, transform.position.x + transform.lossyScale.x / 2.5f)
                                                    , PlayersOnTeam[i].transform.position.y,
                                                    Mathf.Clamp(PlayersOnTeam[i].transform.position.z, transform.position.z - transform.lossyScale.z / 2.5f, transform.position.z + transform.lossyScale.z / 2.5f)
                                                    );
             */
            if (Vector3.Distance(PlayersOnTeam[i].transform.position, transform.position) > transform.lossyScale.x / 2)
            {
                //print(this.name + " PlayersOnTeam.Length: " + PlayersOnTeam.Length + " i: " + i);
                //print(this.name + "LastPos.Count: " + LastPos.Count + " i: " + i);
                PlayersOnTeam[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                PlayersOnTeam[i].transform.localPosition = LastPos[i];
            }
            else
            {
                LastPos[i] = PlayersOnTeam[i].transform.localPosition;
            }
        }
    }
    
        /*
        Vector3 roombaPos = transform.position;
            Vector3 playerPos = PlayersOnTeam[i].transform.position;

            float theta = Mathf.Atan2(playerPos.z - roombaPos.z, playerPos.x - roombaPos.x);

            float dist = Vector3.Distance(roombaPos, playerPos);
            float radius = transform.lossyScale.x / 2.5f;


            if (dist > radius * 1.15f)
            {
                PlayersOnTeam[i].transform.position = roombaPos + new Vector3(Mathf.Cos(theta) * radius, 0.3f, Mathf.Sin(theta) * radius);
            }

            /*
           
        }
    }*/
}
