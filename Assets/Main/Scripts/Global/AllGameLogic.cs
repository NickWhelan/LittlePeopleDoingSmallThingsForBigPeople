using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllGameLogic : MonoBehaviour {
    [SerializeField]
    public List<PlayerControlls> Players;
    public List<string> MiniGamePlayList;
    public int CurrentGame;

    public string CurrentScene = "Menu";
    // Use this for initialization
    void Start () {
       
    }
    void Awake()
    {
        CurrentGame = 0;
        MiniGamePlayList.Add("Menu");
        Players = new List<PlayerControlls>();
        SceneManager.sceneLoaded += SetactiveScene;
        
    }

    void SetactiveScene(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = SceneManager.GetActiveScene().name;
    }


    public void addPlayer(PlayerControlls newPlayer)
    {
        Players.Add(newPlayer);
    }

    public void RemovePlayer(PlayerControlls oldPlayer)
    {
        Players.Remove(oldPlayer);
    }

    public void addGame(string GamesName)
    {
        MiniGamePlayList.Add(GamesName);
    }

    public void RemovePlayer(string GamesName)
    {
        MiniGamePlayList.Remove(GamesName);
    }
}
