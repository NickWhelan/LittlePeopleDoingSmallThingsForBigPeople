using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Toast : MonoBehaviour {
    MeshFilter mesh;
    public List<Vector4> VectorandTemp;
    public List<Color> ColorArray;
    public GameObject SmokePreFab;
    public List<GameObject> SmokePoints;
    public float Score;
    public bool Team2;
    Dictionary<int, int> PointToColorIndex;

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>();

        VectorandTemp = new List<Vector4>();
        ColorArray = new List<Color>();

        PointToColorIndex = new Dictionary<int, int>();

        foreach (Vector3 vert in mesh.mesh.vertices)
        {
            ColorArray.Add(new Color(0,0,0,0));
            if (vert.z <= 0 && !Team2)
            {
                VectorandTemp.Add(new Vector4(vert.x * transform.localScale.x, vert.y * transform.localScale.y, vert.z * transform.localScale.z, 0));
                PointToColorIndex.Add(VectorandTemp.Count - 1, ColorArray.Count - 1);
            }
            else if (vert.z > 0  && Team2)
            {
                VectorandTemp.Add(new Vector4(vert.x * transform.localScale.x, vert.y * transform.localScale.y, vert.z * transform.localScale.z, 0));
                PointToColorIndex.Add(VectorandTemp.Count - 1, ColorArray.Count - 1);
            }
            
        }
        mesh.mesh.MarkDynamic();
        mesh.mesh.SetColors(ColorArray);
    }

    public void FindVert(Vector3 HitPoint)
    {
        for (int i = 0; i < VectorandTemp.Count - 1; i++)
        {
            if (Vector2.Distance(new Vector2(transform.localPosition.x + VectorandTemp[i].x, transform.localPosition.y + VectorandTemp[i].y), new Vector2(HitPoint.x, HitPoint.y)) < 0.8f)
            {
                
                Debug.DrawLine(HitPoint, transform.position + new Vector3(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z), Color.green);
                Debug.DrawRay(transform.position + new Vector3(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z), Vector3.forward, Color.red);
                VectorandTemp[i] = new Vector4(VectorandTemp[i].x, VectorandTemp[i].y, VectorandTemp[i].z, Mathf.Min( VectorandTemp[i].w + 2.5f));
                ColorArray[PointToColorIndex[i]] = new Color(0,0,0,VectorandTemp[i].w/255.0f);
                if (VectorandTemp[i].w < (255 / 3) *2)
                {
                    Score += 0.001f;
                }
                else {
                    if (VectorandTemp[i].w > 255)
                    {
                        SmokePoints.Add(Instantiate(SmokePreFab));
                        SmokePoints[SmokePoints.Count - 1].transform.parent = transform;
                        SmokePoints[SmokePoints.Count - 1].transform.position = VectorandTemp[i];
                        SmokePoints[SmokePoints.Count - 1].transform.position += transform.position;
                    }

                    Score -= 0.001f;
                    if (Score < 0) {
                        Score = 0;
                    }
                }
            }
        }
        GetComponent<MeshFilter>().mesh.SetColors(ColorArray);

    }
}
