using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    public AllGameLogic AllPlayers;
    public GameObject Room;
    

    [Range(1,2)]
    public int Team;

    int playernum;
    bool isPlugged;
    Material BaseMaterial;
    // Use this for initialization
    void Start () {
        playernum = -1;
        Room.GetComponent<Renderer>().material.color = Color.gray;
        BaseMaterial = Room.GetComponent<Renderer>().material;
    }
    void OnTriggerEnter(Collider Other)
    {

        if (!isPlugged)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = Team;
            AllPlayers.addPlayer(Other.GetComponent<PlayerControlls>());
            print(Other.GetComponent<PlayerControlls>().playerInfo.Team);
            Room.GetComponent<Renderer>().material = Other.GetComponent<Renderer>().material;
            isPlugged = true;
            playernum = Other.GetComponent<PlayerControlls>().PlayerNum;
        }

    }
    void OnTriggerExit(Collider Other)
    {
        if (isPlugged &&  playernum == Other.GetComponent<PlayerControlls>().PlayerNum)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = -1;
            AllPlayers.RemovePlayer(Other.GetComponent<PlayerControlls>());
            Room.GetComponent<Renderer>().material = BaseMaterial;
            isPlugged = false;
            playernum = -1;
        }
    }
}
