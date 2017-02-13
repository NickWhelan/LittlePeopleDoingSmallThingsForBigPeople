using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Character
    {
        Astronaut=0,
        BigBusinessOwner=1,
        Cowboy=2,
        Ninja=3,
        Mafioso=4,
        Mathematician=5,
        RockSinger=6,
        StrangeDoctor=7,
        Survivalist=8,
        WaitStaff=9,
        Budgie = 10,
    };
    public Character CurrentCharacter;
    public int Team;
    public int PlayerNum;

    // Use this for initialization
    public Player(int _PlayerNum)
    {
        Team = -1;
        PlayerNum = _PlayerNum;
        CurrentCharacter = (Character) PlayerNum;
    }

    public void ChangeCharacter(int CharacterId) {
        if (CharacterId > 10)
        {
            CharacterId = 0;
        }
        else if (CharacterId < 0) {
            CharacterId = 0;
        }
        CurrentCharacter = (Character) CharacterId;
    }
}
