using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour {
    [Header("Public Objects")]
    public GameObject[] mBoxes;
    public AudioClip[] audioTracks;
    public GameObject[] swapPoints;
    public GameObject volumeSlider;

    private AudioSource audSource;

    // Use this for initialization
    void Start () {
        audSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {


	}

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < mBoxes.Length; i++)
        {
            if (mBoxes[i].GetComponent<MusicPlayer>().isActive)
            {
                print(mBoxes[i] + "is enabled");
                audSource.clip = audioTracks[i];
                audSource.Play();
                swapPoints[i].SetActive(true);
            }
            else if (!mBoxes[i].GetComponent<MusicPlayer>().isActive)
            {
                audSource.Stop();
                swapPoints[i].SetActive(false);
            }
        }
    }
}
