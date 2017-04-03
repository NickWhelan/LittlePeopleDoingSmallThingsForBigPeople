using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildToast : MonoBehaviour {

    public GameObject BreadCube;
    public List<GameObject> BreadCubes;
    public Transform BreadCubesParent;

    public int BreadWidth, BreadHeight;



	// Use this for initialization
	void Start () {

        float startX = transform.position.x - (BreadWidth/2) + 0.5f;
        float startY = transform.position.y + (BreadHeight/2) + 0.5f;
        int w = 0;
        for (int i = 0; i < BreadWidth * BreadHeight; i++) {
            BreadCubes.Add(Instantiate(BreadCube));
            BreadCubes[i].transform.parent = BreadCubesParent;
            BreadCubes[i].name = "BreadCube ("+i +") "+ (int) startX + " " + (int) startY;
            BreadCubes[i].tag = "Bread";
            BreadCubes[i].transform.position = new Vector3(startX, startY, transform.position.z);
            BreadCubes[i].transform.localScale = new Vector3(1, 1, 1);

            if (this.gameObject.layer == LayerMask.NameToLayer("Team 1")) {
                BreadCubes[i].GetComponent<BoxCollider>().center = new Vector3(0, 0,0);
            }

            startX += BreadCubes[i].transform.localScale.x;
            if (w >= BreadWidth-1)
            {
                w = 0;
                startX = transform.position.x - (BreadWidth / 2) + 0.5f;
                startY -= BreadCubes[i].transform.localScale.y;
            }
            else {
                w++;
            }
            
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
