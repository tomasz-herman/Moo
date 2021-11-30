using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public TMP_Text text;
    public Timer timer;
    
    void Update()
    {
        TimeSpan ts = timer.GetElapsedTime();
        if (ts.TotalHours < 1)
            text.text = ts.ToString(@"m\:ss");
        else
            text.text = ts.ToString(@"h\:mm\:ss");
    }
}
