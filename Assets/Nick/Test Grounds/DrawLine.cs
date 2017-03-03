using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    LineRenderer line;
    GameObject EndPoint;
    public GameObject EndPointObj;

    public bool FireBull;
   
	// Use this for initialization
	void Start () {
        EndPoint = new GameObject();
        line = GetComponent<LineRenderer>();
        //line.SetVertexCount(2)  
	}
	
	// Update is called once per frame
	void Update () {
        if (FireBull) {
            Fire();
            FireBull = false;
        }
        if (EndPoint != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, EndPoint.transform.position);
        }
    }
    void Fire() {
        EndPoint = Instantiate(EndPointObj, EndPoint.transform.position, EndPoint.transform.rotation);
        EndPoint.GetComponent<Rigidbody>().velocity = EndPoint.transform.forward * 6;
    }
}
