using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    public Transform HeldPos, DropPos;

    PlayerControlls playerControlls;

    GameObject PickedUpObject;
    Transform PickedUpObjectParent;
    public bool hitobj;

    Rigidbody rigidBody;
    // Use this for initialization
    void Start()
    {
        PickedUpObject = null;
        playerControlls = GetComponentInParent<PlayerControlls>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() && other.gameObject.layer == LayerMask.NameToLayer("Grabbable Object"))
        {
            rigidBody = other.gameObject.GetComponent<Rigidbody>();
            rigidBody.velocity = Vector3.zero;

            if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable Object")
                && PickedUpObject == null && playerControlls.ButtonRBPressed)
            {
                rigidBody.useGravity = false;
                rigidBody.isKinematic = true;
                PickUp(other.gameObject);
            }

            if (PickedUpObject != null && !playerControlls.ButtonRBPressed)
            {
                DropObject();
                rigidBody.useGravity = true;
                rigidBody.isKinematic = false;
                rigidBody = null;
            }

        }
        else if (!other.gameObject.GetComponent<Rigidbody>() && other.gameObject.layer == LayerMask.NameToLayer("Grabbable Object"))
        {
            Debug.Log("There is no rigidbody! I need a Rigidbody to grab things!");
        }
    }

    void PickUp(GameObject other)
    {
        PickedUpObject = other;
        PickedUpObjectParent = other.transform.parent;
        PickedUpObject.transform.position = HeldPos.position;
        PickedUpObject.transform.parent = transform;

    }

    void DropObject()
    {
        PickedUpObject.transform.parent = PickedUpObjectParent;
        PickedUpObject.transform.position = DropPos.position;
        PickedUpObject = null;
    }
}
