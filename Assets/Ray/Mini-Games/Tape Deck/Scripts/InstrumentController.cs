using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour
{
    public string[] s_Instruments;

    [SerializeField]
    private GameObject[] emissionObjs;

    private int activeInstruments = 0;

    private AudioSource audSource;
    // Use this for initialization
    void Start()
    {
        emissionObjs = FindGameObjectsWithLayer(14);
        audSource = GameObject.FindGameObjectWithTag("Swapper").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < s_Instruments.Length; i++)
        {
            if (other.name.Contains(s_Instruments[i]))
            {
                Debug.Log(s_Instruments[i] + " is currently in the Trigger Box");
                emissionObjs[i].SetActive(true);

                activeInstruments += 1;
                Debug.Log("Active Instruments: " + activeInstruments);
                //Turn lights on that are attached to this object
            }
            if (activeInstruments / 2 == 4)
            {
                Debug.Log("All Instruments plugged in");
                audSource.PlayDelayed(0);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < s_Instruments.Length; i++)
        {
            if (other.name.Contains(s_Instruments[i]))
            {
                Debug.Log(s_Instruments[i] + " is no longer in the Trigger Box");
                emissionObjs[i].SetActive(false);
                //Turn lights on that are attached to this object
                activeInstruments -= 1;
            }
            if (activeInstruments / 2 != 4)
            {
                audSource.Stop();
            }
        }
    }

    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] ListOfAllGameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> listofLayeredObjects = new List<GameObject>();

        for (int i = 0; i < ListOfAllGameObjects.Length; i++)
        {
            if (ListOfAllGameObjects[i].layer == layer)
            {
                listofLayeredObjects.Add(ListOfAllGameObjects[i]);
                ListOfAllGameObjects[i].SetActive(false);
            }

        }
        return listofLayeredObjects.ToArray();
    }
}
