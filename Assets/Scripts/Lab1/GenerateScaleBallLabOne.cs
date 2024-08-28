using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScaleBallLabOne : MonoBehaviour
{
    public int RandomValue;
    private float Value;
    private void Start()
    {
        RandomValue = Random.Range(1, 4);
        
    }
}
