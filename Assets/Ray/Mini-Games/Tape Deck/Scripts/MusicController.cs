using UnityEngine;


public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] audSources;

    [SerializeField]
    MusicBoxController[] musicBoxControllers;

    [SerializeField]
    Transform[] musBoxSpawnPos;

    public AIHumanBehaviour humanBehaviour;

    Shader standardShader;

    private string _songName;
    public string SongName
    {
        get { return _songName; }
    }

    [SerializeField]
    AudioSource origSource;

    public VolumeSlider slider;

    bool origStarted = false;

    float audVolume = 0f, audPitch = 0f;

    // Use this for initialization
    void Start()
    {
        standardShader = Shader.Find("Standard");
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
        _songName = songName;
        for (int i = 0; i <= musicBoxControllers.Length - 1; i++)
        {
            musicBoxControllers[i].gameObject.transform.position = musBoxSpawnPos[i].position;
            if (musicBoxControllers[i].transform.childCount > 0)
            {
                musicBoxControllers[i].transform.GetChild(0).GetComponent<Renderer>().material.shader = standardShader;
            }
            else
            {
                musicBoxControllers[i].GetComponent<Renderer>().material.shader = standardShader;
            }
        }
        switch (songName)
        {
            case "20th Century Boy":
                for (int i = 0; i < musicBoxControllers.Length; i++)
                {
                    if (!musicBoxControllers[i].gameObject.activeInHierarchy)
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
                    Debug.Log(musicBoxControllers[i].name);
                    if (i == 2)
                    {
                        musicBoxControllers[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        audSources[i].clip = musicBoxControllers[2].audioClips[i];
                    }

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
        UpdateSong(_activeBox.songName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Instrument"))
        {
            for (int i = 0; i < musicBoxControllers.Length; i++)
            {
                if (audSources[i].clip.name.Contains(other.name) && !origStarted && audSources[i].isActiveAndEnabled)
                {
                    audSources[i].enabled = true;

                    origSource = audSources[i];
                    origStarted = true;
                }
                else if (audSources[i].clip.name.Contains(other.name) && audSources[i].clip != null && audSources[i].isActiveAndEnabled)
                {
                    audSources[i].enabled = true;
                    audSources[i].timeSamples = origSource.timeSamples;
                }
            }
            standardShader = other.transform.GetChild(0).GetComponent<Renderer>().material.shader;
            other.transform.GetChild(0).GetComponent<Renderer>().material.shader = Shader.Find("Custom/Surface Wobble");
            /*
            if (other.transform.childCount > 0)
            {
                standardShader = other.transform.GetChild(0).GetComponent<Renderer>().material.shader;
                other.transform.GetChild(0).GetComponent<Renderer>().material.shader = Shader.Find("Custom/Surface Wobble");
            }
            else
            {
                standardShader = other.GetComponent<Renderer>().material.shader;
                other.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Surface Wobble");
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Instrument"))
        {
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
                else if (audSources[i].clip.name.Contains(other.name))
                {
                    audSources[i].enabled = false;
                }
            }
            if (other.transform.childCount > 0)
            {
                other.transform.GetChild(0).GetComponent<Renderer>().material.shader = standardShader;
            }
            else
            {
                other.GetComponent<Renderer>().material.shader = standardShader;
            }
        }
    }
}
