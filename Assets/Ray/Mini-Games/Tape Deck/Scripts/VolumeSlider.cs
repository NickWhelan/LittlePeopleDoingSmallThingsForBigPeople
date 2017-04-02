using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    public Transform startPoint, 
        endPoint, volumeKnob;

    public float volumeLevel;

    public Text volumeText;
    // Update is called once per frame
    void Update () {
        volumeKnob.position = new Vector3(volumeKnob.position.x, volumeKnob.position.y, Mathf.Clamp(volumeKnob.position.z, startPoint.position.z, endPoint.position.z));
        volumeLevel = volumeKnob.localPosition.z;
        if(volumeLevel > 0)
            UpdateVolume();

        volumeText.text = "/ " + (System.Math.Round(volumeLevel, 2) * 100).ToString();
    }

    public void UpdateVolume()
    {
        volumeKnob.localPosition -= new Vector3(0, 0, .001f);
    }

}
