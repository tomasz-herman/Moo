using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public TMP_Text text;
    
    public void SetTime(TimeSpan ts)
    {
        if (ts.TotalHours < 1)
            text.text = ts.ToString("m:ss");
        else
            text.text = ts.ToString("H:mm:ss");
    }
}
