using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private AudioSource audSource;
    public bool isFading = true;
    public GameObject[] players;
    public float fadeRate = 2;

	// Use this for initialization
	void Start () {
        audSource = GetComponent<AudioSource>();
        audSource.volume = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (audSource.volume > 0.0f && isFading)
        {
            audSource.volume -= Time.deltaTime * fadeRate;
            Debug.Log("Fading Out: " + audSource.volume);
        }
	}

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            Debug.Log("Start Playing: " + audSource.clip.name);
            audSource.Play();
            isFading = false;
        }
        
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player")
        {
            audSource.volume += Time.deltaTime * fadeRate;
            Debug.Log("Fading In: " + audSource.volume);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("Stop Playing: " + audSource.clip.name);
            isFading = true;
        }
    }
}
