using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    public AllGameLogic AllPlayers;

    [Range(1,2)]
    public int Team;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider Other)
    {

        if (Team > 0)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = Team;
            AllPlayers.addPlayer(Other.GetComponent<PlayerControlls>());
            print(Other.GetComponent<PlayerControlls>().playerInfo.Team);
        }
    }
    void OnTriggerExit(Collider Other)
    {
        Other.GetComponent<PlayerControlls>().playerInfo.Team = -1;
        AllPlayers.RemovePlayer(Other.GetComponent<PlayerControlls>());
    }
}
