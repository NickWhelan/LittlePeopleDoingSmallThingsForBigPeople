using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] audSources;

    [SerializeField]
    MusicBoxController[] musicBoxControllers;

    [SerializeField]
    AudioSource origSource;

    public VolumeSlider slider;

    bool origStarted = false;

    float audVolume = 0f, audPitch = 0f;

    // Use this for initialization
    void Start()
    {
        
        audSources = GetComponents<AudioSource>();
        for (int i = 0; i < audSources.Length; i++)
        {
            audSources[i].enabled = false;
        }

    }

    private void Update()
    {
        UpdateVolume(audVolume = slider.volumeLevel);
        if(audPitch >= 1)
            UpdatePitch((audPitch - 0.00001f));
    }

    public void UpdateVolume(float _volume)
    {
        for (int i = 0; i < audSources.Length; i++)
        {
            audSources[i].volume = _volume;
        }
    }

    public void UpdatePitch(float _pitch)
    {
        for (int i = 0; i < audSources.Length; i++)
        {
            audSources[i].pitch = _pitch;
        }
    }

    void UpdateSong(string songName)
    {
        switch (songName)
        {
            case "In Too Deep":
                audSources[0].clip = musicBoxControllers[0].audioClips[1];
                audSources[1].clip = musicBoxControllers[1].audioClips[1];
                audSources[2].clip = musicBoxControllers[2].audioClips[1];
                audSources[3].clip = musicBoxControllers[3].audioClips[1];
                break;

            case "20th Century Boy":
                audSources[0].clip = musicBoxControllers[0].audioClips[0];
                audSources[1].clip = musicBoxControllers[1].audioClips[0];
                audSources[2].clip = musicBoxControllers[2].audioClips[0];
                audSources[3].clip = musicBoxControllers[3].audioClips[0];
                break;

            case "Derezzed":
                Debug.Log("BITCH THIS AINT DONE YET. :V");
                break;

            default:
                Debug.Log("Song isn't in this!");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Instrument")
        {

            Debug.Log(other.name);
            for (int i = 0; i < audSources.Length; i++)
            {
                if (other.GetComponent<MusicBoxController>().isActiveSong)
                {
                    UpdateSong(other.GetComponent<MusicBoxController>().songName);
                }
                if (audSources[i].clip.name.Contains(other.name) &&!origStarted)
                {
                    origSource = audSources[i];
                    Debug.Log(origSource.clip.name);
                    audSources[i].enabled = true;
                    origStarted = true;
                }
                if (audSources[i].clip.name.Contains(other.name) && origStarted)
                {
                    audSources[i].enabled = true;
                    audSources[i].time = origSource.time;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Instrument")
        {
            for (int i = 0; i < audSources.Length; i++)
            {
                if (audSources[i].clip.name.Contains(other.name) && origSource.clip.name.Contains(other.name))
                {
                    audSources[i].enabled = false;
                    origSource = audSources[Random.Range(0, 4)];
                    for(int j = 0; j < 5; j++)
                    {
                        origSource = audSources[Random.Range(0, 4)];
                    }

                    //origStarted = false;
                    Debug.Log("New Source: " + origSource.clip.name);
                }
                else if(audSources[i].clip.name.Contains(other.name))
                {
                    audSources[i].enabled = false;
                }
            }

        }
    }
}
