using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour {
    [SerializeField]
    TapeDeckGameLogic tapeDeckGameLogic;

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
        tapeDeckGameLogic = GameObject.FindGameObjectWithTag("Overlord").GetComponent<TapeDeckGameLogic>();
    }

    // Update is called once per frame
    void Update () {
        audSource.volume = volumeSlider.GetComponent<VolumeSlider>().volumeLevel;
        audSource.volume = 1;
        //audSource.pitch = 1;
        if(audSource.volume < 1)
        {
            tapeDeckGameLogic.volumeLevel.text = string.Format("{0:0.00}", audSource.volume);
        }
        else
        {
            tapeDeckGameLogic.volumeLevel.text = audSource.volume.ToString();
        }
        if(audSource.pitch < 1)
        {
            tapeDeckGameLogic.pitchLevel.text = string.Format("{0:0.00}", audSource.pitch);
        }
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
            tapeDeckGameLogic.songName.text = playingClip.name;
        }
    }
}
