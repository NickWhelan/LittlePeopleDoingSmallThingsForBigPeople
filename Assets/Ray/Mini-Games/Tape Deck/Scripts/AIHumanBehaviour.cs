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
    private float f_chosenVolumeLevel = 50;
    public float ChosenVolumeLevel
    {
        get { return f_chosenVolumeLevel; }
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
        t_multiplierText,
        t_scoreText;

    public bool firstTimeRun = true;

    private int _multiplierLevel;
    public int MultipilerLevel
    {
        get { return _multiplierLevel; }
        set { _multiplierLevel = value; }
    }

    //Used for score
    float multiplierTick = 0;


    [Range(1, 3)]
    public float _multiplierUpgradeRate;

    [Range(5, 10)]
    public int pointValue;
    public int score = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SetUpHuman", 1.0f, 1000.0f);
    }

    void SetUpHuman()
    {
        if (firstTimeRun)
        {
            StartCoroutine(WriteText("Multiplier: x0", t_multiplierText));
            firstTimeRun = false;
        }
        o_chosenSong = musicBoxes[Random.Range(0, 4)];
        f_chosenVolumeLevel = (float)System.Math.Round((Random.Range(0.0f, 101.0f) * 0.01f), 2);

        
        //t_chosenVolumeLevel.text = "Volume: " + (f_chosenVolumeLevel * 100).ToString();
        StartCoroutine(WriteText("Volume: " + (f_chosenVolumeLevel * 100).ToString(), t_chosenVolumeLevel));

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

    IEnumerator WriteText(string textToWrite, Text textToWriteTo, float _delay)
    {
        textToWriteTo.text = "";
        for (int i = 0; i <= textToWrite.Length; ++i)
        {
            textToWriteTo.text += textToWrite[i];

            yield return new WaitForSeconds(_delay);
        }
    }

    public IEnumerator UpdatePitchLevel(float _pitch)
    {
        float tempNum = i_chosenPitchLevel * 0.01f;
        if (_pitch == tempNum)
        {
            Debug.Log("Booooop");
            UpdateScore(_multiplierLevel);
        }

        yield return  null;
    }

    public IEnumerator UpdateVolumeLevel(float _volume)
    {
        float tempNum = f_chosenVolumeLevel;
        if (_volume == tempNum)
        {
            multiplierTick += Time.deltaTime;
            UpdateScore(_multiplierLevel);
        }
        if (multiplierTick >= _multiplierUpgradeRate)
        {
            _multiplierLevel += 2;
            t_multiplierText.text = "Multiplier: x" + _multiplierLevel.ToString();
            multiplierTick = 0;
        }

        yield return null;
    }

    void UpdateScore(int multiplierToPass)
    {
        if(multiplierToPass > 0)
        {
            score += (pointValue * multiplierToPass);
            t_scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            score += pointValue;
            t_scoreText.text = "Score: " + score.ToString();
        }
    }
}
