﻿using System.Collections;
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
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            // Debug.Log("Start Playing: " + audSource.clip.name);
            //audSource.Play();
            isCollidingWithPlayer = true;
            isActive = true;
            //swapPoint.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player")
        {
            audSource.clip = audClip;
            if (isCollidingWithPlayer && c.gameObject.GetComponent<PlayerControlls>().ButtonAPressed
                || c.gameObject.GetComponent<PlayerControlls>().ButtonXPressed)
            {
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

    private void OnCollisionExit(Collision c)
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
