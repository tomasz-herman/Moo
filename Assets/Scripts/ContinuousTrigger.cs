using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//¿ywcem zakoszone z in¿ynierki
public class ContinuousTrigger
{
    private float triggerTimeout = 0;
    private float excessTime = 0;

    //we do not store excess time when there was no pull trigger attempt in the last game tick
    private bool pulledTriggerAfterDecrease;

    /// <summary>
    /// This method decreases the trigger timeout by the specified time delta.
    /// If the timeout passes it keeps track of the excess time that has passed since then.
    /// </summary>
    /// <param name="timeDelta">The amount of time to decrease the timeout in seconds</param>
    public void DecreaseTime(float timeDelta)
    {
        if (timeDelta < 0)
            timeDelta = 0;
        //if we have some excess time we use it as a longer time delta for compensation
        if (pulledTriggerAfterDecrease)
            timeDelta += excessTime;

        float remainingTime = triggerTimeout - timeDelta;
        triggerTimeout = Mathf.Max(0, remainingTime);
        excessTime = Mathf.Max(0, -remainingTime);
        pulledTriggerAfterDecrease = false;
    }

    /// <summary>
    /// This method attempts to discharge the trigger if the trigger timeout has passed.
    /// If there was excess time buildup since last DecreaseTime() call the new trigger timeout
    /// will be shorter by that much.
    /// It is possible that the timeout will be short enough for the trigger to discharge more than once.
    /// </summary>
    /// <param name="timeout">The trigger timeout after discharge in seconds</param>
    /// <returns>The number of discharges that have occured</returns>
    public int PullTrigger(float timeout)
    {
        pulledTriggerAfterDecrease = true;
        if (triggerTimeout != 0)
            return 0;
        if (excessTime == 0)
        {
            triggerTimeout = timeout;
            return 1;
        }
        int dischargeCount = 0;
        while (excessTime >= 0)
        {
            dischargeCount++;
            excessTime -= timeout;
        }
        //excessTime is now negative and shows how much time will actually remain since last discharge
        triggerTimeout = -excessTime;
        excessTime = 0;
        return dischargeCount;
    }

    /// <summary>
    /// Restarts the trigger and sets timeout to zero
    /// </summary>
    public void Zero()
    {
        triggerTimeout = 0;
        excessTime = 0;
        pulledTriggerAfterDecrease = false;
    }
}