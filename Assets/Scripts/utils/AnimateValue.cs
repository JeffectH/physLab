using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lab5Physycs.Utils
{
    public class AnimateValue
    {
        private float newValue = -1;
        private float startTime = 0;
        private float time = -1;
        private float oldValue = 0;
        private AnimationCurve changeCurve;
        private bool isStop = false;
        private float stopTime = -1;

        public UnityEvent endAnimate;

        public void StartAnimateValue(float thisValue, float newValue, float time, AnimationCurve curve)
        {
            this.newValue = newValue;
            startTime = GetNowTimeInSeconds();
            oldValue = thisValue;
            this.time = time;
            changeCurve = curve;
            isStop = false;
            endAnimate = new UnityEvent();
        }

        public bool IsEnd()
        {
            if (newValue != -1 && GetLastTime() > time)
            {
                newValue = -1;
                endAnimate.Invoke();
            }
            return GetLastTime() > time;
        }
        public float GetValue()
        {
            if (newValue == -1 || IsEnd())
            {
                return 0;
            }

            return oldValue + GetMultiply() * GetDelta();
        }

        private float GetDelta()
        {
            return newValue - oldValue;
        }

        private float GetMultiply()
        {
            return changeCurve.Evaluate(GetLastTime() / time);
        }

        public void Stop()
        {
            stopTime = GetLastTime();
            isStop = true;
        }

        private float GetLastTime()
        {
            if (isStop) return stopTime;
            return GetNowTimeInSeconds() - startTime;
        }

        private float GetNowTimeInSeconds()
        {
            return Time.time;
        }

        public void Resume()
        {
            isStop = false;
        }

        public void Reset()
        {
            newValue = -1;
        }

    }
}
