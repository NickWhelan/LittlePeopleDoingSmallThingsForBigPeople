using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Toast : MonoBehaviour {
    MeshFilter mesh;
    public List<Vector4> VectorandTemp;
    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>();

        VectorandTemp = new List<Vector4>();
        foreach (Vector3 vert in mesh.mesh.vertices)
        {
            if (vert.z <= 0)
            {
                VectorandTemp.Add(new Vector4(vert.x * transform.localScale.x, vert.y * transform.localScale.y, vert.z * transform.localScale.z, 0));
            }
        }
    }

    public void FindVert(Vector3 HitPoint)
    {
        for (int i = 0; i < VectorandTemp.Count - 1; i++)
        {
            if (Vector2.Distance(new Vector3(transform.localPosition.x + VectorandTemp[i].x, transform.localPosition.y + VectorandTemp[i].y), new Vector2(HitPoint.x, HitPoint.y)) < 0.8f)
            {
                
                Debug.DrawLine(HitPoint, transform.position + new Vector3(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z), Color.green);
                Debug.DrawRay(transform.position + new Vector3(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z), Vector3.forward, Color.red);
                VectorandTemp[i] = new Vector4(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z, VectorandTemp[i].w + 1);
                
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
