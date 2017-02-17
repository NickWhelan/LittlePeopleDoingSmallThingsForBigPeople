using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllGameLogic : MonoBehaviour {
    [SerializeField]
    public List<PlayerControlls> Players;

    public string CurrentScene = "Menu";
    // Use this for initialization
    void Start () {
        Players = new List<PlayerControlls>();
	}
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update () {
        
	}

    void OnLevelWasLoaded()
    {
        CurrentScene = Application.loadedLevelName;
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
