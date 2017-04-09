using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {
    public PlayerControlls Parent;
    public ParticleSystem FlameThrowerPartical;
    public bool Shooting;
    public bool InvertZ = false;
    // Use this for initialization
    void Start () {
        Shooting = false;
        FlameThrowerPartical.Stop();
        FlameThrowerPartical.Clear();
    }

    void Fire() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, (InvertZ)? -Vector3.forward : Vector3.forward, Color.green);
        if (!Physics.Raycast(transform.position, (InvertZ) ? -Vector3.forward : Vector3.forward, out hit) )
        {
            return;
        }
        if (hit.transform.gameObject.tag != "Bread")
        {
            Debug.DrawLine(hit.point, transform.position);
            print(hit.transform.name + " is Not Bread");
            return;
        }
        Mesh mesh = hit.transform.GetComponent<MeshFilter>().mesh;
        if (mesh == null )
        {
            Debug.LogError("Collided Object doesn't have a MeshFilter");
            return;
        }
        Debug.DrawLine(hit.point, transform.position);
        hit.transform.GetComponent<Toast>().FindVert(hit.point);
    }


	// Update is called once per frame
	void Update () {
        if (Parent.ButtonRBPressed && !Shooting)
        {
            Shooting = true;
            FlameThrowerPartical.Play();
        }
        else if (!Parent.ButtonRBPressed && Shooting)
        {
            Shooting = false;
            FlameThrowerPartical.Stop();
        }
        if (Shooting)
        {
            Fire();
        }
    }

}
