using System;
using UnityEngine;
using UnityEngine.UI;

public class MethodStoksa : MonoBehaviour
{
    public static Action onModeMethodStoks;

    [SerializeField] private Transform _ballPoint;
    private GameObject _actionObject;

    //public TemperatureValue Temperature;
    //public Toggle Glicerin;
    //public Toggle Maslo;

    public MethodStoksa methodStoksa;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ColbTrigger.PlayerInZone)
        {
            onModeMethodStoks?.Invoke();

            if (ObjectMove.Instance.Target != null)
            {
                MoveBall();
            }
            else
            {
                MoveBallToDefault();
            }
        }
    }

    private void MoveBall()
    {
        _actionObject = ObjectMove.Instance.Target;
        ObjectMove.Instance.DropObject();
        _actionObject.GetComponent<Rigidbody>().isKinematic = true;
        _actionObject.transform.position = _ballPoint.position;
    }

    private void MoveBallToDefault()
    {
        if (_actionObject != null)
        {
            _actionObject.GetComponent<Rigidbody>().isKinematic = true;
            ObjectMove.Instance.GrabObject(_actionObject);
        }
    }

    public void StartInstallation()
    {
        if (_actionObject != null)
        {
            _actionObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void ResetBall()
    {
        _actionObject.GetComponent<Rigidbody>().isKinematic = true;
        _actionObject.transform.position = _ballPoint.position;
    }
}
