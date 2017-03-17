using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    public GameObject Room;
 
    [Range(1,2)]
    public int Team;

    int playernum;
    public bool isPlugged;
    Color BaseColor;
    // Use this for initialization
    void Start () {
        playernum = -1;
        Room.GetComponent<Renderer>().material.color = Color.gray;
        BaseColor = Room.GetComponent<Renderer>().material.color;
    }
    void OnTriggerEnter(Collider Other)
    {

        if (!isPlugged)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = Team;
            Changecolor(Other.GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);
            isPlugged = true;
            playernum = Other.GetComponent<PlayerControlls>().PlayerNum;
        }

    }
    void OnTriggerExit(Collider Other)
    {
        if (isPlugged &&  playernum == Other.GetComponent<PlayerControlls>().PlayerNum)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = -1;
            Room.GetComponent<Renderer>().material.color = BaseColor;
            isPlugged = false;
            playernum = -1;
        }
    }

    void Changecolor(Player.Character Character) {

        switch (Character) {
            case Player.Character.Astronaut:
                Room.GetComponent<Renderer>().material.color = Color.black;
                break;
            case Player.Character.BigBusinessOwner:
                Room.GetComponent<Renderer>().material.color = Color.white;
                break;
            case Player.Character.Cowboy:
                Room.GetComponent<Renderer>().material.color = Color.green;
                break;
            case Player.Character.Ninja:
                Room.GetComponent<Renderer>().material.color = Color.red;
                break;
            case Player.Character.Mafioso:
                Room.GetComponent<Renderer>().material.color = Color.black;
                break;

        }

        
    }
}
