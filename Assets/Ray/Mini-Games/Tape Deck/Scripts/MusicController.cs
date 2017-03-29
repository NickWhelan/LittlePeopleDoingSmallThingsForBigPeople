using UnityEngine;


public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] audSources;

    [SerializeField]
    MusicBoxController[] musicBoxControllers;

    public AIHumanBehaviour humanBehaviour;

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
            audSources[i].pitch = 0;
        }
    }

    private void Update()
    {
        UpdateVolume(audVolume = slider.volumeLevel);

        if (audPitch >= 1)
            UpdatePitch((audPitch - 0.00001f));
    }

    public void UpdateVolume(float _volume)
    {
        for (int i = 0; i < audSources.Length; i++)
        {
            audSources[i].volume = (float)System.Math.Round(_volume, 2);
        }
        humanBehaviour.StartCoroutine(humanBehaviour.UpdateVolumeLevel((float)System.Math.Round(_volume, 2)));
    }

    public void UpdatePitch(float _pitch)
    {
        for (int i = 0; i < audSources.Length; i++)
        {
            audSources[i].pitch = _pitch;
        }
        humanBehaviour.StartCoroutine(humanBehaviour.UpdatePitchLevel(_pitch));
    }

    void UpdateSong(string songName)
    {
        Debug.Log("PLAYING SONG " + songName);

        switch (songName)
        {
            case "20th Century Boy":
                for (int i = 0; i < musicBoxControllers.Length; i++)
                {
                    if(!musicBoxControllers[i].gameObject.activeInHierarchy)
                    {
                        musicBoxControllers[i].gameObject.SetActive(true);
                    }
                    audSources[i].clip = musicBoxControllers[0].audioClips[i];
                }
                break;

            case "In Too Deep":
                for (int i = 0; i < musicBoxControllers.Length; i++)
                {
                    if (!musicBoxControllers[i].gameObject.activeInHierarchy)
                    {
                        musicBoxControllers[i].gameObject.SetActive(true);
                    }
                    audSources[i].clip = musicBoxControllers[1].audioClips[i];
                }
                break;

            case "Derezzed":
                for (int i = 0; i < musicBoxControllers.Length - 1; i++)
                {
                    audSources[i].clip = musicBoxControllers[2].audioClips[i];
                }
                break;

            case "Sail Away":
                for (int i = 0; i < musicBoxControllers.Length; i++)
                {
                    if (!musicBoxControllers[i].gameObject.activeInHierarchy)
                    {
                        musicBoxControllers[i].gameObject.SetActive(true);
                    }
                    audSources[i].clip = musicBoxControllers[3].audioClips[i];
                }
                break;

            default:
                Debug.Log("Song isn't in this!");
                break;
        }
    }

    public void SetActiveSong(MusicBoxController _activeBox)
    {
        _activeBox.IsActiveSong = true;
        Debug.Log("PRIMED AND READY.");
        _activeBox.GetComponent<Renderer>().material.color = Color.yellow;
        for (int i = 0; i < musicBoxControllers.Length; i++)
        {
            if (musicBoxControllers[i] != _activeBox)
            {
                Debug.Log("turning off: " + musicBoxControllers[i].name);
                musicBoxControllers[i].IsActiveSong = false;
                musicBoxControllers[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
        UpdateSong(_activeBox.songName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Instrument"))
        {
            for(int i = 0; i < musicBoxControllers.Length; i++)
            {
                if (audSources[i].clip.name.Contains(other.name) && !origStarted)
                {
                    audSources[i].enabled = true;
                    origSource = audSources[i];
                    origStarted = true;
                }
                else if(audSources[i].clip.name.Contains(other.name) && audSources[i].clip != null)
                {
                    audSources[i].enabled = true;
                }
                other.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Instrument"))
        {
            if (other.GetComponent<MusicBoxController>().IsActiveSong)
            {
                other.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                other.GetComponent<Renderer>().material.color = Color.red;
            }
            for (int i = 0; i < audSources.Length; i++)
            {
                if (audSources[i].enabled && origSource.name.Contains(other.name))
                {
                    audSources[i].enabled = false;
                    for (int j = 0; j < 5; j++)
                    {
                        if (audSources[j].enabled)
                        {
                            origSource = audSources[j];
                            break;
                        }
                        else
                        {
                            origSource = null;
                            origStarted = false;
                        }
                    }
                }
                else if(audSources[i].clip.name.Contains(other.name))
                {
                    audSources[i].enabled = false;
                }
            }
        }
    }
}
