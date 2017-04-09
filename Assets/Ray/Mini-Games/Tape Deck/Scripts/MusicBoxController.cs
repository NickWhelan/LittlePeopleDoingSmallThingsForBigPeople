using UnityEngine;

public class MusicBoxController : MonoBehaviour {
    public AudioClip[] audioClips;

    public BoxCollider[] boxColliders;

    public GameObject musicControllerGObj;

    MusicController musicController;

    public string songName;

    private void Start()
    {
        musicController = musicControllerGObj.GetComponent<MusicController>();
        boxColliders = GetComponents<BoxCollider>();
        Physics.IgnoreCollision(boxColliders[0], musicController.GetComponent<BoxCollider>(), true);
    }
}
