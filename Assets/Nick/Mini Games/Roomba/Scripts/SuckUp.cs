using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckUp : MonoBehaviour {
    public bool isTriggered;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "SuckUp" && !isTriggered)
        {
            isTriggered = true;
            Destroy(Other.gameObject);
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "SuckUp")
        {
            isTriggered = false;
        }
    }
}
