using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {
    public AllGameLogic gamelogic;
    public GameObject GreenBox;
    bool TeamA, TeamB;
    float activePlayers;

    void Awake() {
        GreenBox.active = false;
    }

    void FixedUpdate() {
        if (!TeamA || !TeamB)
        {
            activePlayers = 0;
            GreenBox.active = false;
            foreach (PlayerControlls player in gamelogic.Players)
            {
                if (player.playerInfo.Team == 1)
                {
                    TeamA = true;
                    activePlayers++;
                }
                else if (player.playerInfo.Team == 2)
                {
                    TeamB = true;
                    activePlayers++;
                }
            }
        }
        else if (TeamA && TeamB)
        {
            GreenBox.active = true;
            foreach (PlayerControlls player in gamelogic.Players)
            {
                if (player.ButtonAPressed)
                {
                    SceneManager.LoadScene("Roomba", LoadSceneMode.Single);
                }
            }
        }
    }
}
