using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour {
    public Vector3 LocalHitPoint;
    // Use this for initialization
    void Start () {
		
	}
    /*
    void FixedUpdate()
    {  RaycastHit hit;
        if (!Physics.Raycast(transform.position, -Vector3.forward, out hit))
        {
            return;
        }
        Debug.DrawRay(transform.position,  -Vector3.forward, Color.green);
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            print("no");
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Vector3 n0 = normals[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hit.triangleIndex * 3 + 2]];
        Vector3 baryCenter = hit.barycentricCoordinate;
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        interpolatedNormal = interpolatedNormal.normalized;
        Transform hitTransform = hit.collider.transform;
        interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);
        Debug.DrawRay(hit.point, interpolatedNormal,Color.green);
    }*/
    void FixedUpdate()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -Vector3.forward, out hit))
        {
            return;
        }
        Debug.DrawRay(transform.position, -Vector3.forward, Color.green);
        Mesh mesh = hit.transform.GetComponent<MeshFilter>().mesh;
        if (mesh == null)
        {
            Debug.LogError("Collided Object doesn't have a MeshFilter");
            return;
        }
        LocalHitPoint = new Vector3(hit.transform.position.x-hit.point.x, hit.transform.position.y - hit.point.y, hit.transform.position.z - hit.point.z);
    }





        void Update()
    {
      
    }
}
