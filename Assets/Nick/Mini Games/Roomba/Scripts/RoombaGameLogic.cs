using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoombaGameLogic : MonoBehaviour
{
    AllGameLogic _AllGameLogic;
    


    public GameObject RoombaA, RoombaB;
    public GameObject PlayerPrefab;
    public bool DebugTest = false;
    public List<GameObject> TeamA, TeamB;
    public Text TimerText, Team1WinnerText, Team1WinnerSText, Team2WinnerText, Team2WinnerSText;

    public GameObject[] Dirt;
    public int NumberOfDirt;
    public Transform Max, Min,RoombaMax,RoombaMin;
    public Color color;

    Timer timer;
    Vector3 RoombaALastPos, RoombaBLastPos;

    bool EndGame;



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

        EndGame = false;

       RoombaALastPos = RoombaA.transform.position;
        RoombaBLastPos = RoombaB.transform.position;
        //Physics.IgnoreCollision(RoombaA.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>(), RoombaB.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>())


        TeamA = new List<GameObject>();
        TeamB = new List<GameObject>();
        if (!DebugTest)
        {
            _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
            //these are temp to determin the slot in in the roomba to spawn
            //here is where it is used:   RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.position
            int TeamAPlayerNum, TeamBPlayerNum;
            //set to one because the first ones name is "Team A player 1" rather than ""Team A player 0"
            TeamAPlayerNum = TeamBPlayerNum = 1;
            //this loops though all the players in the game. This is set in the main menu
            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                //load a temp gameobject that i change
                GameObject TempPlayer = PlayerPrefab;
                if (_AllGameLogic.Players[i].playerInfo.Team > 0)
                {
                    // make the PlayerControlls the same as the one in the menu
                    TempPlayer.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                    TempPlayer.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;
                }
                //check to see the team of the player
                if (_AllGameLogic.Players[i].playerInfo.Team == 1)
                {
                    //spawn the TempPlayer in the right spot and add it to the Team A's player list
                    TeamA.Add(Instantiate(TempPlayer, RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.position, RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.rotation));
                    //change its name and layer info
                    TeamA[TeamA.Count - 1].transform.parent = RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum);
                    TeamA[TeamA.Count - 1].name = "Player";
                    TeamA[TeamA.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamA[TeamA.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamA[TeamA.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                    TeamAPlayerNum++;
                }
                else if (_AllGameLogic.Players[i].playerInfo.Team == 2)
                {
                    TeamB.Add(Instantiate(TempPlayer, RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.position, RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.rotation));
                    TeamB[TeamB.Count - 1].transform.parent = RoombaB.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum);
                    TeamB[TeamB.Count - 1].name = "Player";
                    TeamB[TeamB.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamB[TeamB.Count - 1].layer = LayerMask.NameToLayer("Team 2");
                    TeamB[TeamB.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                    TeamBPlayerNum++;
                }
            }

            //loop through the AllGameLogic list of players and set them to the players in the scene
            bool found;
            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                found = false;
                //only loop if the player player isn't found and is on the correcret team
                //I loop though TeamA and and TeamB sepratly because i dont have a single player list for the scene
                for (int j = 0; j < TeamA.Count && !found && _AllGameLogic.Players[i].playerInfo.Team == 1; j++)
                {
                    //check to see if the player numbers are equal
                    //if they are set the gobal player ref to be the one in the scene
                    //and set found to true so it breaks from the loop and skips the next one. 
                    if (TeamA[j].GetComponent<PlayerControlls>().PlayerNum == _AllGameLogic.Players[i].PlayerNum)
                    {
                        _AllGameLogic.Players[i] = TeamA[j].GetComponent<PlayerControlls>();
                        found = true;
                    }
                }
                for (int j = 0; j < TeamB.Count && !found && _AllGameLogic.Players[i].playerInfo.Team == 2; j++)
                {
                    if (TeamB[j].GetComponent<PlayerControlls>().PlayerNum == _AllGameLogic.Players[i].PlayerNum)
                    {
                        _AllGameLogic.Players[i] = TeamB[j].GetComponent<PlayerControlls>();
                        found = true;
                    }
                }
            }
            //print(_AllGameLogic.Players[2] == null);
            for (int i = _AllGameLogic.Players.Count-1; i > 0; i--)
            {
                if (_AllGameLogic.Players[i] == null)
                {
                    _AllGameLogic.Players.RemoveAt(i);
                }
            }
        }
        RoombaA.GetComponent<RoombaLogic>().PlayersOnTeam = TeamA.ToArray();
        RoombaB.GetComponent<RoombaLogic>().PlayersOnTeam = TeamB.ToArray();

        RoombaA.GetComponent<RoombaLogic>().SetupTeam();
        RoombaB.GetComponent<RoombaLogic>().SetupTeam();
        MakeDirt();
    }

    void MakeDirt() {
        for (int i = 0; i < NumberOfDirt; i++) {
            GameObject Temp = Instantiate(Dirt[Random.Range(0,Dirt.Length)]); ;
            bool spawned = false;
            while (!spawned)
            {
                float RandomX = Random.Range(Max.position.x, Min.position.x);
                float RandomZ = Random.Range(Max.position.z, Min.position.z);
                if (
                    (RandomX < RoombaMin.position.x || RandomX > RoombaMax.position.x)||
                    (RandomZ < RoombaMin.position.z || RandomZ > RoombaMax.position.z))
                {
                    Temp.transform.position = new Vector3(RandomX, 0.8f, RandomZ);
                    spawned = true;
                }
            }
            Temp.name = "Dirt " + i;
            Temp.tag = "dirt";
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
        else if (!EndGame)
        {
            Team1WinnerSText.enabled = Team1WinnerText.enabled = true;
            Team2WinnerSText.enabled = Team2WinnerText.enabled = true;
            RoombaA.GetComponent<RoombaLogic>().EndOfRound = true;
            RoombaB.GetComponent<RoombaLogic>().EndOfRound = true;
            RoombaA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            RoombaB.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            EndGame = true;
        }
        else if (EndGame) {
            if (_AllGameLogic.Players[0].ButtonStartPressed) {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }/*
            else if (_AllGameLogic.Players[0].ButtonSelectPressed)
            {
                _AllGameLogic.CurrentGame++;
                if (_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame] != null)
                {
                    SceneManager.LoadScene(_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame], LoadSceneMode.Single);
                }
                else {
                    _AllGameLogic.CurrentGame = 0;
                    SceneManager.LoadScene(_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame], LoadSceneMode.Single);

                }
            }*/
        }

    }
}
