using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timePlay = 0f;
    public bool bossDie = false;

    private void FixedUpdate()
    {
        if (!bossDie)
        {
            timePlay += Time.fixedDeltaTime;
        }
    }

    public void SaveTimmer()
    {
        SaveSystem.SaveTimer(timePlay, bossDie);
    }
    
    public void LoadTimer()
    {
        (float time, bool bossDie) = SaveSystem.LoadTimer();
        timePlay = time;
        this.bossDie = bossDie;
    }
}
