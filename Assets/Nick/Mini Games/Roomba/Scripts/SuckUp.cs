using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckUp : MonoBehaviour {
    public bool isTriggered;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "dirt" && !isTriggered)
        {
            isTriggered = true;
            Destroy(Other.gameObject);
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "dirt")
        {
            isTriggered = false;
        }
    }
}
