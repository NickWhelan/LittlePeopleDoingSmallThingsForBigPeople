using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbuildmesh : MonoBehaviour {

    public Material mat;

    Mesh mesh;
    MeshFilter MeshF;


    public Vector3[] newVertices;
    public Vector2[]newUV;
    public int[] newTriangles;

    void Start()
    {
        GetComponent<MeshRenderer>().material = mat;
        MeshF = GetComponent<MeshFilter>();
        newVertices = MeshF.mesh.vertices;
        newUV = MeshF.mesh.uv;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
