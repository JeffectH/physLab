using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInstalationLabThree : MonoBehaviour
{
    public static bool IsPlayerInZone;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            IsPlayerInZone = true;
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            IsPlayerInZone = false;

        }
    }
}
