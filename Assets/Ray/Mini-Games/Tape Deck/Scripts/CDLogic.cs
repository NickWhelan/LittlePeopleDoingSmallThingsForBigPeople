using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDLogic : MonoBehaviour
{ 
    Rigidbody rigidbody;
    
    [Range(1, 100)]
    public float torqueVal;

    [SerializeField]
    GameObject[] arrayOfMBoxes;

    public GameObject[] trackSwapPoints;


    // Use this for initialization
    void Start()
    {
        arrayOfMBoxes = GameObject.FindGameObjectsWithTag("Music Box");
        
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }

    public void spinUpDisc()
    {
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }

    //Collision
    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boop");
            collision.gameObject.transform.parent = transform;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
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
