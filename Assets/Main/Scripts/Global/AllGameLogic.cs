using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllGameLogic : MonoBehaviour {
    [SerializeField]
    public List<PlayerControlls> Players;

    public string CurrentScene = "Menu";
    // Use this for initialization
    void Start () {
    }
    void Awake()
    {
        Players = new List<PlayerControlls>();
        SceneManager.sceneLoaded += SetactiveScene;
        DontDestroyOnLoad(this);
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
}
