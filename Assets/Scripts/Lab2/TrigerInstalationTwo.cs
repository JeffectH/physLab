using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerInstalationTwo : MonoBehaviour
{
    [SerializeField] private MeshRenderer _ballPhantom1;
    [SerializeField] private MeshRenderer _ballPhantom2;

    public static bool IsPlayerInZone;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            IsPlayerInZone = true;
            if (ObjectMove.Instance.Target != null)
            {
                _ballPhantom1.enabled = true;

                _ballPhantom2.enabled = true;
            }
            if (InstallationSimulationTwo._ballOneActive || ObjectMove.Instance.Target == null && !InstallationSimulationTwo._ballOneActive)
            {
                _ballPhantom1.enabled = false;

            }
            if (InstallationSimulationTwo._ballTwoActive || ObjectMove.Instance.Target == null && !InstallationSimulationTwo._ballTwoActive)
            {
                _ballPhantom2.enabled = false;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            IsPlayerInZone = false;

            {
                _ballPhantom1.enabled = false;
                _ballPhantom2.enabled = false;
            }
        }
    }
}
