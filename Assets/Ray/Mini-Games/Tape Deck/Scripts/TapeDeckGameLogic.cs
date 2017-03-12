using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapeDeckGameLogic : MonoBehaviour {
    public GameObject playerPrefab, spawnPoints;
    public bool DebugTest = false;

    //TODO: Make use of these text variables to display volume, pitch and the name of the song being played
    public Text songName, volumeLevel, pitchLevel;

    public int playerCount = 1;

    List<GameObject> playerList;
    
    AllGameLogic _AllGameLogic;
    
	// Use this for initialization
	void Start () {
        _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
        playerList = new List<GameObject>();

        if(!DebugTest)
        {
            for(int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                GameObject tempPlayer = playerPrefab;

                tempPlayer.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                tempPlayer.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;

                playerList.Add(Instantiate(tempPlayer, spawnPoints.transform.FindChild("Player " + playerCount + " Spawn").transform.position, spawnPoints.transform.FindChild("Player " + playerCount + " Spawn").transform.rotation));
                playerList[i].name = "Player";
                playerList[i].tag = "Player Team 1";
                playerList[i].layer = LayerMask.NameToLayer("Team 1");
                //playerList[i].transform.localScale = new Vector3(1, 1, 1);

                playerCount++;
            }

            for(int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                _AllGameLogic.Players[i] = playerList[i].GetComponent<PlayerControlls>();
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
