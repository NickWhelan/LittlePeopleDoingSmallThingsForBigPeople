using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audSource;
    public AudioClip audClip;

    private bool isCollidingWithPlayer = false;

    private float pitchInRate = 1;

    private float pitchOutRate = .2f;

   // public GameObject swapPoint;
  
    public bool isActive = false;

    GameObject disc;

    // Use this for initialization
    void Start()
    {
        disc = GameObject.FindGameObjectWithTag("Disc");
        audSource = GameObject.FindGameObjectWithTag("Swapper").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audSource.pitch > 0 && !isCollidingWithPlayer)
        {
            audSource.pitch -= Time.deltaTime * pitchOutRate;
        }
        else if (audSource.pitch < 0)
        {
            audSource.pitch = 0;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = true;
            isActive = true;
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if(c.gameObject.name.Contains("Player") && audSource.pitch <= 1 && c.gameObject.GetComponent<PlayerControlls>().ButtonAPressed)
        {
            audSource.pitch += Time.deltaTime * pitchInRate;
        }
    }

    public void PlaySong()
    {
        audSource.Play();
    }

    public void StopSong()
    {
        audSource.Stop();
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            // Debug.Log("Stop Playing: " + audSource.clip.name);
            isCollidingWithPlayer = false;
            //audSource.Stop();
            isActive = false;
        }
    }
}
