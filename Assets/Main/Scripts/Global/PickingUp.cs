using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    public Transform HeldPos,DropPos;

    PlayerControlls playerControlls;
 
    GameObject PickedUpObject;
    Transform PickedUpObjectParent;
    public bool hitobj;
    // Use this for initialization
    void Start()
    {
        PickedUpObject = null;
        playerControlls =  GetComponentInParent<PlayerControlls>();
       //amIGrabbing = false;
    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        print(other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable Object")
            && PickedUpObject == null && playerControlls.ButtonRBPressed)
        {
            PickUp(other.gameObject);
        }

        if (PickedUpObject != null && !playerControlls.ButtonRBPressed)
        {
            DropObject();
        }
    }
    void PickUp(GameObject other) {
        PickedUpObject = Instantiate(other,gameObject.transform);
        PickedUpObject.name = other.name;
        PickedUpObjectParent = other.transform.parent;
        Destroy(other, 0);
        PickedUpObject.transform.position = HeldPos.position;

    }
    void DropObject() {
        GameObject temp = Instantiate(PickedUpObject, DropPos);
        temp.transform.parent = PickedUpObjectParent;
        temp.name = PickedUpObject.name;
        Destroy(PickedUpObject);
        PickedUpObject = null;
    }
}
