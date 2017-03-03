using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {
    public AllGameLogic gamelogic;
    public Transform Stage1Pos, Stage2Pos;
    public Camera cam;

    public  GUIEffects uiEffects;
    public Text m_StartText, m_StartTextShadow;

    bool TeamA, TeamB;
    bool stage2,loadGame;
    float activePlayers;

    void Awake() {
        stage2 = loadGame = TeamA = TeamB = false;
        m_StartTextShadow.color = m_StartText.color = new Vector4(0,0,0,0);
    }

    void Start()
    {
        

        uiEffects.m_Text = m_StartText;
        uiEffects.m_TextShadow = m_StartTextShadow;

        uiEffects.m_StartTrans.transform.position = m_StartText.transform.position;
        uiEffects.m_EndTrans.transform.position = m_StartText.transform.position;
        uiEffects.m_EndTrans.transform.localScale = new Vector3(uiEffects.m_EndTrans.transform.localScale.x*1.2f, uiEffects.m_EndTrans.transform.localScale.y * 1.2f, uiEffects.m_EndTrans.transform.localScale.z);
    }

        void FixedUpdate() {
        if (!TeamA || !TeamB)
        {
            activePlayers = 0;
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
            if (!stage2) {
                m_StartText.color = Color.white;
                m_StartTextShadow.color = Color.black;
                uiEffects.ScaleText();
            }

            foreach (PlayerControlls player in gamelogic.Players)
            {
                if (player.ButtonStartPressed)
                {
                    if (!stage2)
                    {
                        stage2 = true;
                        m_StartTextShadow.color = m_StartText.color = new Vector4(0, 0, 0, 0);
                    }
                }
                else if (player.ButtonBPressed && stage2) {
                    stage2 = false;

                }
            }
            if (gamelogic.Players[0].ButtonRBPressed && stage2 && !loadGame)
            {
                loadGame = true;
                SceneManager.LoadScene("Roomba", LoadSceneMode.Single);
            }
        }
    }

    void Update() {

        if (!stage2 && cam.transform.position != Stage1Pos.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Stage1Pos.position, 0.1f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Stage1Pos.rotation, 0.1f);

        }
        else if (stage2 && cam.transform.position != Stage2Pos.position)
        {

            cam.transform.position = Vector3.Lerp(cam.transform.position, Stage2Pos.position, 0.1f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Stage2Pos.rotation, 0.1f);
        }
    }
}
