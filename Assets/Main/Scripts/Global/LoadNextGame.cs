using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextGame : MonoBehaviour {
    GameObject _AllGameLogic;
    bool LoadNewGame;
    // Use this for initialization
    void Start () {
        _AllGameLogic = GameObject.Find("OverWatch");
        LoadNewGame = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
