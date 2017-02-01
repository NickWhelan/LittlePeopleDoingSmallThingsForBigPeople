using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDLogic : MonoBehaviour {

    Rigidbody rigidbody;

    [Range(1, 25)]
    public float torqueVal;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }


    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boop");
            //collision.gameObject.transform.parent = transform;
            //collision.gameObject.transform.position = transform.localPosition + new Vector3(0, 1, 0);
        }
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }

        void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }
}
