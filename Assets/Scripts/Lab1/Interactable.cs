using UnityEngine;
using UnityEngine.SceneManagement;


public class Interactable : MonoBehaviour
{
    public static Interactable Instance { get; private set; }

    private float _rayDistance = 10f;

    private GameObject _previousInteracteble;

    public GameObject ActionObject { get; private set; }
    public bool is2DRay = false;

    [SerializeField] private Camera _cameraCaliper;
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
                if (CaliperMode.ModeActive)
                {
                    ray = _cameraCaliper.ScreenPointToRay(Input.mousePosition);
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
            if (ObjectInteraction.GetComponent<InteractableObjects>().IsMoveSphere)
            {
                if (ObjectInteraction != _previousInteracteble)
                {
                    if (_previousInteracteble != null)
                        _previousInteracteble.GetComponent<Outline>().enabled = false;

                    ObjectInteraction.GetComponent<Outline>().enabled = true;

                    _previousInteracteble = ObjectInteraction;
                }
            }
        }
        else if (_previousInteracteble != null)
        {
            _previousInteracteble.GetComponent<Outline>().enabled = false;
            _previousInteracteble = null;
        }
    }
}