using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private AudioSource audSource;

    private bool isFading = true;
    private bool isCollidingWithPlayer = false;

    private float pitchInRate = .75f;

    private float pitchOutRate = .005f;

    public GameObject volumeSlider;

    GameObject disc;
    // Use this for initialization
    void Start()
    {
        disc = GameObject.FindGameObjectWithTag("Disc");
        audSource = GetComponent<AudioSource>();
        audSource.volume = volumeSlider.GetComponent<VolumeSlider>().volumeLevel;
        audSource.pitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        audSource.volume = volumeSlider.GetComponent<VolumeSlider>().volumeLevel;

        if (audSource.pitch > 0 && !isCollidingWithPlayer)
        {
            audSource.pitch -= pitchOutRate;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("Start Playing: " + audSource.clip.name);
            audSource.Play();
            isCollidingWithPlayer = true;
        }

    }

    private void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player")
        {

            if (isCollidingWithPlayer && c.gameObject.GetComponent<PlayerControlls>().inputPressed)
            {
                disc.GetComponent<CDLogic>().spinUpDisc();   
                //Increase the pitch of the song with each button press to a max of one
                //0 means the song is stopped
                //1 is normal speed
                //2 is double speed
                //3 is triple speed
                if (audSource.pitch < 1.0f)
                {
                    audSource.pitch += Time.deltaTime * pitchInRate;
                }
                else
                {
                    audSource.pitch = 1.0f;
                }
            }
            else
            {
                //Slow the song down, reduce volume when button isn't being pressed.
                if (audSource.pitch > 0)
                {
                    audSource.pitch -= pitchOutRate;
                }
            }
        }
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("Stop Playing: " + audSource.clip.name);
            isCollidingWithPlayer = false;
        }
    }
}
