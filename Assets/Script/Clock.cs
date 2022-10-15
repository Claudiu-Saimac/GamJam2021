using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public event Action OnTimeEnded;

    public float TimeRemaining;
    public float StartTime;
    public float CountTime;
    public bool Started = false;

    public Image Img;

    public void StartClock(float time)
    {
        CountTime = 0;
        StartTime = time;
        Started = true;

        TimeRemaining = time;
    }

    public void Update()
    {
        if (!Started) return;

        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            CountTime = StartTime - TimeRemaining;

            DrawTime();
        }
        else
        {
            Started = false;
            OnTimeEnded?.Invoke();
        }
    }

    public void Stop()
    {
        Started = false;
    }

    private void DrawTime()
    {
        Img.fillAmount = CountTime/StartTime;
    }
}
