using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToasterGameLogic : MonoBehaviour {
    AllGameLogic _AllGameLogic;

    public GameObject Bread1, Bread2;
    public GameObject PlayerPrefabTeamA, PlayerPrefabTeamB;
    public bool DebugTest,EndGame;
    public Text TimerText, Team1WinnerText, Team1WinnerSText, Team2WinnerText, Team2WinnerSText;

    public GameObject TeamAPlayers, TeamBPlayers;

    public GameObject ToasterTeam1, ToasterTeam2;

    Timer timer;
    // Use this for initialization
    void Start () {
        timer = new Timer();
        timer.isCountingDown = true;
        timer.StartTime = 10;
        timer.EndTime = 0;
        timer.Start();

        Team1WinnerText.enabled = false;
        Team1WinnerSText.enabled = false;
        Team2WinnerText.enabled = false;
        Team2WinnerSText.enabled = false;

        EndGame = false;

        if (!DebugTest) {
            _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
            ParshMenu();
        }

    }

    void ParshMenu() {
        List<GameObject> TeamA = new List<GameObject>();
        List<GameObject> TeamB = new List<GameObject>();
        //these are temp to determin the slot in in the roomba to spawn
        //here is where it is used:   RoombaA.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.position
        int TeamAPlayerNum, TeamBPlayerNum;
        //set to one because the first ones name is "Team A player 1" rather than ""Team A player 0"
        TeamAPlayerNum = TeamBPlayerNum = 1;
        //this loops though all the players in the game. This is set in the main menu
        for (int i = 0; i < _AllGameLogic.Players.Count; i++)
        {
            //load a temp gameobject that i change
            GameObject TempPlayer = PlayerPrefabTeamA;
            if (_AllGameLogic.Players[i].playerInfo.Team > 0)
            {
                if (_AllGameLogic.Players[i].playerInfo.Team == 2)
                {
                    TempPlayer = PlayerPrefabTeamB;
                }
                // make the PlayerControlls the same as the one in the menu
                TempPlayer.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                TempPlayer.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;
            }
            //check to see the team of the player
            if (_AllGameLogic.Players[i].playerInfo.Team == 1)
            {
                //spawn the TempPlayer in the right spot and add it to the Team A's player list
                TeamA.Add(Instantiate(TempPlayer, ToasterTeam1.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.position, ToasterTeam1.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum).transform.rotation));
                //change its name and layer info
                TeamA[TeamA.Count - 1].transform.parent = ToasterTeam1.transform.FindChild("Players").FindChild("Player " + TeamAPlayerNum);
                TeamA[TeamA.Count - 1].name = "Player";
                TeamA[TeamA.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                TeamA[TeamA.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                TeamA[TeamA.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                TeamAPlayerNum++;
            }
            else if (_AllGameLogic.Players[i].playerInfo.Team == 2)
            {
                TeamB.Add(Instantiate(TempPlayer, ToasterTeam2.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.position, ToasterTeam2.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum).transform.rotation));
                TeamB[TeamB.Count - 1].transform.parent = ToasterTeam2.transform.FindChild("Players").FindChild("Player " + TeamBPlayerNum);
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

        TeamAPlayers.GetComponent<TosterPlayerManager>().Players = TeamA;
        TeamBPlayers.GetComponent<TosterPlayerManager>().Players = TeamB;

        //print(_AllGameLogic.Players[2] == null);
        for (int i = _AllGameLogic.Players.Count - 1; i > 0; i--)
        {
            if (_AllGameLogic.Players[i] == null)
            {
                _AllGameLogic.Players.RemoveAt(i);
            }
        }
    }

    // Update is called once per frame
    void Update() {
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
        }
        else
        {
            Bread1.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            Destroy(Bread1.gameObject, 5);
            Bread2.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            Destroy(Bread2.gameObject, 5);
        }
    }
}
