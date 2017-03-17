using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxController : MonoBehaviour {
    public AudioClip[] audioClips;

    public BoxCollider[] boxColliders;

    public string songName;

    [SerializeField]
    private bool _isActiveSong;

    public bool isActiveSong
    {
        get { return _isActiveSong; }
        set { _isActiveSong = isActiveSong; }
    }

    private void Start()
    {
        boxColliders = GetComponents<BoxCollider>();
    }
}
