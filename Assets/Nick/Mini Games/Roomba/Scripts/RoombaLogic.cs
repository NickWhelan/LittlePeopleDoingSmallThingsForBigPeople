using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaLogic : MonoBehaviour {
    public int TeamNumber;
    public GameObject[] PlayersOnTeam;
    public GameObject RoombaGround;
	// Use this for initialization
	void Start () {
        if (TeamNumber == 1)
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), PlayersOnTeam[0].GetComponent<CapsuleCollider>());
        }
        if (TeamNumber == 2)
        {
            Physics.IgnoreLayerCollision(9, 11);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayersOnTeam[0].transform.position.x + -(PlayersOnTeam[0].transform.localScale.x / 2) > RoombaGround.transform.position.x + (RoombaGround.transform.localScale.x / 2)) {
            PlayersOnTeam[0].transform.position = new Vector3(RoombaGround.transform.position.x + (RoombaGround.transform.localScale.x / 2) -(PlayersOnTeam[0].transform.localScale.x / 2),
                                                              RoombaGround.transform.position.y+ (RoombaGround.transform.localScale.y / 2), 
                                                              PlayersOnTeam[0].transform.position.z);
            print(transform.position.y);
        }
	}
}
