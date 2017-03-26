using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMovementBox : MonoBehaviour {
    public Light effectLight;
    public List<GameObject> PlayersOnTeam;
    public int playersInside;
    public bool isTriggered;
    // Use this for initialization
    void Start () {
        isTriggered = false;
        effectLight.enabled = false;
    }

    void OnTriggerEnter(Collider Other){
        if (PlayersOnTeam.Contains(Other.gameObject)) {
            playersInside++;
            isTriggered = true;
            effectLight.enabled = true;
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (PlayersOnTeam.Contains(Other.gameObject))
        {
            playersInside--;
            if (playersInside == 0)
            {
                isTriggered = false;
                effectLight.enabled = false;
            }
        }
    }
}
