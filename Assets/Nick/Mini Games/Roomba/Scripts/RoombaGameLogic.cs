using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaGameLogic : MonoBehaviour
{
    public GameObject RoombaA, RoombaB;
    Vector3 RoombaALastPos, RoombaBLastPos;
    // Use this for initialization
    void Start()
    {
        RoombaALastPos = RoombaA.transform.position;
        RoombaBLastPos = RoombaB.transform.position;
        Physics.IgnoreCollision(RoombaA.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>(), RoombaB.GetComponent<RoombaLogic>().RoombaGround.GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(RoombaA.transform.position.x, 1, RoombaA.transform.position.z), new Vector3(RoombaA.transform.position.x+ (RoombaA.transform.lossyScale.x / 2), 0.5f, RoombaA.transform.position.z));
        Debug.DrawLine(new Vector3(RoombaA.transform.position.x, 1, RoombaA.transform.position.z), new Vector3(RoombaA.transform.position.x, 0.5f, RoombaA.transform.position.z+(RoombaA.transform.lossyScale.z / 2)));
        if (Vector3.Distance(RoombaA.transform.position, RoombaB.transform.position) <= (RoombaA.transform.lossyScale.x/2) + (RoombaB.transform.lossyScale.x/2))
        {
            Debug.DrawLine(new Vector3(RoombaA.transform.position.x,1, RoombaA.transform.position.z), new Vector3(RoombaB.transform.position.x, 1, RoombaB.transform.position.z), Color.red);
            RoombaA.transform.position = RoombaALastPos;
            RoombaB.transform.position = RoombaBLastPos;
        }
        else
        {

            RoombaALastPos = RoombaA.transform.position;
            RoombaBLastPos = RoombaB.transform.position;
        }
    }
}
