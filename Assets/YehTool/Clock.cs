using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour {
    static int currentID=0;
    int id;
    bool startDelay = false;
    float delayTime;
    float startTime;
    Action action;
	// Use this for initialization
	void Start () {

    }
	
    public static void Delay(float delayTime, Action action)
    {
        GameObject go = Resources.Load<GameObject>("Clock");
        Clock clock = GameObject.Instantiate<GameObject>(go).GetComponent<Clock>();
        currentID++;
        clock.Delay(currentID, delayTime, action);
    }

    void Delay(int id, float delayTime,Action action)
    {
        this.id = id;
        this.action = action;
        this.delayTime = delayTime;
        this.startTime = Time.time;
        startDelay = true;
    }

	// Update is called once per frame
	void Update () {
        if (startDelay)
        {
            UIDebug.Log(id+10, "Delay count down time : "+(startTime+delayTime-Time.time));
            if(Time.time>startTime+ delayTime)
            {
                action();
                UIDebug.RemoveLog(id + 10);
                Destroy(gameObject);
            }
        }
	}
}
