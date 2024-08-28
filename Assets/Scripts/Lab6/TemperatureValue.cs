using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureValue : MonoBehaviour
{

    [SerializeField] private float _maxTemperature = 90f;
    [SerializeField] private TextMeshProUGUI _textValue;
    public static float Value;
    private float _speed;
    public float _speed2;
    public void TemperatureChange(float speed)
    {
        _speed = speed;
    }


    public void ValueChange() 
    {
        

      
    }

    private void Update()
    {
        Value = Mathf.Lerp(0, _maxTemperature, _speed);
        _textValue.text = Mathf.Round(Value).ToString() + " °C";

    }
}
