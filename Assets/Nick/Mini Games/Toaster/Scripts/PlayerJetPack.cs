using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetPack : MonoBehaviour {
    public PlayerControlls Parent;
    public ParticleSystem JetFuel;
	// Use this for initialization
	void Start () {
        JetFuel.Stop();
        JetFuel.Clear();

	}

    void FixedUpdate() {
        if (Parent.isJumping && !JetFuel.isPlaying)
        {
            JetFuel.Play();
        }
        else if (!Parent.isJumping && JetFuel.isPlaying)
        {
            JetFuel.Stop();
        }
    }
}
