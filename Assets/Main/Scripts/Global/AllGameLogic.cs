using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGameLogic : MonoBehaviour {
    [SerializeField]
    public List<PlayerControlls> Players;

    public enum Scene {
        Splash,
        Menu,
        Roomba,
        TapeDeck
    }
    public Scene CurrentScene = Scene.Menu;
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
        if (Input.GetKey(KeyCode.L)) {
            if (CurrentScene == Scene.Menu) {
                Application.LoadLevel("Roomba");
            }
        }		
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
