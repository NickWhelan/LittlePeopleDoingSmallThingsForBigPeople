using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {


    public GameObject JointObj,StartObj, EndObj,PlugObj;
    public Transform trans_CordParent;
    public Material material;

    int int_NumberOfJoints;
    float float_LengthBettweenJoints;
    List<GameObject> list_Joint_Objs;
    List<HingeJoint> list_Joints;
    LineRenderer line_Cord;
    // Use this for initialization
    void Start () {
        //setup();
    }

    public void setup() {
        list_Joint_Objs = new List<GameObject>();
        list_Joints = new List<HingeJoint>();

        line_Cord = gameObject.AddComponent<LineRenderer>();
        line_Cord.material.color = Color.black;
        line_Cord.SetWidth(0.1f, 0.1f);
        line_Cord.material = material;
        line_Cord.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;

        float_LengthBettweenJoints = 0.05f;
        int_NumberOfJoints = (int)( Vector3.Distance(StartObj.transform.position, EndObj.transform.position) / float_LengthBettweenJoints);
        line_Cord.numPositions = int_NumberOfJoints + 1;
        transform.position = PlugObj.transform.position;
        StartObj.transform.position = new Vector3(PlugObj.transform.position.x + (PlugObj.transform.localScale.x / 2), PlugObj.transform.position.y, PlugObj.transform.position.z);
       

        MakeJoints();
    }

    public void setupJetPack()
    {
        list_Joint_Objs = new List<GameObject>();
        list_Joints = new List<HingeJoint>();

        line_Cord = gameObject.AddComponent<LineRenderer>();
        line_Cord.material.color = Color.black;
        line_Cord.SetWidth(0.1f, 0.1f);
        line_Cord.material = material;

        //StartObj.transform.position = PlugObj.transform.position;
        float_LengthBettweenJoints = 0.005f;
        int_NumberOfJoints = (int)(Vector3.Distance(StartObj.transform.position, EndObj.transform.position) / float_LengthBettweenJoints);
        //int_NumberOfJoints *= 2;
        
        line_Cord.useWorldSpace = false;

        MakeJointsJetPack();
    }

    void MakeJointsJetPack()
    {

        list_Joint_Objs.Add(StartObj);


        float temp_X = 0;

        for (int i = 1; i < int_NumberOfJoints; i++)
        {
            list_Joint_Objs.Add(Instantiate(JointObj));
            list_Joint_Objs[i].GetComponent<Rigidbody>().mass = 0.05f;
            list_Joint_Objs[i].GetComponent<Rigidbody>().drag = 2.5f;
            list_Joint_Objs[i].layer = LayerMask.NameToLayer("RopeJoint");
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("RopeJoint"), LayerMask.NameToLayer("RopeJoint"));
            list_Joint_Objs[i].name = "Joint " + i;
            list_Joint_Objs[i].transform.position = new Vector3(temp_X, transform.position.y, transform.position.z);

            list_Joints.Add(list_Joint_Objs[i].AddComponent<HingeJoint>());

            list_Joint_Objs[i].transform.parent = trans_CordParent;
            list_Joints[i-1].connectedBody = list_Joint_Objs[i-1].GetComponent<Rigidbody>();
            list_Joints[i-1].autoConfigureConnectedAnchor = false;
            list_Joints[i - 1].anchor = Vector3.zero;
            list_Joints[i - 1].connectedAnchor = Vector3.zero;
            list_Joints[i - 1].axis = new Vector3(1, 1, 1);
            //list_Joints[i - 1].enableCollision = true;
            temp_X += float_LengthBettweenJoints;
        }

        list_Joint_Objs.Add(EndObj);
        list_Joints.Add(EndObj.GetComponent<HingeJoint>());
        list_Joints[list_Joints.Count - 1].connectedBody = list_Joint_Objs[list_Joint_Objs.Count - 2].GetComponent<Rigidbody>();
        list_Joints[list_Joints.Count - 1].autoConfigureConnectedAnchor = false;
        list_Joints[list_Joints.Count - 1].anchor = Vector3.zero;
        list_Joints[list_Joints.Count - 1].connectedAnchor = Vector3.zero;
        list_Joints[list_Joints.Count - 1].axis = new Vector3(1, 1, 1);

        line_Cord.numPositions = list_Joints.Count-1;
        for (int i = 0; i < list_Joints.Count - 1; i++)
        {
            line_Cord.SetPosition(i, list_Joint_Objs[i].transform.localPosition);
        }
    }
        void MakeJoints() {

        list_Joint_Objs.Add(StartObj);

        float temp_X =0;

        for (int i = 1; i < int_NumberOfJoints; i++) {
            list_Joint_Objs.Add(Instantiate(JointObj));
            list_Joint_Objs[i].GetComponent<Rigidbody>().mass = 0.05f;
            list_Joint_Objs[i].GetComponent<Rigidbody>().drag = 2.5f;
            list_Joints.Add(list_Joint_Objs[i].AddComponent<HingeJoint>());

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
            list_Joints[i - 1].axis = new Vector3(1, 1, 1);
            list_Joints[i - 1].enableCollision = true;
        }

        list_Joint_Objs.Add(EndObj);
        list_Joints.Add(EndObj.GetComponent<HingeJoint>());
        list_Joints[list_Joints.Count-1].connectedBody = list_Joint_Objs[list_Joint_Objs.Count - 2].GetComponent<Rigidbody>();
        list_Joints[list_Joints.Count - 1].anchor = new Vector3(list_Joint_Objs[list_Joints.Count - 1].transform.position.x - list_Joint_Objs[list_Joints.Count - 1].transform.position.x, 0, 0);
        list_Joints[list_Joints.Count - 1].autoConfigureConnectedAnchor = false;
        list_Joints[list_Joints.Count - 1].connectedAnchor = Vector3.zero;
        list_Joints[list_Joints.Count - 1].axis = new Vector3(1, 1, 1);
        //list_Joints[list_Joints.Count-1].anchor = new Vector3(list_Joint_Objs[list_Joints.Count -2].transform.position.x - list_Joint_Objs[list_Joints.Count-1].transform.position.x, 0, 0);
    }

  void FixedUpdate() {
        for (int i = 0; i < list_Joints.Count - 1; i++)
        {
            line_Cord.SetPosition(i, list_Joint_Objs[i].transform.localPosition);
        }

        if (PlugObj && list_Joint_Objs != null)
        {
            StartObj.transform.position = new Vector3(PlugObj.transform.position.x + (PlugObj.transform.localScale.x / 2), PlugObj.transform.position.y, PlugObj.transform.position.z);
            for (int i = 0; i < list_Joints.Count - 1; i++)
            {
                line_Cord.SetPosition(i, list_Joint_Objs[i].transform.localPosition);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        
	}
}
