using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    public bool isCountingDown,isTimeUp;

    public float StartTime, EndTime, CurrentTime;

	// Use this for initialization

	public void Start () {
        CurrentTime = StartTime;
        isTimeUp = false;
	}
	
	// Update is called once per frame
	public void Update () {
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
            CurrentTime = EndTime;
        }
    }
    void CountDown() {
        CurrentTime -= Time.deltaTime;
        if (CurrentTime < EndTime)
        {
            isTimeUp = true;
            CurrentTime = EndTime;
        }
    }
}
