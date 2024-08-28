using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lab5Physycs.Utils;
public class TimerController : MonoBehaviour
{

    private Text textTimer;
    private AnimationCurve timerCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private AnimateValue timerValue;

    // Start is called before the first frame update
    void Start()
    {
        timerValue = new AnimateValue();
        textTimer = GetComponent<Text>();
    }

    public void RemoveTimer()
    {
        timerValue.Reset();
    }

    public void StartTimer(float time)
    {
        timerValue.StartAnimateValue(time, 0, time, timerCurve);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerValue.GetValue() != 0)
        {
            textTimer.text = FormatTime(timerValue.GetValue());
        }
        else
        {
            textTimer.text = "";
        }
    }

    private string FormatTime(float timeInSecond)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSecond);
        return time.ToString(@"mm\:ss\:ff");
    }
}
