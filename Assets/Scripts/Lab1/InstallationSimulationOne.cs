
using UnityEngine;
using UnityEngine.UI;

public class InstallationSimulationOne : MonoBehaviour
{
    [SerializeField] private MeshRenderer _ballPhantom;
    [SerializeField] private CableComponentDefault _cableComponent;

    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Camera _cameraInstallation;
    [SerializeField] private FirstPersonControllerTim _firstPersonController;

    [SerializeField] private GameObject _uiInstallation;

    [SerializeField] private Image _aim;
    
    private GameObject _ballActive;

    public static bool _installationMode;

    public Transform MagnetPoint;

    public static bool Activate;

    public Image _image;

    float direction;

    public GameObject Shkala;

    public static bool _freazeBall = true;
    public bool _prilipBall;

    private HingeJoint hingeJoint;
    private void Update()
    {
        if (TriiggerInstallation.IsPlayerInZone)
        {
            if (_firstPersonController.enabled)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {

                    if (_ballPhantom.enabled)
                    {
                        if (ObjectMove.Instance.Target != null)
                            ConnectingBall();
                    }
                    else
                    {
                        if (!ObjectMove.Instance.Target)
                        {
                            if(_installationMode)
                             DisconectingBall();
                        }
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_installationMode)
                {
                     if (ObjectMove.Instance.Target == null)
                           SwitchingMode();   
                }
            }
        }
        if (_installationMode && Activate)
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
        _ballActive = ObjectMove.Instance.Target;
        Debug.Log(_ballActive.name);
        ObjectMove.Instance.DropObject();
        
        _ballActive.transform.position = _ballPhantom.transform.position;
        _cableComponent.EndPoint = _ballActive.transform;
        _cableComponent.GetComponent<SpringJoint>().connectedBody = _ballActive.GetComponent<Rigidbody>();
        _ballActive.GetComponent<Rigidbody>().isKinematic = true;
        
        _cableComponent.InitCableParticles();
        _cableComponent.InitLineRenderer();

        _ballActive.GetComponent<InteractableObjects>().IsMoveSphere = false;
        
        _installationMode = true;
        _image.color = Color.green;
        Activate = true;
    }
    private void DisconectingBall()
    {
        _cableComponent.EndPoint = _ballPhantom.transform;
        _cableComponent.GetComponent<SpringJoint>().connectedBody =_ballPhantom.GetComponent<Rigidbody>();
        _cableComponent.InitCableParticles();
        _cableComponent.InitLineRenderer();

        ObjectMove.Instance.GrabObject(_ballActive);

        _ballActive.GetComponent<InteractableObjects>().IsMoveSphere = true;

        _installationMode = false;
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

        _aim.enabled = !_aim.enabled;
        
        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;
        
        _ballActive.GetComponent<InteractableObjects>().IsMoveSphere = !_ballActive.GetComponent<InteractableObjects>().IsMoveSphere;
        
    }

    private void Magnet()
    {

        if (Vector3.Distance(_cableComponent.EndPoint.position, MagnetPoint.position) < 0.25)
        {
            //ObjectMove.Instance.DropObject(_cableComponent.EndPoint);
            _cableComponent.EndPoint.position = Vector3.MoveTowards(_cableComponent.EndPoint.position, MagnetPoint.position, 2 * Time.deltaTime);
        }

        if (Vector3.Distance(_cableComponent.EndPoint.position, MagnetPoint.position) < 0.05f)
        {
            _prilipBall = true;
            _cableComponent.EndPoint.GetComponent<Rigidbody>().isKinematic = true;
        }
    }


    public void ActivationInstalation()
    {

        Activate = !Activate;

        if (Activate)
        {
            _image.color = Color.green;

            Voltmeter.Instance.RotateArrowModeDefault();
        }
        else
        {
            if (_prilipBall)
            {
                TriggerBall.HitOne = false;
                _cableComponent.EndPoint.GetComponent<Rigidbody>().isKinematic = false;
                _prilipBall = false;

                TriggerBall.RebootHitBox = true;
            }
            _image.color = Color.white;

        }
    }
}
