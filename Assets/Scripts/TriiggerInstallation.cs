
using UnityEngine;

public class TriiggerInstallation : MonoBehaviour
{
    [SerializeField] private MeshRenderer _ballPhantom;

    public static bool IsPlayerInZone;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            IsPlayerInZone = true;
            if (ObjectMove.Instance.Target != null)
            {


                _ballPhantom.enabled = true;
            }

            if (InstallationSimulationOne._installationMode)
            {
                _ballPhantom.enabled = false;

            }

        }
    }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<FirstPersonControllerTim>())
            {
                IsPlayerInZone = false;

                {
                    _ballPhantom.enabled = false;
                }
            }
        }

    }
