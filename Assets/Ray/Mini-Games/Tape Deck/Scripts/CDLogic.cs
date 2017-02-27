using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDLogic : MonoBehaviour
{ 
    Rigidbody rigidbody;
    //Gotta convert the linear velocity into angular velocity

    [Range(1, 100)]
    public float torqueVal;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddTorque(new Vector3(0, torqueVal, 0));
    }

    /// <summary>
    /// Accepts a linear velocity from the player currently on the disc
    /// Using this it will convert that linear velocity into an angular velocity
    /// that will in turn be used to rotate the disc
    /// </summary>
    /// <param name="linearVelocity"></param>
    public void spinUpDisc(Vector3 linearVelocity)
    {
        float linVelAlongX = linearVelocity.x;
        float angAccelAlongY = 0;

        if (linearVelocity != Vector3.zero)
            angAccelAlongY = (2 * Mathf.PI) / linVelAlongX;

        angAccelAlongY = (linVelAlongX * linVelAlongX) / gameObject.transform.localScale.x;

        rigidbody.transform.Rotate(new Vector3(0, 1 * angAccelAlongY, 0));
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hello Player " + other.GetComponent<PlayerControlls>().PlayerNum);
            spinUpDisc(other.GetComponent<Rigidbody>().velocity);
        }
    }
}
