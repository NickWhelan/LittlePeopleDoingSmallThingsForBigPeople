using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private AudioSource audSource;
    public bool isFading = true;
    public bool isCollidingWithPlayer = false;

    [Range(0, 2), SerializeField]
    private float fadeInRate = 1;

    [Range(0, 2), SerializeField]
    private float fadeOutRate = .75f;

    [Range(0, 2), SerializeField]
    private float pitchInRate = .75f;

    [Range(0, 2), SerializeField]
    private float pitchOutRate = .50f;

    // Use this for initialization
    void Start () {
        audSource = GetComponent<AudioSource>();
        audSource.volume = 0;
        audSource.pitch = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (audSource.volume > 0.0f && isFading)
        {
            audSource.volume -= Time.deltaTime * fadeOutRate;
            if(audSource.pitch >= 1)
            {
                audSource.pitch -= pitchOutRate;
            }
            //Debug.Log("Fading Out: " + audSource.volume);
        }
	}

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        { 
            Debug.Log("Start Playing: " + audSource.clip.name);
            audSource.Play();
            isFading = false;
            isCollidingWithPlayer = true;
        }
        
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player")
        {
            //TODO: Update this to have input with new player controls
            //c.tag.
            if (isCollidingWithPlayer && c.gameObject.GetComponent<PlayerControlls>().inputPressed)
            {
                Debug.Log("Getting Input");
                //Increase audio volume with each button press
                audSource.volume += Time.deltaTime * fadeInRate;

                //Increase the pitch of the song with each button press to a max of one
                //0 means the song is stopped
                //1 is normal speed
                //2 is double speed
                //3 is triple speed
                if (audSource.pitch <= 1.0f) {
                    audSource.pitch += Time.deltaTime * pitchInRate;
                }
                else { audSource.pitch = 1.0f; }
            }
            else
            {
                //Slow the song down, reduce volume when button isn't being pressed.
                if(audSource.pitch > 0)
                {
                    audSource.pitch -= 0.001f;
                    audSource.volume -= 0.001f;
                }
            }
            //Debug.Log("Fading In: " + audSource.volume);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("Stop Playing: " + audSource.clip.name);
            isFading = true;
            isCollidingWithPlayer = false;
        }
    }
}
