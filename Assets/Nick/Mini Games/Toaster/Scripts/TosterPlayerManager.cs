using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TosterPlayerManager : MonoBehaviour {
    public List<GameObject> Players;
    public Vector3 MinPlayerBounds, MaxPlayerBounds;
    // Use this for initialization
    void Start () {
		
	}

    void FixedUpdate() {
        foreach (GameObject player in Players) {
            player.transform.localPosition = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y, Mathf.Clamp(player.transform.localPosition.z, MinPlayerBounds.z, MaxPlayerBounds.z));
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
