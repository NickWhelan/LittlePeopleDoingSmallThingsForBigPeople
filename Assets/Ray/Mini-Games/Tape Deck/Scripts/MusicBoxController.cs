using UnityEngine;

public class MusicBoxController : MonoBehaviour {
    public AudioClip[] audioClips;

    public BoxCollider[] boxColliders;

    public GameObject musicControllerGObj;

    MusicController musicController;

    public string songName;

    [SerializeField]
    private bool _isActiveSong;

    public bool IsActiveSong
    {
        get { return _isActiveSong; }
        set { _isActiveSong = value; }
    }

    private void Start()
    {
        musicController = musicControllerGObj.GetComponent<MusicController>();
        boxColliders = GetComponents<BoxCollider>();
        Physics.IgnoreCollision(boxColliders[0], musicController.GetComponent<BoxCollider>(), true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            if(other.GetComponent<PlayerControlls>().ButtonAPressed)
            {
                musicController.SetActiveSong(this);
            }
        }
    }
}
