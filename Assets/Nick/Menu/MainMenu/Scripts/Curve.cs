using UnityEngine;
using System.Collections;

public class Curve : MonoBehaviour {
    Camera cam;
    Vector3 OrginPoint; 
    Transform EndPoint;
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
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        OrginPoint = ScreenZeroZero;
        EndPoint = transform;

        OrginPointControl = new Vector3(OrginPoint.x, OrginPoint.y-10, OrginPoint.z-5f);
        EndPointControl = new Vector3(EndPoint.position.x, EndPoint.position.y-10, EndPoint.position.z+15);
        //line.numPositions = numberOfsegInLine;
        line.numPositions = numberOfsegInLine;
        //line.SetVertexCount(numberOfsegInLine);
    }
    void Update()
    {
        EndPointControl = new Vector3(EndPoint.position.x, EndPoint.position.y - 10, EndPoint.position.z + 15);
        for (int i = 0; i < numberOfsegInLine; ++i)
        {
            float t = (float)i / (float)(numberOfsegInLine - 1);
            // Bezier curve function
            Vector3 pos = Mathf.Pow((1 - t), 3) * OrginPoint + 3 * Mathf.Pow((1 - t), 2) * t * OrginPointControl + 3 * (1 - t) * Mathf.Pow(t, 2) * EndPointControl + Mathf.Pow(t, 3) * EndPoint.transform.position;
            line.SetPosition(i, pos);
        }

    }
}
