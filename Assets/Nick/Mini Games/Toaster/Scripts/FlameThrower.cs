using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {
    public PlayerControlls Parent;
    public ParticleSystem FlameThrowerPartical;
    public bool Shooting;
    // Use this for initialization
    void Start () {
        Shooting = false;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Parent.ButtonRBPressed && !FlameThrowerPartical.isPlaying)
        {
            Shooting = true;
            FlameThrowerPartical.Play();
        }
        else if (!Parent.ButtonRBPressed && FlameThrowerPartical.isPlaying)
        {
            Shooting = false;
            FlameThrowerPartical.Stop();
        }
    }
}
