using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    [SerializeField] private float _moveMax;
    [SerializeField] private float _moveMin;

    private Transform _instalation;

    private int _countHeight;

    public Image h1, h2;
    private void Start()
    {
        _instalation = transform.GetChild(0).transform;
        HeightChange(0);
        h1.color = Color.green;
        h2.color = Color.white;
    }

    private void Update()
    {
        if (_countHeight == 0)
        {
           
        _instalation.position = Vector3.MoveTowards(_instalation.position,
            new Vector3(_instalation.position.x, _moveMin, _instalation.position.z), 2f * Time.deltaTime); 
        h1.color = Color.green;
        h2.color = Color.white;
        }

        if (_countHeight == 1)
        {
            _instalation.position = Vector3.MoveTowards(_instalation.position,
                new Vector3(_instalation.position.x, _moveMax, _instalation.position.z), 2f*Time.deltaTime);   
            h2.color = Color.green;
            h1.color = Color.white;
        }

    }
    
    public void HeightChange(int MoveStep)
    {
        if (MoveStep == 0)
        {
            _countHeight = MoveStep;
        }
        if (MoveStep == 1)
        {
            _countHeight = MoveStep;
        } 
    }
}
