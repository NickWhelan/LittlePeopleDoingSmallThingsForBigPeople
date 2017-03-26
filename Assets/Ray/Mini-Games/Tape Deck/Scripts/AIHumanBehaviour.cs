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
    private int i_chosenVolumeLevel;
    public int ChosenVolumeLevel
    {
        get { return i_chosenVolumeLevel; }
    }

    [SerializeField]
    private int i_chosenPitchLevel;
    public int ChosenPitchLevel
    {
        get { return i_chosenPitchLevel; }
    }

    public Text s_chosenSongName,
        s_chosenVolumeLevel,
        s_chosenPitchLevel,
        s_multiplierText;

    public bool firstTimeRun = true;

    private int _multiplierLevel;
    public int MultipilerLevel
    {
        get { return _multiplierLevel; }
        set { _multiplierLevel = value; }
    }
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SetUpHuman", 1.0f, 5.0f);
    }

    void SetUpHuman()
    {
        s_chosenSongName.text = "Song Name: ";
        o_chosenSong = musicBoxes[Random.Range(0, 3)];
        i_chosenVolumeLevel = Random.Range(0, 101);
        s_chosenVolumeLevel.text = "Volume: " + i_chosenVolumeLevel.ToString();
        i_chosenPitchLevel = Random.Range(49, 100);
        s_chosenPitchLevel.text = "Pitch: " + i_chosenPitchLevel.ToString();

        if (firstTimeRun)
        {
            s_multiplierText.text = "x0";
            firstTimeRun = false;
        }
        else
        {
            s_multiplierText.text = "x2";
        }
        StartCoroutine(WriteText());
    }
    IEnumerator WriteText()
    {

        for(int i = 0; i <= s_chosenSongName.text.Length;++i)
        {
            s_chosenSongName.text += o_chosenSong.songName[i];

            yield return new WaitForSeconds(delay);
        }
        
    }

    void UpdatePitchLevel()
    {

    }

    void UpdateVolumeLevel()
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
