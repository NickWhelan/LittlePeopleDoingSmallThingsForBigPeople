using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {
    AllGameLogic _AllGameLogic;
    public Transform Stage1Pos, Stage2Pos;
    public Camera cam;

    public  GUIEffects uiEffects;
    public Text m_StartText, m_StartTextShadow;


    public GameObject PlugPrefab, AllGameLogicPrefab, CablesPrefab;
    public List<GameObject> Plugs; 
    public Plug[] Team1plugs, Team2plugs;
    public List<GameObject> Cables;


    bool Team1Ready, Team2Ready;
    int TeamAplayers, TeamBplayers;

    List<bool> PlayersReady;

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
        uiEffects.m_EndTrans.transform.localScale = new Vector3(uiEffects.m_EndTrans.transform.localScale.x * 2f, uiEffects.m_EndTrans.transform.localScale.y * 2f, uiEffects.m_EndTrans.transform.localScale.z);
        uiEffects.m_EndTransVaule = uiEffects.m_EndTrans;


        Plugs = new List<GameObject>();
        PlayersReady = new List<bool>();


        if (GameObject.Find("OverWatch"))
        {
            _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                GameObject TempPlug = PlugPrefab;
                TempPlug.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                TempPlug.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;

                PlayersReady.Add(new bool());
                PlayersReady[i] = true;
                int Playernum = TempPlug.GetComponent<PlayerControlls>().PlayerNum;
                switch (TempPlug.GetComponent<PlayerControlls>().PlayerNum)
                {
                    case 5:
                    case 1:
                        Plugs.Add(Instantiate(TempPlug, new Vector3(-9.5f, 6, 1), Quaternion.identity));
                        break;
                    case 2:
                        Plugs.Add(Instantiate(TempPlug, new Vector3(-9.5f, 2, 1), Quaternion.identity));
                        break;
                    case 3:
                        Plugs.Add(Instantiate(TempPlug, new Vector3(-9.5f, -2, 1), Quaternion.identity));
                        break;
                    case 4:
                        Plugs.Add(Instantiate(TempPlug, new Vector3(-9.5f, -6, 1), Quaternion.identity));
                        break;

                }
                _AllGameLogic.Players[i] = Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>();
            }
        }
        else
        {
            GameObject temp = Instantiate(AllGameLogicPrefab, transform);
            temp.transform.parent = null;
            temp.name = "OverWatch";
            _AllGameLogic = temp.GetComponent<AllGameLogic>();
            DontDestroyOnLoad(temp);
            for (int i = 0; i < 4; i++)
            {
                PlayersReady.Add(new bool());
                PlayersReady[i] = false;
            }

        }
        _AllGameLogic.addGame("Roomba");
        _AllGameLogic.addGame("Tape Deck");
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
                //uiEffects.ScaleText();
            }

            if (_AllGameLogic.Players[0].ButtonStartPressed)
            {
                if (!stage2)
                {
                    stage2 = true;
                    m_StartTextShadow.color = m_StartText.color = new Vector4(0, 0, 0, 0);
                    foreach (PlayerControlls player in _AllGameLogic.Players)
                    {
                        player.Frozen = true;
                    }
                }
            }
            else if (stage2)
            {
                foreach (PlayerControlls player in _AllGameLogic.Players)
                {
                    if (player.ButtonBPressed)
                    {
                        stage2 = false;
                        foreach (PlayerControlls player2 in _AllGameLogic.Players)
                        {
                            player2.Frozen = false;
                        }
                    }
                }
                if (_AllGameLogic.Players[0].ButtonRBPressed && !loadGame)
                {
                    loadGame = true;
                    _AllGameLogic.CurrentGame=1;
                    print(_AllGameLogic.CurrentGame);
                    SceneManager.LoadScene(_AllGameLogic.MiniGamePlayList[_AllGameLogic.CurrentGame], LoadSceneMode.Single);
                }
            }
        }
        else
        {
            m_StartTextShadow.color = m_StartText.color = new Vector4(0, 0, 0, 0);
        }

    }
    void Update() {

        if (!stage2) {
            if (Input.anyKeyDown && _AllGameLogic.Players.Count < 4)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button7)) && !PlayersReady[0])
                {
                    PlayersReady[0] = true;
                    Plugs.Add(Instantiate(PlugPrefab, new Vector3(10, 6, 10), Quaternion.identity));
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 1;
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                    Plugs[Plugs.Count - 1].name = "Plug 1";

                   _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

                    Rope temp_rope = Cables[0].GetComponent<Rope>();
                    temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                    temp_rope.setup();
                    temp_rope.Changecolor(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);
                }
                else if (Input.GetKeyDown(KeyCode.Return) && !PlayersReady[0])
                {
                    PlayersReady[0] = true;
                    PlugPrefab.transform.position = new Vector3(10, 6, 10);
                    Plugs.Add(Instantiate(PlugPrefab));
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 5;
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                    Plugs[Plugs.Count - 1].name = "Plug 1";
                    _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

                    Rope temp_rope = Cables[0].GetComponent<Rope>();
                    temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                    temp_rope.setup();
                    temp_rope.Changecolor(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);

                }
                else if ((Input.GetKeyDown(KeyCode.Joystick2Button7)|| Input.GetKeyDown(KeyCode.Alpha2)) && !PlayersReady[1])
                {
                    PlayersReady[1] = true;
                    Plugs.Add(Instantiate(PlugPrefab, new Vector3(10, 2, 10), Quaternion.identity));
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 2;
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                    Plugs[Plugs.Count - 1].name = "Plug 2";
                    _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

                    Rope temp_rope = Cables[1].GetComponent<Rope>();
                   temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                    temp_rope.setup();
                    temp_rope.Changecolor(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Alpha3)) && !PlayersReady[2])
                {
                    PlayersReady[2] = true;
                    Plugs.Add(Instantiate(PlugPrefab, new Vector3(10, -2, 10), Quaternion.identity));
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 3;
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                    Plugs[Plugs.Count - 1].name = "Plug 3";
                    _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

                    Rope temp_rope = Cables[2].GetComponent<Rope>();
                    temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                    temp_rope.setup();
                    temp_rope.Changecolor(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick4Button7) || Input.GetKeyDown(KeyCode.Alpha4)) && !PlayersReady[3])
                {
                    PlayersReady[3] = true;
                    Plugs.Add(Instantiate(PlugPrefab, new Vector3(10, -6, 10), Quaternion.identity));
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 4;
                    Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                    Plugs[Plugs.Count - 1].name = "Plug 4";
                    _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

                    Rope temp_rope = Cables[3].GetComponent<Rope>();
                    //temp_rope.ChangeColor(Plugs[Plugs.Count - 1].GetComponent<Renderer>().material.color);
                    temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                    temp_rope.setup();
                    temp_rope.Changecolor(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().playerInfo.CurrentCharacter);

                }

            }

        }

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
