using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LabFourInstalation : MonoBehaviour
{
    public static LabFourInstalation Instance;
    [SerializeField] private Camera CameraInstalation;
    [SerializeField] private Camera CameraPlayer;

    [SerializeField] private GameObject UI;

    [SerializeField] private FirstPersonControllerTim _firstPersonController;

    [SerializeField] private TextMeshPro high;

    [SerializeField] private MeshRenderer _ballPhantomOne;
    [SerializeField] private MeshRenderer _ballPhantomTwo;

    [SerializeField] private Button _connectedBallButtonOne;
    [SerializeField] private Button _connectedBallButtonTwo;

    private bool _isPlayerInZone;
    private bool _activate;
    private bool _ballOneActive;
    private bool _ballTwoActive;
    private bool _modeOn;

    private bool BallOneButtonActive;
    private bool BallTwoButtonActive;
    private bool BallOneActiveEX;
    private bool BallTwoActiveEX;
    [SerializeField] private float _force;

    public GameObject _ballActive;
    public GameObject _ballOne;
    public GameObject _ballTwo;

    [SerializeField] private GameObject _aim;

    private bool _allDisconnected = true;
    private bool _allConnected = true;

    public Image Aim;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            _isPlayerInZone = true;
            if (ObjectMove.Instance.Target != null)
            {
                _ballPhantomOne.enabled = true;

                _ballPhantomTwo.enabled = true;
            }

            if (_ballOneActive || ObjectMove.Instance.Target == null && !_ballOneActive)
            {
                _ballPhantomOne.enabled = false;
            }

            if (_ballTwoActive || ObjectMove.Instance.Target == null && !_ballTwoActive)
            {
                _ballPhantomTwo.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            _isPlayerInZone = false;

            _ballPhantomOne.enabled = false;
            _ballPhantomTwo.enabled = false;
        }
    }


    private void Update()
    {
        if (_isPlayerInZone)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_firstPersonController.enabled)
                {
                    if (ObjectMove.Instance.Target != null && !_ballTwoActive)
                    {
                        ConnectedBall();
                    }
                    else
                    {
                        if (ObjectMove.Instance.Target == null)
                            DisconnectedBall();
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ObjectMove.Instance.Target == null)
                    SwitchingMode();
            }
        }
    }

    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    private void SwitchingMode()
    {
        (CameraInstalation.depth, CameraPlayer.depth) = (CameraPlayer.depth, CameraInstalation.depth);

        _firstPersonController.enabled = !_firstPersonController.enabled;
        SetCursorLock(_firstPersonController.enabled);

        UI.SetActive(!UI.activeSelf);

        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;
        _aim.SetActive(!_aim.activeSelf);

        if (_ballTwoActive)
        {
            _activate = true;
        }

        Aim.enabled = !Aim.enabled;
    }

    private void ConnectedBall()
    {
        if (_ballOneActive)
        {
            if (ObjectMove.Instance.Target.GetComponent<InteractableObjects>() != null)
            {
                if (_ballOne.GetComponent<InteractableObjects>().MaterialSphere ==
                    ObjectMove.Instance.Target.GetComponent<InteractableObjects>().MaterialSphere)
                {
                    _ballActive = ObjectMove.Instance.Target;

                    ObjectMove.Instance.DropObject();

                    Debug.Log("Con2");

                    _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 2;
                    _ballActive.GetComponent<Rigidbody>().isKinematic = true;
                    _ballActive.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                    _ballActive.transform.parent = _ballPhantomTwo.transform;
                    _ballActive.transform.localPosition = Vector3.zero;

                    _ballTwoActive = true;
                    _ballTwo = _ballActive;
                    _ballActive = null;
                }
            }
        }
        else
        {
            _ballActive = ObjectMove.Instance.Target;

            ObjectMove.Instance.DropObject();
            Debug.Log("Con1");

            _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 1;
            _ballActive.AddComponent<CollisionBallLabFour>();
            _ballActive.GetComponent<Rigidbody>().isKinematic = true;
            _ballActive.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            _ballActive.transform.parent = _ballPhantomOne.transform;
            _ballActive.transform.localPosition = Vector3.zero;

            _ballOneActive = true;
            _ballOne = _ballActive;
            _ballActive = null;
        }

        //_image.color = Color.green;
    }

    private void DisconnectedBall()
    {
        if (_ballTwoActive)
        {
            Debug.Log("Dis2");
            _ballTwo.GetComponent<InteractableObjects>().NumSphereBall = 0;

            _ballTwoActive = false;


            _ballTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            ObjectMove.Instance.GrabObject(_ballTwo);

            _ballTwo = null;
        }
        else
        {
            Debug.Log("Dis1");
            _ballOne.GetComponent<InteractableObjects>().NumSphereBall = 0;

            Destroy(_ballOne.GetComponent<CollisionBallLabFour>());


            _ballOneActive = false;
            _ballOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


            ObjectMove.Instance.GrabObject(_ballOne);

            _ballOne = null;
        }


        //  _image.color = Color.white;
        _activate = false;
    }


    public void Push()
    {
        if (_ballOneActive && _ballTwoActive)
        {
            ResetBallPosition();
            _ballOne.GetComponent<Rigidbody>().isKinematic = false;
            _ballTwo.GetComponent<Rigidbody>().isKinematic = false;

            _ballTwo.GetComponent<Rigidbody>().AddForce(Vector3.left * _force, ForceMode.Impulse);
        }
    }

    public void ResetBallPosition()
    {
        if (_ballOneActive)
        {
            _ballOne.GetComponent<Rigidbody>().isKinematic = true;
            _ballOne.transform.parent = _ballPhantomOne.transform;
            _ballOne.transform.localPosition = Vector3.zero;
        }

        if (_ballTwoActive)
        {
            _ballTwo.GetComponent<Rigidbody>().isKinematic = true;
            _ballTwo.transform.parent = _ballPhantomTwo.transform;
            _ballTwo.transform.localPosition = Vector3.zero;
        }
    }
}