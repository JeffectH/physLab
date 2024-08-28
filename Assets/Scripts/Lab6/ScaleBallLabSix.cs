using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBallLabSix : MonoBehaviour
{
    public int NumScale = 0;

    private void Start()
    {
        NumScale = Random.Range(2, 5);
    }
}
