using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    private Text timeText;
    public List<TimeConfig> timeConfigs = new List<TimeConfig>();
    private int timeIndex = 0;

    void Start()
    {
        timeText = GetComponent<Text>();
        if(timeConfigs.Count < 1)
        {
            timeIndex = -1;
        }
    }

    public void ResetTime()
    {
        if (timeConfigs.Count > 0)
        {
            timeIndex = 0;
            UpdateTimeAndText();
        }
    }

    public void NextTime()
    {
        if(timeIndex == -1)
        {
            return;
        }

        timeIndex = BalanceTimeIndex();


        UpdateTimeAndText();
    }

    private void UpdateTimeAndText()
    {
        timeText.text = timeConfigs[timeIndex].text;
        Time.timeScale = timeConfigs[timeIndex].value;
    }

    private int BalanceTimeIndex()
    {
        if (timeConfigs.Count - 1 == timeIndex)
        {
            return 0;
        }
        else
        {
            return timeIndex + 1;
        }
    }

    [Serializable]
    public class TimeConfig
    {
        public string text;
        public int value;
    }
}
