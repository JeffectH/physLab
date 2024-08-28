using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLabTwo : MonoBehaviour
{
    public static InteractableLabTwo Instance { get; private set; }

    private float _rayDistance = 5f;

    private GameObject _previousInteracteble;

    public GameObject ActionObject { get; private set; }
    public bool is2DRay;

    [SerializeField] private Camera _cameraMicrometer;
    [SerializeField] private Camera _cameraInstalation;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ActionObject = DiscoveredObject();
        OutlineOnOff(DiscoveredObject());
    }


    private GameObject DiscoveredObject()
    {
        RaycastHit hit;
        Ray ray;
        if (is2DRay)
        {
            if (MicrometerMode.ModeActive)
            {
                ray = _cameraMicrometer.ScreenPointToRay(Input.mousePosition);

            }
            else
            {
                ray = _cameraInstalation.ScreenPointToRay(Input.mousePosition);
            }

        }
        else
        {
            ray = new Ray(transform.position, transform.forward);
        }

        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider != null)
            {
                return hit.transform.gameObject;
            }
        }
        return null;
    }


    private void OutlineOnOff(GameObject ObjectInteraction)
    {

        if (ObjectInteraction != null && ObjectInteraction.GetComponent<Outline>() != null)
        {
            if (ObjectInteraction != _previousInteracteble)
            {
                if (_previousInteracteble != null)
                    _previousInteracteble.GetComponent<Outline>().enabled = false;


                ObjectInteraction.GetComponent<Outline>().enabled = true;

                _previousInteracteble = ObjectInteraction;
            }
        }
        else if (_previousInteracteble != null)
        {
            _previousInteracteble.GetComponent<Outline>().enabled = false;
            _previousInteracteble = null;
        }
    }
}
