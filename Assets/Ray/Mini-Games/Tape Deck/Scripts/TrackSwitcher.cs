using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour {
    [Header("Public Objects")]
    public GameObject[] mBoxes;
    public AudioClip[] audioTracks;

    private AudioClip playingClip;

    public GameObject[] swapPoints;
    public GameObject volumeSlider;
    private AudioSource audSource;

    private int activeBox = 0;

    // Use this for initialization
    void Start () {
        audSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        audSource.volume = volumeSlider.GetComponent<VolumeSlider>().volumeLevel;
        audSource.volume = 1;
        //audSource.pitch = 1;
	}

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < mBoxes.Length; i++)
        {
            if (mBoxes[i].GetComponent<MusicPlayer>().isActive && !audSource.isPlaying)
            {
                swapPoints[activeBox].SetActive(false);
                activeBox = i;
                playingClip = audSource.clip = audioTracks[i];
                swapPoints[i].SetActive(true);
            }
            else if(mBoxes[i].GetComponent<MusicPlayer>().isActive && audSource.isPlaying && audioTracks[i] != playingClip)
            {
                swapPoints[activeBox].SetActive(false);
                swapPoints[i].SetActive(true);
                playingClip = audSource.clip = audioTracks[i];
                activeBox = i;
            }
        }
    }
}
