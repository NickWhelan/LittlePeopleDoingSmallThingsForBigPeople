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

    public Plug[] Team1plugs, Team2plugs;
    bool Team1Ready, Team2Ready;
    int TeamAplayers, TeamBplayers;

    bool stage2,loadGame;
    float activePlayers;

    void Awake() {
        stage2 = loadGame = false;
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

    void FixedUpdate()
    {
        TeamReady();
        if (Team1Ready && Team2Ready)
        {
            if (!stage2)
            {
                m_StartText.color = Color.white;
                m_StartTextShadow.color = Color.black;
                uiEffects.ScaleText();
            }

            if (gamelogic.Players[0].ButtonStartPressed)
            {
                if (!stage2)
                {
                    stage2 = true;
                    m_StartTextShadow.color = m_StartText.color = new Vector4(0, 0, 0, 0);
                    foreach (PlayerControlls player in gamelogic.Players)
                    {
                        player.Frozen = true;
                    }
                }
            }
            else if (stage2)
            {
                foreach (PlayerControlls player in gamelogic.Players)
                {
                    if (player.ButtonBPressed)
                    {
                        stage2 = false;
                        foreach (PlayerControlls player2 in gamelogic.Players)
                        {
                            player2.Frozen = false;
                        }
                    }
                }
                if (gamelogic.Players[0].ButtonRBPressed && !loadGame)
                {
                    loadGame = true;
                    SceneManager.LoadScene("Roomba", LoadSceneMode.Single);
                }
            }
        }
        else
        {
            m_StartTextShadow.color = m_StartText.color = new Vector4(0, 0, 0, 0);
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

    void TeamReady() {
        TeamAplayers = TeamBplayers = 0;
        foreach (Plug plug in Team1plugs)
        {
            if (plug.isPlugged)
            {
                TeamAplayers++;

            }
        }
        foreach (Plug plug in Team2plugs)
        {
            if (plug.isPlugged)
            {
                TeamBplayers++;
            }
        }
        if (TeamAplayers > 0)
        {
            Team1Ready = true;
        }
        else
        {
            Team1Ready = false;
        }
        if (TeamBplayers > 0)
        {
            Team2Ready = true;
        }
        else
        {
            Team2Ready = false;
        }

    }
}
