using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour {
    [Header("Public Objects")]
    public GameObject[] mBoxes;
    public AudioClip[] audioTracks;
    public GameObject[] swapPoints;
    public GameObject volumeSlider;
    public GameObject cd;
    private AudioSource audSource;
    private int activeBox;
    // Use this for initialization
    void Start () {
        audSource = GetComponent<AudioSource>();
        audSource.Stop();
    }

    // Update is called once per frame
    void Update () {
        audSource.volume = volumeSlider.GetComponent<VolumeSlider>().volumeLevel;
	}

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < mBoxes.Length; i++)
        {
            if (mBoxes[i].GetComponent<MusicPlayer>().isActive && !audSource.isPlaying)
            {
                mBoxes[activeBox].GetComponent<MusicPlayer>().StopSong();
                swapPoints[activeBox].SetActive(false);

                print(mBoxes[i] + "is enabled");

                audSource.clip = audioTracks[i];

                mBoxes[i].GetComponent<MusicPlayer>().PlaySong();

                Debug.Log(audSource.isPlaying + " " + audSource.clip);

                swapPoints[i].SetActive(true);

                activeBox = i;
            }
        }
    }
}
