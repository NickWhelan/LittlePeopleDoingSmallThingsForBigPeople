using UnityEngine;
using System.Collections;

public class Curve : MonoBehaviour {
    Camera cam;
    Vector3 OrginPoint; 
    Vector3 EndPoint;
    Vector3 OrginPointControl, EndPointControl;
    Vector3 ScreenZeroZero;
    LineRenderer line;
    [Range (2,25)]
    public int numberOfsegInLine = 11;
    //public OrginPointControlObj, EndPointControlObj;
    // Update is called once per frame
    public void Start() {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();

        ScreenZeroZero = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Random.Range(30,40)));
        OrginPoint = ScreenZeroZero;
        EndPoint = transform.position;

        OrginPointControl = new Vector3(OrginPoint.x, OrginPoint.y-10, OrginPoint.z-5f);
        EndPointControl = new Vector3(EndPoint.x, EndPoint.y-10, EndPoint.z+15);
        //line.numPositions = numberOfsegInLine;
        line.numPositions = numberOfsegInLine;
        //line.SetVertexCount(numberOfsegInLine);
    }
    void Update()
    {
        line.numPositions = numberOfsegInLine;
        for (int i = 0; i < numberOfsegInLine; ++i)
        {
            float t = (float)i / (float)(numberOfsegInLine - 1);
            // Bezier curve function
            Vector3 pos = Mathf.Pow((1 - t), 3) * OrginPoint + 3 * Mathf.Pow((1 - t), 2) * t * OrginPointControl + 3 * (1 - t) * Mathf.Pow(t, 2) * EndPointControl + Mathf.Pow(t, 3) * EndPoint;
            line.SetPosition(i, pos);
        }

    }
}
