﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapeDeckGameLogic : MonoBehaviour {
    public GameObject playerPrefab;
    public bool DebugTest = false;

    public GameObject TapeDeckA, TapeDeckB;

    public int playerCount;

    public Text scoreTextOne, scoreTextTwo;
    float teamAScore, teamBScore;

    List<GameObject> playerList, TeamAList, TeamBList;
    
    AllGameLogic _AllGameLogic;

    public Image[] UIImages;

    private void Awake()
    {
        Color leftCol = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
        Color rightCol = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

        for(int i = 0; i < UIImages.Length; i++)
        {
            UIImages[i].material.SetColor("_LeftColour", leftCol);
            UIImages[i].material.SetColor("_RightColour", rightCol);
        }
    }

    // Use this for initialization
    void Start()
    {
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
                    playerList.Add(tempPlayer);
                    TeamAList[TeamAList.Count - 1].name = "Player";
                    TeamAList[TeamAList.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamAList[TeamAList.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamAPlayerNum++;
                    playerCount++;
                }
                else if(_AllGameLogic.Players[i].playerInfo.Team == 2)
                {
                    TeamBList.Add(Instantiate(tempPlayer, TapeDeckA.transform.FindChild("Player " + playerCount + " Spawn").transform.position, TapeDeckA.transform.FindChild("Player " + playerCount + " Spawn").transform.rotation));
                    playerList.Add(tempPlayer);
                    TeamBList[TeamBList.Count - 1].name = "Player";
                    TeamBList[TeamBList.Count - 1].tag = "Player Team " + _AllGameLogic.Players[i].playerInfo.Team;
                    TeamBList[TeamBList.Count - 1].layer = LayerMask.NameToLayer("Team 1");
                    TeamBPlayerNum++;
                    playerCount++;
                }
            }

            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                _AllGameLogic.Players[i] = playerList[i].GetComponent<PlayerControlls>();
            }
        }

    }
}
