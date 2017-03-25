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


    public GameObject PlugPrefab, AllGameLogicPrefab;
    public List<GameObject> Plugs; 
    public Plug[] Team1plugs, Team2plugs;
    public List<GameObject> Cables;


    bool Team1Ready, Team2Ready;
    int TeamAplayers, TeamBplayers;

    public List<bool> PlayersReady;

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
        for (int i = 0; i < 4; i++)
        {
            PlayersReady.Add(new bool());
            PlayersReady[i] = false;
        }

        if (GameObject.Find("OverWatch"))
        {
            _AllGameLogic = GameObject.Find("OverWatch").GetComponent<AllGameLogic>();
            for (int i = 0; i < _AllGameLogic.Players.Count; i++)
            {
                GameObject TempPlug = PlugPrefab;
                TempPlug.GetComponent<PlayerControlls>().playerInfo = _AllGameLogic.Players[i].playerInfo;
                TempPlug.GetComponent<PlayerControlls>().PlayerNum = _AllGameLogic.Players[i].playerInfo.PlayerNum;
                
                int Playernum = TempPlug.GetComponent<PlayerControlls>().PlayerNum;
                switch (Playernum)
                {
                    case 5:
                    case 1:
                        Plugs.Add(Instantiate(PlugPrefab, new Vector3(-2, 6, 10), Quaternion.identity));
                        PlayersReady[0] = true;
                        break;
                    case 2:
                        Plugs.Add(Instantiate(PlugPrefab, new Vector3(-2, 4, 10), Quaternion.identity));
                        PlayersReady[1] = true;
                        break;
                    case 3:
                        Plugs.Add(Instantiate(PlugPrefab, new Vector3(-2, 0, 10), Quaternion.identity));
                        PlayersReady[2] = true;
                        break;
                    case 4:
                        PlayersReady[3] = true;
                        Plugs.Add(Instantiate(PlugPrefab, new Vector3(-2, -2, 10), Quaternion.identity));
                        break;   
                }
                Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = Playernum;
                Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
                Plugs[Plugs.Count - 1].name = "Plug " + Playernum;
                _AllGameLogic.Players[i] = Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>();
                Rope temp_rope;
                if (Playernum == 5)
                {
                    temp_rope = Cables[0].GetComponent<Rope>();
                }
                else {
                    temp_rope = Cables[Playernum - 1].GetComponent<Rope>();
                }
                temp_rope.PlugObj = Plugs[Plugs.Count - 1];
                temp_rope.setup();

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
    void AddPlug(int _PlayerNum) {
        if (_PlayerNum == 5)
        {
            PlayersReady[0] = true;
        }
        else
        {
            PlayersReady[_PlayerNum - 1] = true;
        }
        switch (_PlayerNum)
        {
            case 5:
            case 1:
                Plugs.Add(Instantiate(PlugPrefab, new Vector3(1, 6, 10), Quaternion.identity));
                break;
            case 2:
                Plugs.Add(Instantiate(PlugPrefab, new Vector3(1, 4, 10), Quaternion.identity));
                break;
            case 3:
                Plugs.Add(Instantiate(PlugPrefab, new Vector3(1, 2, 10), Quaternion.identity));
                break;
            case 4:
                Plugs.Add(Instantiate(PlugPrefab, new Vector3(1, 0, 10), Quaternion.identity));
                break;
        }
        Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = _PlayerNum;
        Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().Start();
        Plugs[Plugs.Count - 1].name = "Plug " + _PlayerNum;

        _AllGameLogic.addPlayer(Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>());

        Rope temp_rope = Cables[0].GetComponent<Rope>();

        if (_PlayerNum == 5)
        {
            temp_rope = Cables[0].GetComponent<Rope>();
        }
        else {
            temp_rope = Cables[_PlayerNum - 1].GetComponent<Rope>();
        }
            temp_rope.PlugObj = Plugs[Plugs.Count - 1];
            temp_rope.setup();
    }

    void Update() {

        if (!stage2) {
            if (Input.anyKeyDown)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button7)) && !PlayersReady[0])
                {
                    AddPlug(1);
                }
                else if (Input.GetKeyDown(KeyCode.Return) && !PlayersReady[0])
                {
                    AddPlug(5);
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick2Button7)|| Input.GetKeyDown(KeyCode.Alpha2)) && !PlayersReady[1])
                {
                    AddPlug(2);
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Alpha3)) && !PlayersReady[2])
                {
                    AddPlug(3);
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick4Button7) || Input.GetKeyDown(KeyCode.Alpha4)) && !PlayersReady[3])
                {
                    AddPlug(4);

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
