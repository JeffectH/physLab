using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallationSimulationTwo : MonoBehaviour
{
    public static InstallationSimulationTwo Instance { get; private set; }

    [SerializeField] private MeshRenderer _ballPhantom1;
    [SerializeField] private MeshRenderer _ballPhantom2;

    [SerializeField] private CableComponentDefault _cableComponent1;
    [SerializeField] private CableComponentDefault _cableComponent2;

    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Camera _cameraInstallation;
    [SerializeField] private FirstPersonControllerTim _firstPersonController;

    [SerializeField] private GameObject _uiInstallation; //UI

    private GameObject _ballActive; //активный шарик

    public static bool _installationMode; //активация режима установки

    public Transform MagnetPoint; //точка магнита

    public static bool Activate; //запуск установки

    public static bool _ballOneActive;
    public static bool _ballTwoActive;

    public Image _image;
    public Image Aim;

    public Transform DirectionForce;

    float direction;

    public GameObject Shkala;

    public static bool _freazeBall = true;
    public static bool _prilipBall;

    private GameObject ballOne;
    private GameObject ballTwo;

    private void Awake()
    {
        Instance = this;
        _ballOneActive = false;
        _ballTwoActive = false;
    }

    private void Update()
    {
        if (TrigerInstalationTwo.IsPlayerInZone)
        {
            if (!Interactable.Instance.is2DRay)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (ObjectMove.Instance.Target != null && !_ballTwoActive)
                    {
                        ConnectingBall();
                    }
                    else
                    {
                        if (ObjectMove.Instance.Target == null)
                            DisconectingBall();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ObjectMove.Instance.Target == null)
                {
                    if (ballOne != null && ballTwo != null)
                    {
                        SwitchingMode();
                    }
                }
            }
        }

        if (_ballTwoActive && Activate)
        {
            Magnet();
        }
    }

    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    private void ConnectingBall()
    {


        if (_ballOneActive)
        {
            if (ObjectMove.Instance.Target != null)
            {
                if (ObjectMove.Instance.Target.GetComponent<InteractableObjects>() != null)
                {
                    if (ballOne.GetComponent<InteractableObjects>().MaterialSphere ==
                        ObjectMove.Instance.Target.GetComponent<InteractableObjects>().MaterialSphere)
                    {
                        _ballActive = ObjectMove.Instance.Target;

                        ObjectMove.Instance.DropObject();

                        Debug.Log("Con2");
                        _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 2;
                        _ballActive.transform.position = _ballPhantom2.transform.position;

                        _cableComponent2.EndPoint = _ballActive.transform;
                        _cableComponent2.GetComponent<SpringJoint>().connectedBody =
                            _ballActive.GetComponent<Rigidbody>();
                        _ballActive.GetComponent<Rigidbody>().isKinematic = true;

                        _cableComponent2.InitCableParticles();
                        _cableComponent2.InitLineRenderer();

                        _ballTwoActive = true;
                        ballTwo = _ballActive;
                        ballTwo.layer = 2;
                    }
                }
            }
        }
        else
        {
            _ballActive = ObjectMove.Instance.Target;

            ObjectMove.Instance.DropObject();

            Debug.Log("Con1");
            _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 1;
            _ballActive.AddComponent<CollisionBallLabTwo>();
            _ballActive.transform.position = _ballPhantom1.transform.position;

            _cableComponent1.EndPoint = _ballActive.transform;
            _cableComponent1.GetComponent<SpringJoint>().connectedBody = _ballActive.GetComponent<Rigidbody>();
            _ballActive.GetComponent<Rigidbody>().isKinematic = true;

            _cableComponent1.InitCableParticles();
            _cableComponent1.InitLineRenderer();

            _ballOneActive = true;
            ballOne = _ballActive;
            ballOne.layer = 2;
        }

        _image.color = Color.green;
    }

    private void DisconectingBall()
    {
        if (_ballTwoActive)
        {
            Debug.Log("Dis2");
            _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 0;

            _cableComponent2.EndPoint = _ballPhantom2.transform;
            _cableComponent2.GetComponent<SpringJoint>().connectedBody = _ballPhantom2.GetComponent<Rigidbody>();
            _cableComponent2.InitCableParticles();
            _cableComponent2.InitLineRenderer();

            _ballTwoActive = false;


            _ballActive.layer = 7;


            ObjectMove.Instance.GrabObject(_ballActive);

            _ballActive = _cableComponent1.EndPoint.gameObject;

            ballTwo = null;
        }
        else
        {
            Debug.Log("Dis1");
            _ballActive.GetComponent<InteractableObjects>().NumSphereBall = 0;
            Destroy(_ballActive.GetComponent<CollisionBallLabTwo>());

            _cableComponent1.EndPoint = _ballPhantom1.transform;
            _cableComponent1.GetComponent<SpringJoint>().connectedBody = _ballPhantom1.GetComponent<Rigidbody>();
            _cableComponent1.InitCableParticles();
            _cableComponent1.InitLineRenderer();

            _ballOneActive = false;


            _ballActive.layer = 7;

            ObjectMove.Instance.GrabObject(_ballActive);


            ballOne = null;
        }

        _image.color = Color.white;
        Activate = false;
    }

    private void SwitchingMode()
    {
        (_cameraInstallation.depth, _cameraPlayer.depth) = (_cameraPlayer.depth, _cameraInstallation.depth);

        var enabled1 = _firstPersonController.enabled;
        enabled1 = !enabled1;
        _firstPersonController.enabled = enabled1;
        SetCursorLock(enabled1);
        _uiInstallation.SetActive(!_uiInstallation.activeSelf);
        Aim.enabled = !Aim.enabled;
        if (_ballTwoActive)
        {
            Activate = true;
        }

        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;

        if (_firstPersonController.enabled)
        {
            if (ballOne)
            {
                ballOne.layer = 2;
            }

            if (ballTwo)
            {
                ballTwo.layer = 2;
            }
        }
        else
        {
            if (ballOne)
            {
                ballOne.layer = 7;
            }

            if (ballTwo)
            {
                ballTwo.layer = 7;
            }
        }
    }

    private void Magnet()
    {
        if (Vector3.Distance(_cableComponent1.EndPoint.position, MagnetPoint.position) < 0.25)
        {
            //ObjectMove.Instance.DropObject(_cableComponent1.EndPoint);
            _cableComponent1.EndPoint.position = Vector3.MoveTowards(_cableComponent1.EndPoint.position,
                MagnetPoint.position, 2 * Time.deltaTime);
        }

        if (Vector3.Distance(_cableComponent1.EndPoint.position, MagnetPoint.position) < 0.05f)
        {
            //_ballActive.GetComponent<Rigidbody>().isKinematic = false;

            _prilipBall = true;
            _cableComponent1.EndPoint.GetComponent<Rigidbody>().isKinematic = true;
        }
    }


    public void ActivationInstalation()
    {
        Activate = !Activate;

        if (Activate)
        {
            _image.color = Color.green;
        }
        else
        {
            if (_prilipBall)
            {
                TriggerBall.HitOne = false;
                _cableComponent1.EndPoint.GetComponent<Rigidbody>().isKinematic = false;
                _prilipBall = false;

                if (ballOne)
                {
                    ballTwo.transform.position = _ballPhantom2.transform.position;
                    ballTwo.GetComponent<Rigidbody>().isKinematic = false;
                    ballTwo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }

            _image.color = Color.white;
        }
    }

    public void ResetBallTwo()
    {
        if (ballOne)
        {
            ballTwo.transform.position = _ballPhantom2.transform.position;
            ballTwo.GetComponent<Rigidbody>().isKinematic = true;
            ballTwo.GetComponent<Rigidbody>().velocity = Vector3.zero;

            ballOne.transform.position = _ballPhantom1.transform.position;
            ballOne.GetComponent<Rigidbody>().isKinematic = true;
            ballOne.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}