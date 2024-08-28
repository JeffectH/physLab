using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEngine;

public class ButtonOnScript : MonoBehaviour
{
    private readonly float deltaY = 0.015f;
    private readonly float time = 700;
    private Timer timer;
    private bool isActive = false;
    private bool isBlock = false;

    public void Start()
    {
        timer = CreateNewTimer();
    }

    public void Update()
    {
        if(isActive)
        {
            StopAnimate();
        }
    }

    private void StopAnimate()
    {
        isActive = false;
        isBlock = false;
        transform.Translate(Vector3.forward * deltaY);
        timer = CreateNewTimer();
    }

    public void StartAnimate(ElapsedEventHandler actionToEndAnimation)
    {
        if (isBlock)
        {
            return;
        }

        timer.Elapsed += actionToEndAnimation;
        timer.Start();
        isBlock = true;

        transform.Translate(Vector3.back * deltaY);
    }

    private Timer CreateNewTimer()
    {
        Timer newTimer = new Timer(time);
        newTimer.AutoReset = false;
        newTimer.Elapsed += StopTimer;
        return newTimer;
    }

    private void StopTimer(object sender, ElapsedEventArgs e)
    {
        isActive = true;
    }
}
