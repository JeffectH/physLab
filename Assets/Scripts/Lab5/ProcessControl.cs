using Lab5Physycs.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class ProcessControl : MonoBehaviour
{
    [Header("Базовые объекты")] public ButtonOnScript buttonPower;
    private GasController gasController;
    [SerializeField] private GameObject lamp;
    private WaterControl waterControl;
    private AudioSource workAuidoSource;

    [Header("Таймер")] public TimerController timerController;
    public float startTime;

    private bool isActive = false;
    private float cacheValue = 0;
    private float cachePercent = 0;
    private StateProcess state;

    private enum StateProcess
    {
        START,
        YELD_DROP,
        DROPPED
    }

    public void Start()
    {
        waterControl = GetComponent<WaterControl>();
        gasController = GetComponent<GasController>();
        workAuidoSource = GetComponent<AudioSource>();
        state = StateProcess.START;
        gasController.changeGas.AddListener(ChangeGas);
    }

    void Update()
    {
        if (isActive != lamp.activeSelf)
        {
            lamp.SetActive(isActive);
            workAuidoSource.mute = !isActive;
            SetWork();
        }
    }

    private void ChangeGas()
    {
        ResetMech();
    }

    private void ResetMech()
    {
        lamp.SetActive(false);
        isActive = false;
        workAuidoSource.mute = true;
        state = StateProcess.START;
        timerController.RemoveTimer();
    }

    public void ControllDropGas()
    {
        if (isActive)
            return;
        if (state != StateProcess.YELD_DROP)
        {
            gasController.DropFullGas();
            state = StateProcess.START;
            return;
        }
        else
        {
            state = StateProcess.DROPPED;
            gasController.DropPartialGas();
            timerController.StartTimer(startTime);
        }
    }

    public void StartSwitchActivity()
    {
        buttonPower.StartAnimate(SwitchActivityAndSetWork);
    }

    private void SwitchActivityAndSetWork(object sender, ElapsedEventArgs args)
    {
        isActive = !isActive;
    }

    private void SetWork()
    {
        GasController.Gas gas = gasController.GetGas();
        float newValue = gas.maxValue + RandomUtils.GetRandomValueAndZnak(gas.deltaMaxRandomValue);
        float newPercent = (gas.percentStop + RandomUtils.GetRandomValueAndZnak(gas.deltaMaxRandomPercent)) / 100.0f;

        if (gasController.IsClear())
        {
            cacheValue = newValue;
            cachePercent = newPercent;
        }
        else
        {
            newValue = cacheValue;
            newPercent = cachePercent;
        }

        if (!isActive)
        {
            waterControl.StartAnimateMovePercentFromDelta(
                -1 * newPercent,
                gas.timeBeforeDrop);
            timerController.StartTimer(startTime);
        }

        if (isActive)
        {
            waterControl.StartAnimateMove(
                newValue,
                gas.timeChange);
            state = StateProcess.YELD_DROP;
            gasController.FillTube();
        }
    }

    [Serializable]
    public class Gas
    {
        public string name;
        public float maxValue;
        public float percentStop;
        public float valueAfterDrop;
        public float deltaMaxRandomValue = 10;
        public float deltaMaxRandomPercent;
        public float deltaMaxRandomValueAfterDrop;
        public float timeChange = 10;
        public float timeBeforeDrop = 10;
        public float timeAfterDrop = 10;
    }
}