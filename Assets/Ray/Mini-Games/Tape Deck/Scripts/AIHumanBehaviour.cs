using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AIHumanBehaviour : MonoBehaviour {
    public MusicBoxController[] musicBoxes;

    MusicBoxController o_chosenSong;
    public MusicController musControl;

    
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
    private float f_chosenPitchLevel = 50;
    public float ChosenPitchLevel
    {
        get { return f_chosenPitchLevel; }
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

    private bool _gameOver = false;
    public bool GameOver
    {
        set { _gameOver = value; }
    }

    [Range(1, 3)]
    public float _multiplierUpgradeRate;

    [Range(5, 10)]
    public int pointValue;
    public int score = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SetUpHuman", 1.0f, 40.0f);
        InvokeRepeating("CheckPlayingSong", 15.0f, 20.0f);
    }

    private void Update()
    {
        if(_gameOver)
        {
            CancelInvoke("SetUpHuman");
            CancelInvoke("CheckPlayingSong");
        }
    }
    void SetUpHuman()
    {
        if (firstTimeRun)
        {
            StartCoroutine(WriteText("Multiplier: x0", t_multiplierText));
            firstTimeRun = false;
        }

        //Pick a song
        o_chosenSong = musicBoxes[Random.Range(0, 4)];
        musControl.SetActiveSong(o_chosenSong);

        //Round the volume level to two decimal places to be the same as the volume 0.0f - 1.0f
        f_chosenVolumeLevel = (float)System.Math.Round((Random.Range(0.0f, 101.0f) * 0.01f), 2);
        
        //Print the number to the screen using coroutines and multiply it back up to be between 0 - 100
        StartCoroutine(WriteText("Volume: " + (f_chosenVolumeLevel * 100).ToString(), t_chosenVolumeLevel));

        //Pick a random pitch level, do the same as volume
        f_chosenPitchLevel = (float)System.Math.Round((Random.Range(50.0f, 101.0f) * 0.01f), 2);
        StartCoroutine(WriteText("Pitch: " + (f_chosenPitchLevel * 100).ToString(), t_chosenPitchLevel));

        StartCoroutine(WriteText(o_chosenSong.songName, t_chosenSongName));
    }

    //Used to print text one character at a time
    IEnumerator WriteText(string textToWrite, Text textToWriteTo)
    {
        textToWriteTo.text = "";
        for(int i = 0; i <= textToWrite.Length - 1; i++)
        {
            textToWriteTo.text += textToWrite[i];
            yield return new WaitForSeconds(delay);
        }
    }

    //Used to print one character at a time, with a custom delay value
    IEnumerator WriteText(string textToWrite, Text textToWriteTo, float _delay)
    {
        textToWriteTo.text = "";
        for (int i = 0; i <= textToWrite.Length - 1; i++)
        {
            textToWriteTo.text += textToWrite[i];

            yield return new WaitForSeconds(_delay);
        }
    }

    public IEnumerator UpdatePitchLevel(float _pitch)
    {
        float tempNum = f_chosenPitchLevel;
        if (!_gameOver)
        {
            if (_pitch == tempNum)
            {
                UpdateScore(_multiplierLevel);
            }
            if (multiplierTick >= _multiplierUpgradeRate && _multiplierLevel < 10)
            {
                _multiplierLevel += 2;
                t_multiplierText.text = "Multiplier: x" + _multiplierLevel.ToString();
                multiplierTick = 0;
            }
        }
        yield return null;
    }

    public IEnumerator UpdateVolumeLevel(float _volume)
    {
        float tempNum = f_chosenVolumeLevel; 
        if (!_gameOver)
        {
            if (_volume == tempNum)
            {
                multiplierTick += Time.deltaTime;
                UpdateScore(_multiplierLevel);
            }
            if (multiplierTick >= _multiplierUpgradeRate && _multiplierLevel < 10)
            {
                _multiplierLevel += 2;
                t_multiplierText.text = "Multiplier: x" + _multiplierLevel.ToString();
                multiplierTick = 0;
            }
        }
        yield return null;
    }

    void UpdateScore(int multiplierToPass)
    {
        if(multiplierToPass > 0)
        {
            score += (pointValue * multiplierToPass);
            t_scoreText.text = score.ToString();
        }
        else
        {
            score += pointValue;
            t_scoreText.text = score.ToString();
        }
    }

    void CheckPlayingSong()
    {
        if (musControl.SongName == o_chosenSong.songName)
        {
            score += 5000;
        }
        else if (musControl.SongName != o_chosenSong.songName)
        {
            score -= 5000;
        }
    }
}
