using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIHumanBehaviour : MonoBehaviour {
    public MusicBoxController[] musicBoxes;

    MusicBoxController o_chosenSong;
    

    //Delay for writing text to the screen
    [Range(0, 0.5f)]
    public float delay;

    [SerializeField]
    private int i_chosenVolumeLevel = 50;
    public int ChosenVolumeLevel
    {
        get { return i_chosenVolumeLevel; }
    }

    [SerializeField]
    private int i_chosenPitchLevel = 50;
    public int ChosenPitchLevel
    {
        get { return i_chosenPitchLevel; }
    }

    public Text t_chosenSongName,
        t_chosenVolumeLevel,
        t_chosenPitchLevel,
        t_multiplierText;

    public bool firstTimeRun = true;

    private int _multiplierLevel;
    public int MultipilerLevel
    {
        get { return _multiplierLevel; }
        set { _multiplierLevel = value; }
    }

    float multiplierTick = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SetUpHuman", 1.0f, 1.0f);
    }

    void SetUpHuman()
    {
        if (firstTimeRun)
        {
            StartCoroutine(WriteText("Multiplier: x0", t_multiplierText));
            firstTimeRun = false;
        }
       // t_chosenSongName.text = "Song Name: ";
        o_chosenSong = musicBoxes[Random.Range(0, 4)];
        i_chosenVolumeLevel = Random.Range(0, 101);

        t_chosenVolumeLevel.text = "Volume: " + i_chosenVolumeLevel.ToString();
        StartCoroutine(WriteText("Volume: " + i_chosenVolumeLevel.ToString(), t_chosenVolumeLevel));

        i_chosenPitchLevel = Random.Range(49, 100);
        StartCoroutine(WriteText("Pitch: " + i_chosenPitchLevel.ToString(), t_chosenPitchLevel));

        StartCoroutine(WriteText(o_chosenSong.songName, t_chosenSongName));
    }

    IEnumerator WriteText(string textToWrite, Text textToWriteTo)
    {
        textToWriteTo.text = "";
        for(int i = 0; i <= textToWrite.Length; ++i)
        {
            textToWriteTo.text += textToWrite[i];

            yield return new WaitForSeconds(delay);
        }
    }

   public IEnumerator UpdatePitchLevel(float _pitch)
    {
        float tempNum = i_chosenPitchLevel * 0.01f;
        if (_pitch == tempNum)
        {
            Debug.Log("Booooop");
        }



        yield return  null;
    }

    public IEnumerator UpdateVolumeLevel(float _volume)
    {
        float tempNum = i_chosenVolumeLevel * 0.01f;
        if (_volume == tempNum)
        {
            Debug.Log("Beeeeep");
            multiplierTick += Time.deltaTime;
        }
        if (multiplierTick >= 5)
        {
            _multiplierLevel += 2;
            t_multiplierText.text = "Multiplier: x" + _multiplierLevel.ToString();
            StartCoroutine(WriteText("Multiplier: x" + _multiplierLevel.ToString(), t_multiplierText));
            multiplierTick = 0;
        }

        yield return null;
    }
}
