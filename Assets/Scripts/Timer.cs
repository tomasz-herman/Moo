using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TimeSpan elapsedTime = TimeSpan.Zero;
    private bool doTick = true;

    void Update()
    {
        if(doTick)
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
    }

    public void SetTicking(bool tick) { doTick = tick; }

    public TimeSpan GetElapsedTime() { return elapsedTime; }
}
