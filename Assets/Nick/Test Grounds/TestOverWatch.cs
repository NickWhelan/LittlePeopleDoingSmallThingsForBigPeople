using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOverWatch : MonoBehaviour {
    public List<GameObject> Cables, Plugs;
   public GameObject PlugPrefab, CablesPrefab;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < 4; i++) { }
        //Cables.Add(Instantiate(CablesPrefab));
        //Rope temp_rope = Cables[Cables.Count - 1].GetComponent<Rope>();
        //temp_rope.EndObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlugPrefab.transform.position = new Vector3(10, 6, 10);

            Plugs.Add(Instantiate(PlugPrefab));
            //UnityEditor.EditorApplication.isPaused = true;
            Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 5;
            Plugs[Plugs.Count - 1].name = "Plug 1";
            Cables[0].GetComponent<Rope>().PlugObj = Plugs[Plugs.Count - 1];
            Cables[0].GetComponent<Rope>().setup();

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlugPrefab.transform.position = new Vector3(10, 6, 10);

            Plugs.Add(Instantiate(PlugPrefab));
            //UnityEditor.EditorApplication.isPaused = true;
            Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 5;
            Plugs[Plugs.Count - 1].name = "Plug 1";
            Cables[1].GetComponent<Rope>().PlugObj = Plugs[Plugs.Count - 1];
            Cables[1].GetComponent<Rope>().setup();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlugPrefab.transform.position = new Vector3(10, 6, 10);

            Plugs.Add(Instantiate(PlugPrefab));
            //UnityEditor.EditorApplication.isPaused = true;
            Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 5;
            Plugs[Plugs.Count - 1].name = "Plug 1";
            Cables[2].GetComponent<Rope>().PlugObj = Plugs[Plugs.Count - 1];
            Cables[2].GetComponent<Rope>().setup();

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlugPrefab.transform.position = new Vector3(10, 6, 10);

            Plugs.Add(Instantiate(PlugPrefab));
            //UnityEditor.EditorApplication.isPaused = true;
            Plugs[Plugs.Count - 1].GetComponent<PlayerControlls>().PlayerNum = 5;
            Plugs[Plugs.Count - 1].name = "Plug 1";
            Cables[3].GetComponent<Rope>().PlugObj = Plugs[Plugs.Count - 1];
            Cables[3].GetComponent<Rope>().setup();

        }
    }
}
