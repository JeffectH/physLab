using UnityEngine;
using TMPro;

public class StopWatchLabSix : MonoBehaviour
{
    public TextMeshProUGUI SpeedWatchText;

    public float SpeedWatchSec = 0;
    public double SpeedWatchSec2 = 0;

    [SerializeField] private double SpeedWatchMiliSec = 0;
    [SerializeField] private bool _isActiveTime = false;

    private void OnEnable()
    {
        FixStopWatchTrigger.onFixStopWatch += ActiveStopWatch;
    }

    private void OnDisable()
    {
        FixStopWatchTrigger.onFixStopWatch -= ActiveStopWatch;
    }

    private void Update()
    {
        if (_isActiveTime)
        {
            SpeedWatchView();
        }
    }

    private void ActiveStopWatch() 
    {
        _isActiveTime = !_isActiveTime;

        SpeedWatchSec = 0;
        SpeedWatchSec2 = 0;
    }

    private void SpeedWatchView()
    {
        SpeedWatchSec += Time.deltaTime;

        SpeedWatchMiliSec = System.Math.Round(SpeedWatchSec, 2);

        SpeedWatchMiliSec = SpeedWatchMiliSec % 1 * 100;

        SpeedWatchSec2 += SpeedWatchSec;

        SpeedWatchText.text = Mathf.Ceil(SpeedWatchSec) + ":" + ((int)SpeedWatchMiliSec).ToString() + " c";
    }
}
