using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lab5Physycs.Utils;

public class WaterControl : MonoBehaviour
{
    [Header("Столбы воды")]
    public Transform WaterLeft;
    public Transform WaterRight;
    public int maxLineLength = 400;
    public int minLineLength = 39;
    public int startLineLength = 210;

    private float thisLength = 0;

    [Header("Настройка анимации")]
    public AnimationCurve ChangeCurve = new AnimationCurve(new Keyframe[] {
        new Keyframe(0, 0),
        new Keyframe(0.6f, 0.7f),
        new Keyframe(1, 1)
    });
    public float timeReset = 4;

    private AnimateValue animateValue;

    public void Start()
    {
        thisLength = startLineLength;
        animateValue = new AnimateValue();
    }

    public void Update()
    {
        if (!animateValue.IsEnd())
        {
            SetRightAndBackLeftLength(animateValue.GetValue());
        }
    }

    

    public void ResetAnimWaterAndMoveToDelta(float delta = 0, float time = -1)
    {
        StartAnimateMove(startLineLength, timeReset);
        animateValue.endAnimate.AddListener(() =>
        {
            StartAnimateMove(startLineLength + delta, time == -1 ? timeReset : time);
        });
    }

    public void StartAnimateMovePercentFromDelta(float percent, float time)
    {
        StartAnimateMove(thisLength + GetLastToStandartLenght() * percent, time);
    }

    private float GetLastToStandartLenght()
    {
        return Mathf.Abs(thisLength - startLineLength);
    }

    public void StartAnimateMove(float newLength, float time)
    {
        animateValue.StartAnimateValue(thisLength, newLength, time, ChangeCurve);
    }

    public void ResetWater()
    {
        SetRightAndBackLeftLength(startLineLength);
    }

    public void SetRightAndBackLeftLength(float newLength)
    {
        newLength = CheckLengthAndReplaceIfNoValid(newLength);


        WaterRight.localScale = NormalizeLength(newLength);
        WaterLeft.localScale = NormalizeLength(GetLeftLenghtFromRightLength(newLength));
        thisLength = newLength;
    }

    private Vector3 NormalizeLength(float LineLength)
    {
        float normalizedLength = GetBalancedLength(LineLength) / GetBalancedLength(startLineLength);

        return new Vector3(1, 1, normalizedLength);
    }

    private float GetBalancedLength(float length)
    {
        return length - minLineLength;
    }

    private float CheckLengthAndReplaceIfNoValid(float length)
    {
        if (!CheckValidityMaxLength(length))
        {
            length = GetBalancedMaxLengthToRightLine();
        }
        else if (!CheckValidityMinLength(length))
        {
            length = GetBalancedMinLengthToRightLine();
        }

        return length;
    }

    private bool CheckValidityMaxLength(float length)
    {
        return length < maxLineLength
            && GetLeftLenghtFromRightLength(length) > minLineLength;
    }

    private bool CheckValidityMinLength(float length)
    {
        return length > minLineLength
            && GetLeftLenghtFromRightLength(length) < maxLineLength;
    }

    private float GetBalancedMaxLengthToRightLine()
    {
        return Mathf.Min(
            GetMaxLenght(),
            maxLineLength);
    }

    private float GetBalancedMinLengthToRightLine()
    {
        return Mathf.Max(
            GetMinLenght(),
            minLineLength);
    }

    private float GetMaxLenght()
    {
        return maxLineLength - (minLineLength - GetLeftLenghtFromRightLength(maxLineLength));
    }

    private float GetMinLenght()
    {
        return minLineLength + (GetLeftLenghtFromRightLength(minLineLength) - maxLineLength);
    }

    private float GetLeftLenghtFromRightLength(float rightLength)
    {
        return startLineLength * 2 - rightLength;
    }

    public void StopMove()
    {
        animateValue.Stop();
    }

    public void ResumeMove()
    {
        animateValue.Resume();
    }

    public bool IsMove()
    {
        return !animateValue.IsEnd();
    }
}
