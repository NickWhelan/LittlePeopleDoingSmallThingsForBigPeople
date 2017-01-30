using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckUp : MonoBehaviour {
    public bool isTriggered;
    public int Score;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "trash")
        {
            isTriggered = true;
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "trash")
        {
            isTriggered = false;
        }
    }
}
