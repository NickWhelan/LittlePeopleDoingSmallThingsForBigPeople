using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {
    public AllGameLogic gamelogic;
    public GameObject GreenBox, Stage1Pos, Stage2Pos;
    public Camera cam;
    bool TeamA, TeamB;
    bool stage2;
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
                    stage2 = true;
                    //SceneManager.LoadScene("Roomba", LoadSceneMode.Single);
                }
                if (player.ButtonBPressed && stage2) {
                    stage2 = false;
                }
                
            }
        }
    }

    void Update() {
        if (!stage2 && cam.transform.position != Stage1Pos.transform.position) {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Stage1Pos.transform.position, 0.1f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Stage1Pos.transform.rotation, 0.1f);
        }
        else if (stage2 && cam.transform.position != Stage2Pos.transform.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Stage2Pos.transform.position, 0.1f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Stage2Pos.transform.rotation, 0.1f);
        }
    }
}
