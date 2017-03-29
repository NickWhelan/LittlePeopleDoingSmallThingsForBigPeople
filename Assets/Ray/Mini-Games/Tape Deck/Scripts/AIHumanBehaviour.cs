using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AIHumanBehaviour : MonoBehaviour {
    public MusicBoxController[] musicBoxes;

    MusicBoxController o_chosenSong;


    public Shader lastShader;
    
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


    [Range(1, 3)]
    public float _multiplierUpgradeRate;

    [Range(5, 10)]
    public int pointValue;
    public int score = 0;

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

        //If there is a chosen song, return its shader to normal
        if (o_chosenSong != null)
        {
            o_chosenSong.GetComponent<Renderer>().material.shader = lastShader;
        }
        if (o_chosenSong != null && o_chosenSong.transform.childCount > 0 )
        {
            o_chosenSong.transform.GetChild(0).GetComponent<Renderer>().material.shader = lastShader;
        }

        //Pick a song
        o_chosenSong = musicBoxes[Random.Range(0, 4)];

        //If that song has a child, change that child shader
        if(o_chosenSong.transform.childCount > 0)
        {
            lastShader = o_chosenSong.transform.GetChild(0).GetComponent<Renderer>().material.shader;
            o_chosenSong.transform.GetChild(0).GetComponent<Renderer>().material.shader = Shader.Find("Custom/Surface Wobble");
        }
        else //Just change the shader
        {
            lastShader = o_chosenSong.GetComponent<Renderer>().material.shader;
            o_chosenSong.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Surface Wobble");
        }

        //Round the volume level to two decimal places to be the same as the volume 0.0f - 1.0f
        f_chosenVolumeLevel = (float)System.Math.Round((Random.Range(0.0f, 101.0f) * 0.01f), 2);
        
        //Print the number to the screen using coroutines and multiply it back up to be between 0 - 100
        StartCoroutine(WriteText("Volume: " + (f_chosenVolumeLevel * 100).ToString(), t_chosenVolumeLevel));

        //Pick a random pitch level, do the same as volume
        f_chosenPitchLevel = (float)System.Math.Round((Random.Range(50.0f, 101.0f) * 0.01f), 2);
        StartCoroutine(WriteText("Pitch: " + (f_chosenPitchLevel * 100).ToString(), t_chosenPitchLevel));

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
        float tempNum = f_chosenPitchLevel * 0.01f;
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
