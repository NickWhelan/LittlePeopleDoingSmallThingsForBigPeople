using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public bool isCountingDown,isTimeUp;

    public float StartTime, EndTime, CurrentTime;

	// Use this for initialization
	void Start () {
        CurrentTime = StartTime;
        isTimeUp = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isCountingDown)
        {
            CountDown();
        }
        else {
            CountUp();
        }
	}

    void CountUp() {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > EndTime) {
            isTimeUp = true;
        }
    }
    void CountDown() {
        CurrentTime -= Time.deltaTime;
        if (CurrentTime < EndTime)
        {
            isTimeUp = true;
        }
    }
}
