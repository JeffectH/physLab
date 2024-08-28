using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatchLabThree : MonoBehaviour
{

    public TextMeshProUGUI SpeedWatchText;

    public float SpeedWatchSec = 0;
    [SerializeField] private double SpeedWatchMiliSec = 0;
    [SerializeField] private bool _isActiveTime = false;

    private void OnEnable()
    {
        Cargo.offFixStopWatch += ActiveStopWatchOff;
        InstalationThree.offFixStopWatch += ActiveStopWatchOn;
        InstalationThree.StopWatchReset += StopWatchReset;
    }

    private void OnDisable()
    {
        Cargo.offFixStopWatch -= ActiveStopWatchOff;
        InstalationThree.offFixStopWatch -= ActiveStopWatchOn;
        InstalationThree.StopWatchReset -= StopWatchReset;


    }
    private void Update()
    {
        if (_isActiveTime)
        {
            SpeedWatchView();
        }
    }

    private void ActiveStopWatchOn()
    {
        _isActiveTime = true;

        SpeedWatchSec = 0;
    }
    private void ActiveStopWatchOff()
    {

        _isActiveTime = false;
        SpeedWatchSec = 0;

    }
    private void StopWatchReset()
    {
        SpeedWatchSec = 0;
        SpeedWatchMiliSec = 0;
        SpeedWatchText.text = System.Math.Round(SpeedWatchSec, 0).ToString() + ":" + ((int)SpeedWatchMiliSec).ToString() + " c";
        _isActiveTime = false;
       

    }
    private void SpeedWatchView()
    {
        SpeedWatchSec += Time.deltaTime;
        SpeedWatchMiliSec = System.Math.Round(SpeedWatchSec, 2);
        SpeedWatchMiliSec = SpeedWatchMiliSec % 1 * 100;
        SpeedWatchText.text = Mathf.FloorToInt(SpeedWatchSec).ToString() + ":" + ((int)SpeedWatchMiliSec).ToString() + " c";

    }
}
