using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetPack : MonoBehaviour {
    public PlayerControlls Parent;
    public ParticleSystem JetFuel,FlameThrower;
    public Rope cable;
    public bool Shooting;
	// Use this for initialization
	void Start () {
        Shooting = false;
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

        if (Parent.ButtonRBPressed && !FlameThrower.isPlaying)
        {
            Shooting = true;
            FlameThrower.Play();
        }
        else if (!Parent.ButtonRBPressed && FlameThrower.isPlaying)
        {
            Shooting = false;
            FlameThrower.Stop();
        }
    }
}
