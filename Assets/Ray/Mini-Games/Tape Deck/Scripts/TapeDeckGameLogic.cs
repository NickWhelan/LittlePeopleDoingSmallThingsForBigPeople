
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TapeDeckGameLogic : MonoBehaviour
{
    public GameObject playerPrefab;
    public bool DebugTest = false;

    public GameObject TapeDeckA, TapeDeckB;

    public int playerCount;

    public int timerLength;

    public Text timerText,
        Team1WinnerText,
        Team1WinnerSText,
        Team2WinnerText,
        Team2WinnerSText;

    float teamAScore, teamBScore;

    Timer timer;

    List<GameObject> playerList, TeamAList, TeamBList;
    
    AllGameLogic _AllGameLogic;

    bool gameOver = false;

    public Image[] UIImages;

    //private void Awake()
    //{
    //    Color leftCol = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
    //    Color rightCol = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

    //    for(int i = 0; i < UIImages.Length; i++)
    //    {
    //        UIImages[i].material.SetColor("_LeftColour", leftCol);
    //        UIImages[i].material.SetColor("_RightColour", rightCol);
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        timer = new Timer();
        timer.isCountingDown = true;
        timer.StartTime = timerLength;
        timer.EndTime = 0;
        timer.Start();

        Team1WinnerText.enabled = false;
        Team1WinnerSText.enabled = false;
        Team2WinnerText.enabled = false;
        Team2WinnerSText.enabled = false;

        gameOver = false;

        if (!DebugTest)
        {
            int TeamAPlayerNum, TeamBPlayerNum;

            TeamAPlayerNum = TeamBPlayerNum = playerCount = 1;

            _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
            playerList = new List<GameObject>();
            TeamAList = new List<GameObject>();
            TeamBList = new List<GameObject>();

            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                GameObject tempPlayer = playerPrefab;

                if (_AllGameLogic.Players[i].playerInfo.Team > 0)
                {
                    tempPlayer.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                    tempPlayer.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;
                }
                if (_AllGameLogic.Players[i].playerInfo.Team == 1)
                {
                    TeamAList.Add(Instantiate(tempPlayer, TapeDeckA.transform.FindChild("Player " + playerCount + " Spawn").transform.position, TapeDeckA.transform.FindChild("Player " + playerCount + " Spawn").transform.rotation));
                    //playerList.Add(tempPlayer);
                    TeamAList[TeamAList.Count - 1].name = "Player " + playerCount;
                    TeamAList[TeamAList.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamAList[TeamAList.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamAPlayerNum++;
                    playerCount++;
                }
                else if (_AllGameLogic.Players[i].playerInfo.Team == 2)
                {
                    TeamBList.Add(Instantiate(tempPlayer, TapeDeckB.transform.FindChild("Player " + playerCount + " Spawn").transform.position, TapeDeckB.transform.FindChild("Player " + playerCount + " Spawn").transform.rotation));
                    //playerList.Add(tempPlayer);
                    TeamBList[TeamBList.Count - 1].name = "Player " + playerCount;
                    TeamBList[TeamBList.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamBList[TeamBList.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamBPlayerNum++;
                    playerCount++;
                }
            }

            //loop through the AllGameLogic list of players and set them to the players in the scene
            bool found;
            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                found = false;
                //only loop if the player player isn't found and is on the correcret team
                //I loop though TeamA and and TeamB sepratly because i dont have a single player list for the scene
                for (int j = 0; j < TeamAList.Count && !found && _AllGameLogic.Players[i].playerInfo.Team == 1; j++)
                {
                    //check to see if the player numbers are equal
                    //if they are set the gobal player ref to be the one in the scene
                    //and set found to true so it breaks from the loop and skips the next one. 
                    if (TeamAList[j].GetComponent<PlayerControlls>().PlayerNum == _AllGameLogic.Players[i].PlayerNum)
                    {
                        _AllGameLogic.Players[i] = TeamAList[j].GetComponent<PlayerControlls>();
                        found = true;
                    }
                }
                for (int j = 0; j < TeamBList.Count && !found && _AllGameLogic.Players[i].playerInfo.Team == 2; j++)
                {
                    if (TeamBList[j].GetComponent<PlayerControlls>().PlayerNum == _AllGameLogic.Players[i].PlayerNum)
                    {
                        _AllGameLogic.Players[i] = TeamBList[j].GetComponent<PlayerControlls>();
                        found = true;
                    }
                }
            }
            //print(_AllGameLogic.Players[2] == null);
            for (int i = _AllGameLogic.Players.Count - 1; i > 0; i--)
            {
                if (_AllGameLogic.Players[i] == null)
                {
                    _AllGameLogic.Players.RemoveAt(i);
                }
            }
        }

    }

   
    void Update()
    {

        if (!timer.isTimeUp)
        {
            timer.Update();
            if (timer.CurrentTime < 10)
            {
                timerText.text = string.Format("{0:0.00}", timer.CurrentTime);
            }
            else
            {
                timerText.text = ((int)timer.CurrentTime).ToString();
            }
            if (TapeDeckA.GetComponent<AIHumanBehaviour>().score > TapeDeckB.GetComponent<AIHumanBehaviour>().score)
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Winner";
                Team2WinnerSText.text = Team2WinnerText.text = "Loser";
            }
            else if (TapeDeckA.GetComponent<AIHumanBehaviour>().score < TapeDeckB.GetComponent<AIHumanBehaviour>().score)
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Loser";
                Team2WinnerSText.text = Team2WinnerText.text = "Winner";
            }
            else
            {
                Team1WinnerSText.text = Team1WinnerText.text = "Tie";
                Team2WinnerSText.text = Team2WinnerText.text = "Tie";
            }
        }
        else if (!gameOver)
        {
            gameOver = true;
            Team1WinnerSText.enabled = Team1WinnerText.enabled = true;
            Team2WinnerSText.enabled = Team2WinnerText.enabled = true;
            TapeDeckA.GetComponent<AIHumanBehaviour>().GameOver = true;
            TapeDeckB.GetComponent<AIHumanBehaviour>().GameOver = true;

        }
        else if(gameOver)
        {
            if (_AllGameLogic.Players[0].ButtonStartPressed)
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            else if (_AllGameLogic.Players[0].ButtonSelectPressed)
            {
                _AllGameLogic.CurrentGame++;
                if (_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame] != null)
                {
                    SceneManager.LoadScene(_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame], LoadSceneMode.Single);
                }
                else
                {
                    _AllGameLogic.CurrentGame = 0;
                    SceneManager.LoadScene(_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame], LoadSceneMode.Single);

                }
            }
        }
    }

}
