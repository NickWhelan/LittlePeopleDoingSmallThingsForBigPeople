using UnityEngine;
using UnityEngine.UI;

public class CDLogic : MonoBehaviour
{
    Rigidbody rigidbody;

    Transform origParent;

    RigidbodyConstraints otherObjectConstraints;

    [Range(1, 100)]
    public float torqueVal;

    MusicController musicController;
    float pitchVol;
    public Text pitchText;
    // Use this for initialization
    void Start()
    {
        if (tag == "Disc")
        {
            musicController = GameObject.FindGameObjectWithTag("Music Control").GetComponent<MusicController>();
        }
        else
        {
            musicController = GameObject.FindGameObjectWithTag("Music Control 2").GetComponent<MusicController>();
        }
        musicController.UpdatePitch(0);
        Physics.IgnoreCollision(GetComponent<MeshCollider>(), GetComponent<MeshCollider>(), true);
        rigidbody = GetComponent<Rigidbody>();
        
    }

    /// <summary>
    /// Accepts a linear velocity from the player currently on the disc
    /// Using this it will convert that linear velocity into an angular velocity
    /// that will in turn be used to rotate the disc
    /// </summary>
    /// <param name="linearVelocity"></param>
    public void spinUpDisc(Vector3 linearVelocity)
    {
        pitchVol = linearVelocity.x;
        pitchText.text = System.Math.Round(pitchVol * 100, 0).ToString();
        musicController.UpdatePitch(Mathf.Clamp((float)System.Math.Round(pitchVol, 2), 0, 1));
        rigidbody.transform.Rotate(new Vector3(0, -linearVelocity.x * 5, 0));
    }

    //Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            

            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 14.25f, other.gameObject.transform.position.z);
            if (other.gameObject.GetComponent<PlayerControlls>().ButtonAPressed && !other.gameObject.GetComponent<PlayerControlls>().ButtonRBPressed)
            {
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                spinUpDisc(other.GetComponent<PlayerControlls>().movement);
            }

            if (!other.gameObject.GetComponent<PlayerControlls>().ButtonAPressed)
            {
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                spinUpDisc(other.GetComponent<PlayerControlls>().movement);
            }

        }

    }
}
