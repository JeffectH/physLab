using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMove : MonoBehaviour
{
    public static ObjectMove Instance { get; private set; }
    
    [SerializeField] private Transform _pointMagnet;
    [SerializeField] private Camera _camera;

   //private GameObject _actionObject;
    private Vector3 _pointScreen;

    public GameObject Target;
    public bool IsGetObject, State;
    public MeshRenderer _renderer;
    private void Awake()
    {
        Instance = this;
        State = false;
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (State)
        {
            GrabObject(Target.transform, Target.GetComponent<Rigidbody>());
        }

        if (Interactable.Instance.is2DRay && !CaliperMode.ModeActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Interactable.Instance.ActionObject != null)
                {
                    if (Interactable.Instance.ActionObject.GetComponent<InteractableObjects>() != null)
                    {
                        Target = Interactable.Instance.ActionObject;
                       
                            State = true;
                        
                    }
                  
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (Interactable.Instance.ActionObject.GetComponent<InteractableObjects>() != null)
                {
                    State = false;
                    DropObject();
                }
            }
        }
        else if (!CaliperMode.ModeActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Interactable.Instance.ActionObject != null)
                {
                    if (Interactable.Instance.ActionObject.GetComponent<InteractableObjects>() != null)
                    {
                        //if(InstallationSimulationOne.Activate)
                        Target = Interactable.Instance.ActionObject;

                        if (Target.GetComponent<InteractableObjects>().IsMoveSphere)
                        {
                            if (IsGetObject)
                            {
                                DropObject();
                            }
                            else
                            { 
                                GrabObject();   
                            }
                        }
                    } 
                }

            }

        }
    }
    public void GrabObject()
    {
  //      Debug.Log("Grab");

        Target.GetComponent<Rigidbody>().isKinematic = true;
        Target.transform.position = _pointMagnet.transform.position;
        Target.transform.parent = _pointMagnet.transform;
        IsGetObject = true;

    }
    public void GrabObject(GameObject actionObject)
    {
    //    Debug.Log("Grab: go");
        
        Target = actionObject;

        actionObject.GetComponent<Rigidbody>().isKinematic = true;
        var transform1 = _pointMagnet.transform;
        actionObject.transform.position = transform1.position;
        actionObject.transform.parent = transform1;
        
        IsGetObject = true;
    }
    public void DropObject()
    {
    //    Debug.Log("Drop:"+Target.name);
        if (Target.GetComponent<Rigidbody>())
        {
            Target.GetComponent<Rigidbody>().isKinematic = false;
            Target.transform.parent = null;
            Target = null;
          //  Debug.Log("Drop");
            IsGetObject = false;
        }
    }




    public void GrabObject(Transform actionObject)
    {
        var position1 = actionObject.position;
        Vector3 position = new Vector3(

               Input.mousePosition.x,
               Input.mousePosition.y,
               _camera.WorldToScreenPoint(position1).z);

        Vector3 worldPosition = _camera.ScreenToWorldPoint(position);

        position1 = new Vector3(worldPosition.x, position1.y, position1.z);
        actionObject.position = position1;

        Target = actionObject.gameObject;
    }

    public void DropObject(Transform ActionObject)
    {
  //      Debug.Log("drop: tr");
        Target = null;
        IsGetObject = false;

    }


    public void GrabObject(Transform actionObject, Rigidbody rigidbody)
    {
        rigidbody.isKinematic = true;

        _pointScreen = _camera.WorldToScreenPoint(actionObject.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _pointScreen.z);
        Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenPoint);
        actionObject.position = curPosition;

  

        Target = actionObject.gameObject;
//        Debug.Log("Grab: Tr+Rg = "+Target.name);
        IsGetObject = true;

    }

    public void DropObject(Transform ActionObject, Rigidbody rigidbody)
    {
        rigidbody.isKinematic = false;
        Target = null;
    }
}

