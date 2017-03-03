using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public enum Character
    {
        Astronaut=1,
        BigBusinessOwner=2,
        Cowboy=3,
        Ninja=4,
        Mafioso=5,
        Mathematician=6,
        RockSinger=7,
        StrangeDoctor=8,
        Survivalist=9,
        WaitStaff=10,
        Budgie = 11,
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
