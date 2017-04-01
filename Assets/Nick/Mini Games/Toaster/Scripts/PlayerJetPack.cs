using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetPack : MonoBehaviour {
    public PlayerControlls Parent;
    public ParticleSystem JetFuel;
    public Rope cable;
   
	// Use this for initialization
	void Start () {

        JetFuel.Stop();
        JetFuel.Clear();

        cable.setupJetPack();
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
