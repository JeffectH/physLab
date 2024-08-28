using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixStopWatchTrigger : MonoBehaviour
{
    public static Action onFixStopWatch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObjects>()) 
        {
            onFixStopWatch.Invoke();
        }
    }
}
