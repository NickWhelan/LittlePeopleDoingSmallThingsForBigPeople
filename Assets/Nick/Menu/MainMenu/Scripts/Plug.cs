using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    public Light[] lights;

    [Range(1,2)]
    public int Team;

    int playernum;
    public bool isPlugged;
    Color BaseColor;
    // Use this for initialization
    void Start () {
        playernum = -1;
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }
    void OnTriggerEnter(Collider Other)
    {

        if (!isPlugged)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = Team;

            isPlugged = true;
            playernum = Other.GetComponent<PlayerControlls>().PlayerNum;
            foreach (Light light in lights) {
                light.enabled = true;
                Changecolor(Other.GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);
            }

        }

    }
    void OnTriggerExit(Collider Other)
    {
        if (isPlugged &&  playernum == Other.GetComponent<PlayerControlls>().PlayerNum)
        {
            Other.GetComponent<PlayerControlls>().playerInfo.Team = -1;
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            isPlugged = false;
            playernum = -1;
        }
    }

    void Changecolor(Player.Character Character) {
        /*
        Astronaut=1,
        BigBusinessOwner=2,
        Cowboy=3,
        Ninja=4,
        Mafioso=5,
        */
        switch (Character) {
            case Player.Character.Mafioso: 
            case Player.Character.Astronaut:
                foreach (Light light in lights)
                {
                    light.color = Color.gray;
                }
                break;
            case Player.Character.BigBusinessOwner:
                foreach (Light light in lights)
                {
                    light.color = Color.white;
                }
                break;
            case Player.Character.Cowboy:
                foreach (Light light in lights)
                {
                    light.color = Color.green;
                }
                break;
            case Player.Character.Ninja:
                foreach (Light light in lights)
                {
                    light.color = Color.red;
                }
                break;
        }
    }
}
