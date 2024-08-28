using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedwatch : MonoBehaviour
{
    public TextMeshPro SpeedWatchText, SpeedWatchTextMiliSec;
    public static float SpeedWatchMin = 0;
    public static float SpeedWatchSec = 0;
    [SerializeField] private double SpeedWatchMiliSec = 0;
    [SerializeField] private bool _isActiveTime = false;


    private void Update()
    {
      
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    _isActiveTime = !_isActiveTime;
                    SpeedWatchMin = 0;
                    SpeedWatchSec = 0;
                }
            

        if (_isActiveTime)
        {
            SpeedWatchView();
        }
    }

    public void SpeedWatchView()
    {
        SpeedWatchSec += Time.deltaTime;

        SpeedWatchMiliSec = System.Math.Round(SpeedWatchSec, 2);
        SpeedWatchMiliSec = SpeedWatchMiliSec % 1 * 100;

        SpeedWatchTextMiliSec.text = ((int)SpeedWatchMiliSec).ToString();

        if (SpeedWatchSec <= 9.5f)
        {
            if (SpeedWatchMin < 10)
            {
                SpeedWatchText.text = "0" + SpeedWatchMin.ToString() + ":0" +
                                      System.Math.Round(SpeedWatchSec, 0).ToString();
            }
            else
            {
                SpeedWatchText.text = SpeedWatchMin.ToString() + ":0" + System.Math.Round(SpeedWatchSec, 0).ToString();
            }
        }
        else if (SpeedWatchMin < 10)
        {
            SpeedWatchText.text = "0" + SpeedWatchMin.ToString() + ":" + System.Math.Round(SpeedWatchSec, 0).ToString();
        }
        else
        {
            SpeedWatchText.text = SpeedWatchMin.ToString() + ":" + System.Math.Round(SpeedWatchSec, 0).ToString();
        }


        if (SpeedWatchSec >= 59.5f)
        {
            SpeedWatchMin++;
            SpeedWatchSec = 0f;
        }
    }
}