using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public int int_NumberOfJoints;
  
    public GameObject JointObj,StartObj, EndObj,PlugObj;
    public Transform trans_CordParent;

    float float_LengthBettweenJoints;
    List<GameObject> list_Joint_Objs;
    List<CharacterJoint> list_Joints;
    LineRenderer line_Cord;
    // Use this for initialization
    void Start () {
        //setup();
    }

    public void setup() {
        list_Joint_Objs = new List<GameObject>();
        list_Joints = new List<CharacterJoint>();
       print(float_LengthBettweenJoints = 0.05f);
        print(Vector3.Distance(StartObj.transform.position, EndObj.transform.position));
       print(int_NumberOfJoints = (int)(Vector3.Distance(StartObj.transform.position, EndObj.transform.position) / float_LengthBettweenJoints));

        line_Cord = gameObject.AddComponent<LineRenderer>();
        line_Cord.material.color = Color.black;
        line_Cord.SetWidth(0.1f, 0.1f);

        line_Cord.numPositions = int_NumberOfJoints + 1;
        PlugObj.transform.position = transform.position;
        StartObj.transform.position = new Vector3(PlugObj.transform.position.x + (PlugObj.transform.localScale.x / 2), PlugObj.transform.position.y, PlugObj.transform.position.z);
       

        MakeJoints();
    }

    void MakeJoints() {

        list_Joint_Objs.Add(StartObj);

        float temp_X =0;

        for (int i = 1; i < int_NumberOfJoints; i++) {
            list_Joint_Objs.Add(Instantiate(JointObj));
            list_Joint_Objs[i].GetComponent<Rigidbody>().mass = 0.05f;
            list_Joint_Objs[i].GetComponent<Rigidbody>().drag = 2.5f;
            list_Joints.Add(list_Joint_Objs[i].AddComponent<CharacterJoint>());

            list_Joint_Objs[i].name = "Joint " + i;
            list_Joint_Objs[i].transform.position = new Vector3(temp_X, transform.position.y, transform.position.z);

            line_Cord.SetPosition(i, list_Joint_Objs[i].transform.position);

            
            list_Joint_Objs[i].transform.parent = trans_CordParent;
            temp_X += float_LengthBettweenJoints;

            list_Joints[i -1].anchor = new Vector3(temp_X - float_LengthBettweenJoints, list_Joint_Objs[i].transform.position.y, list_Joint_Objs[i].transform.position.z);
            list_Joints[i-1].connectedBody = list_Joint_Objs[i-1].GetComponent<Rigidbody>();

            list_Joints[i-1].anchor = new Vector3(list_Joint_Objs[i].transform.position.x - list_Joint_Objs[i].transform.position.x, 0, 0);
            list_Joints[i - 1].autoConfigureConnectedAnchor = false;
            list_Joints[i-1].connectedAnchor = Vector3.zero;
            list_Joints[i - 1].swingAxis = new Vector3(1, 1, 1);
            list_Joints[i - 1].enableCollision = true;
            


        }

        list_Joint_Objs.Add(EndObj);
        list_Joints.Add(EndObj.GetComponent<CharacterJoint>());
        list_Joints[list_Joints.Count-1].connectedBody = list_Joint_Objs[list_Joint_Objs.Count - 2].GetComponent<Rigidbody>();
        list_Joints[list_Joints.Count-1].anchor = new Vector3(list_Joint_Objs[list_Joints.Count -2].transform.position.x - list_Joint_Objs[list_Joints.Count-1].transform.position.x, 0, 0);
    }
    public void ChangeColor(Color color) {

        line_Cord.material.color = color;
    }
    void FixedUpdate() {
        if (PlugObj)
        {
            StartObj.transform.position = new Vector3(PlugObj.transform.position.x + (PlugObj.transform.localScale.x / 2), PlugObj.transform.position.y, PlugObj.transform.position.z);
            for (int i = 0; i < list_Joint_Objs.Count; i++)
            {
                line_Cord.SetPosition(i, list_Joint_Objs[i].transform.position);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        
	}
}
