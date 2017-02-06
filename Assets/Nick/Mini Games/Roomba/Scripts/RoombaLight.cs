using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaLight : MonoBehaviour {
    public GameObject RoombaA, RoombaB;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, RoombaA.transform.position);
        Debug.DrawLine(transform.position, RoombaB.transform.position);
        transform.position = new Vector3(
                            (RoombaA.transform.position.x + RoombaB.transform.position.x) / 2,
                           Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(RoombaA.transform.position),2) + Mathf.Pow(Vector3.Magnitude(RoombaB.transform.position),2)),
                            (RoombaA.transform.position.z + RoombaB.transform.position.z) / 2);
	}
}
