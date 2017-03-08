using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaGameLogic : MonoBehaviour
{
    GameObject _AllGameLogic;
    


    public GameObject RoombaA, RoombaB;
    public GameObject PlayerPrefab;
    public bool DebugTest = false;
    public List<GameObject> TeamA, TeamB;
    public Text TimerText, Team1WinnerText, Team1WinnerSText, Team2WinnerText, Team2WinnerSText;

    public GameObject Dirt;
    public int NumberOfDirt;
    public Transform Max, Min;
    public Color color;

    Timer timer;
    Vector3 RoombaALastPos, RoombaBLastPos;





    // Use this for initialization
    void Start()
    {
        timer = new Timer();
        timer.isCountingDown = true;
        timer.StartTime = 60;
        timer.EndTime = 0;
        timer.Start();


       Team1WinnerText.enabled = false;
        Team1WinnerSText.enabled = false;
        Team2WinnerText.enabled = false;
        Team2WinnerSText.enabled = false;

        RoombaALastPos = RoombaA.transform.position;
        RoombaBLastPos = RoombaB.transform.position;
        //Physics.IgnoreCollision(RoombaA.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>(), RoombaB.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>())
        _AllGameLogic = GameObject.Find("OverWatch");

        TeamA = new List<GameObject>();
        TeamB = new List<GameObject>();
        if (!DebugTest)
        {
            int TeamAPlayerNum, TeamBPlayerNum;
            TeamAPlayerNum = TeamBPlayerNum = 1;
            foreach (PlayerControlls player in _AllGameLogic.GetComponent<AllGameLogic>().Players)
            {
                GameObject TempPlayer = PlayerPrefab;
                if (player.playerInfo.Team > 0)
                {
                    TempPlayer.GetComponent<PlayerControlls>().playerInfo = player.playerInfo;
                    TempPlayer.GetComponent<PlayerControlls>().PlayerNum = player.playerInfo.PlayerNum;
                }
                if (player.playerInfo.Team == 1)
                {
                    TeamA.Add(Instantiate(TempPlayer, RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.position, RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.rotation));
                    TeamA[TeamA.Count - 1].transform.parent = RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum);
                    TeamA[TeamA.Count - 1].name = "Player";
                    TeamA[TeamA.Count - 1].tag = "Player Team " + player.playerInfo.Team;
                    TeamA[TeamA.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamA[TeamA.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                    TeamAPlayerNum++;
                }
                else if (player.playerInfo.Team == 2)
                {
                    TeamB.Add(Instantiate(TempPlayer, RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.position, RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.rotation));
                    TeamB[TeamB.Count - 1].transform.parent = RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum);
                    TeamB[TeamB.Count - 1].name = "Player";
                    TeamB[TeamB.Count - 1].tag = "Player Team " + player.playerInfo.Team;
                    TeamB[TeamB.Count - 1].layer = LayerMask.NameToLayer("Team 2");
                    TeamB[TeamB.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                    TeamBPlayerNum++;
                }
            }
        }
        RoombaA.GetComponent<RoombaLogic>().SetupTeam();
        RoombaB.GetComponent<RoombaLogic>().SetupTeam();
        MakeDirt();
    }

    void MakeDirt() {
        for (int i = 0; i < NumberOfDirt; i++) {
            GameObject Temp =Instantiate(Dirt, new Vector3(Random.RandomRange(Max.position.x, Min.position.x), 0.8f, Random.RandomRange(Max.position.z, Min.position.z)), Max.localRotation);
            Temp.name = "Dirt " + i;
            
            Temp.GetComponent<Renderer>().material.color = color; 
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!timer.isTimeUp)
        {
            timer.Update();
            if (timer.CurrentTime < 10)
            {
                TimerText.text = string.Format("{0:0.00}", timer.CurrentTime);
            }
            else
            {
                TimerText.text = ((int)timer.CurrentTime).ToString();
            }
            if (RoombaA.GetComponent<RoombaLogic>().score > RoombaB.GetComponent<RoombaLogic>().score)
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Winner";
                Team2WinnerSText.text = Team2WinnerText.text = "Loser";
            }
            else if (RoombaA.GetComponent<RoombaLogic>().score < RoombaB.GetComponent<RoombaLogic>().score)
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Loser";
                Team2WinnerSText.text = Team2WinnerText.text = "Winner";
            }
            else
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Tie";
                Team2WinnerSText.text = Team2WinnerText.text = "Tie";
            }



            Debug.DrawLine(new Vector3(RoombaA.transform.position.x, 1, RoombaA.transform.position.z), new Vector3(RoombaA.transform.position.x + (RoombaA.transform.lossyScale.x / 2), 0.5f, RoombaA.transform.position.z));
            Debug.DrawLine(new Vector3(RoombaA.transform.position.x, 1, RoombaA.transform.position.z), new Vector3(RoombaA.transform.position.x, 0.5f, RoombaA.transform.position.z + (RoombaA.transform.lossyScale.z / 2)));
            if (Vector3.Distance(RoombaA.transform.position, RoombaB.transform.position) <= (RoombaA.transform.lossyScale.x / 2) + (RoombaB.transform.lossyScale.x / 2))
            {
                Debug.DrawLine(new Vector3(RoombaA.transform.position.x, 1, RoombaA.transform.position.z), new Vector3(RoombaB.transform.position.x, 1, RoombaB.transform.position.z), Color.red);
                RoombaA.transform.position = RoombaALastPos;
                RoombaB.transform.position = RoombaBLastPos;
            }
            else
            {

                RoombaALastPos = RoombaA.transform.position;
                RoombaBLastPos = RoombaB.transform.position;
            }
        }
        else
        {
            Team1WinnerSText.enabled = Team1WinnerText.enabled = true;
            Team2WinnerSText.enabled = Team2WinnerText.enabled = true;
            RoombaA.GetComponent<RoombaLogic>().EndOfRound = true;
            RoombaB.GetComponent<RoombaLogic>().EndOfRound = true;
            RoombaA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            RoombaB.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
