using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMovementBox : MonoBehaviour {

    public List<GameObject> PlayersOnTeam;
    int playersInside;
    public bool isTriggered;
    // Use this for initialization
    void Start () {
        isTriggered = false;
	}

    void OnTriggerEnter(Collider Other){
        if (PlayersOnTeam.Contains(Other.gameObject)) {
            playersInside++;
            isTriggered = true;
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (PlayersOnTeam.Contains(Other.gameObject))
        {
            playersInside--;
            if (playersInside == 0)
                isTriggered = false;
        }
    }
}
