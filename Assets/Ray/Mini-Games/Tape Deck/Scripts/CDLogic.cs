using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDLogic : MonoBehaviour
{ 
    Rigidbody rigidbody;

    Transform origParent;

    RigidbodyConstraints otherObjectConstraints;

    [Range(1, 100)]
    public float torqueVal;


    private AudioSource audSource;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<MeshCollider>(), GetComponent<MeshCollider>(), true);
        rigidbody = GetComponent<Rigidbody>();
        audSource = GameObject.FindGameObjectWithTag("Swapper").GetComponent<AudioSource>();
    }

    /// <summary>
    /// Accepts a linear velocity from the player currently on the disc
    /// Using this it will convert that linear velocity into an angular velocity
    /// that will in turn be used to rotate the disc
    /// </summary>
    /// <param name="linearVelocity"></param>
    public void spinUpDisc(Vector3 linearVelocity)
    {
        audSource.pitch = Mathf.Clamp(linearVelocity.x, 0, 1);

        rigidbody.transform.Rotate(new Vector3(0, -linearVelocity.x, 0));
    }

    public void spinUpDisc()
    {
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }

    //Collision
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.transform.parent != null)
        {
            other.transform.parent = origParent;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player") && other.gameObject.GetComponent<PlayerControlls>().ButtonAPressed)
        {
            otherObjectConstraints = other.GetComponent<Rigidbody>().constraints;
            other.transform.parent = null;
            otherObjectConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            Debug.Log("Hello Player " + other.GetComponent<PlayerControlls>().PlayerNum);
            spinUpDisc(other.GetComponent<PlayerControlls>().movement);
        }
        else if(other.name.Contains("Player") && !other.gameObject.GetComponent<PlayerControlls>().ButtonAPressed)
        {
            otherObjectConstraints = RigidbodyConstraints.FreezeRotation;
        }
        if(other.name.Contains("Player"))
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 13.78f, other.gameObject.transform.position.z);
        }
    }
}
