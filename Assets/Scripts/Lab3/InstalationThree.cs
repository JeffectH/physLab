using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InstalationThree : MonoBehaviour
{
    public static InstalationThree instance { get; private set; }

    public static Action offFixStopWatch;
    public static Action StopWatchReset;
    public static Action ConnectCargo;

    public GameObject _activeCargo;
    public bool _isCargo;
    public bool _isMagnitActiv;
    public bool _isMoveCargoToMagnit;

    [SerializeField] private Camera _cameraMode1;
    [SerializeField] private Camera _cameraMode2;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private FirstPersonControllerTim _firstPersonController;
    [SerializeField] private GameObject _uiInstallationMode1;
    [SerializeField] private GameObject _uiInstallationMode2;
    [SerializeField] private GameObject _uiInstallation;
    [SerializeField] private GameObject _shtift;
    [SerializeField] private GameObject _rama;
    [SerializeField] private MeshRenderer _fantomCargo;
    [SerializeField] private Button _magnitButton;
    [SerializeField] private Button _connectedCargoButton;
    [SerializeField] private float _speedMoveCargo = 5f;
    [SerializeField] private CableComponentDefault _cableComponent;
    [SerializeField] private Image _aim;
    [SerializeField] private List<GameObject> _enviroments = new List<GameObject>();

    private bool _isCargoActiv;
    private bool _isCargoDisconected;
    private bool _isLabMode2;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {

        if (TriggerInstalationLabThree.IsPlayerInZone)
        {
            if (!_isLabMode2)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (!(ObjectMove.Instance.Target != null && _isCargoActiv))
                    {


                        SwitchingMode();
                        if (_isCargoDisconected && !_isCargoActiv && _isCargo)
                        {
                            DisconectedCargo();
                        }
                        else
                        {

                            if (ObjectMove.Instance.Target != null)
                            {
                                if (ObjectMove.Instance.Target.GetComponent<Cargo>())
                                {
                                    ConnectedCargo();
                                }
                            }
                        }
                    }
                }
            }
        }


        if (_isMoveCargoToMagnit)
            MoveCargoToMagnit();

    }


    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }
    private void SwitchingMode()
    {
        float switchCount;
        switchCount = _cameraMode1.depth;
        _cameraMode1.depth = _mainCamera.depth;
        _mainCamera.depth = switchCount;

        _firstPersonController.enabled = !_firstPersonController.enabled;

        SetCursorLock(_firstPersonController.enabled);

        _uiInstallation.SetActive(!_uiInstallation.activeSelf);

        _aim.enabled = !_aim.enabled;

        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;

        foreach (var item in _enviroments)
        {
            item.SetActive(!item.activeSelf);
        }

    }


    public void LabMode1()
    {
        float switchCount;
        switchCount = _cameraMode1.depth;
        _cameraMode1.depth = _cameraMode2.depth;
        _cameraMode2.depth = switchCount;

        _uiInstallationMode1.SetActive(true);
        _uiInstallationMode2.SetActive(false);

        _rama.SetActive(true);

        _isLabMode2 = false;

    }

    public void LabMode2()
    {
        float switchCount;
        switchCount = _cameraMode2.depth;
        _cameraMode2.depth = _cameraMode1.depth;
        _cameraMode1.depth = switchCount;

        _uiInstallationMode2.SetActive(true);
        _uiInstallationMode1.SetActive(false);

        _rama.SetActive(false);

        _isLabMode2 = true;

    }


    private void ConnectedCargo()
    {

        _activeCargo = ObjectMove.Instance.Target;

        ObjectMove.Instance.DropObject();

        _activeCargo.transform.position = _fantomCargo.transform.position;

        _activeCargo.GetComponent<MeshRenderer>().enabled = false;

        _activeCargo.GetComponent<Rigidbody>().isKinematic = true;


        _connectedCargoButton.interactable = true;
        _connectedCargoButton.GetComponent<Image>().color = Color.red;

        _isCargo = true;



    }

    private void DisconectedCargo()
    {
        _cableComponent.EndPoint = _fantomCargo.transform;
        _activeCargo.GetComponent<MeshRenderer>().enabled = true;

        _cableComponent.GetComponent<SpringJoint>().connectedBody = _fantomCargo.GetComponent<Rigidbody>();

        _cableComponent.InitCableParticles();
        _cableComponent.InitLineRenderer();

        ObjectMove.Instance.GrabObject(_activeCargo);

        _connectedCargoButton.interactable = false;

        _connectedCargoButton.GetComponent<Image>().color = Color.white;
        _magnitButton.GetComponent<Image>().color = Color.white;
        _fantomCargo.enabled = true;

        _isCargo = false;



    }

    public void CargoActivation()
    {
        _isCargoActiv = !_isCargoActiv;
        _fantomCargo.enabled = !_fantomCargo.enabled;
        _activeCargo.GetComponent<MeshRenderer>().enabled = !_activeCargo.GetComponent<MeshRenderer>().enabled;
        _magnitButton.interactable = !_magnitButton.interactable;
        if (_isCargoActiv)
        {
            _isCargoDisconected = false;
            _connectedCargoButton.GetComponent<Image>().color = Color.green;

            _activeCargo.transform.position = _fantomCargo.transform.position;
            _cableComponent.EndPoint = _activeCargo.transform;
            _cableComponent.GetComponent<SpringJoint>().connectedBody = _activeCargo.GetComponent<Rigidbody>();

            _cableComponent.InitCableParticles();
            _cableComponent.InitLineRenderer();

            ConnectCargo.Invoke();

            if (_isMagnitActiv)
            {
                MagnitActivation();
            }
            else
            {
                MagnitActivation();
                MagnitActivation();

            }

        }
        else
        {

            _isCargoDisconected = true;
            _connectedCargoButton.GetComponent<Image>().color = Color.red;

            _cableComponent.EndPoint = _fantomCargo.transform;
            _activeCargo.GetComponent<MeshRenderer>().enabled = false;

            _cableComponent.GetComponent<SpringJoint>().connectedBody = _fantomCargo.GetComponent<Rigidbody>();

            _cableComponent.InitCableParticles();
            _cableComponent.InitLineRenderer();

            _fantomCargo.enabled = true;
            _isMoveCargoToMagnit = false;





        }

    }

    public void MagnitActivation()
    {
        _isMagnitActiv = !_isMagnitActiv;

        if (_isMagnitActiv)
        {
            _magnitButton.GetComponent<Image>().color = Color.red;
            _activeCargo.GetComponent<Rigidbody>().isKinematic = false;
            _isMoveCargoToMagnit = false;
            offFixStopWatch.Invoke();
        }
        else
        {
            _isMoveCargoToMagnit = true;
            _magnitButton.GetComponent<Image>().color = Color.green;

            StopWatchReset.Invoke();

        }

    }

    private void MoveCargoToMagnit()
    {

        if (Vector3.Distance(_activeCargo.transform.position, _fantomCargo.transform.position) > 0.1f)
        {
            _activeCargo.transform.position = Vector3.MoveTowards(_activeCargo.transform.position, _fantomCargo.transform.position, Time.deltaTime * _speedMoveCargo);
            _activeCargo.GetComponent<Rigidbody>().isKinematic = true;
        }

    }

}
