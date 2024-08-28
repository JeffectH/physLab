
using UnityEngine;
using UnityEngine.UI;

public class CaliperMode : MonoBehaviour
{
    [SerializeField] private Caliper _caliper;
    [SerializeField] private CaliperFrame _caliperFrame;
    [SerializeField] private Transform _caliperModePoint;
    [SerializeField] private Transform _caliperDefaultPoint;


    [SerializeField] private Camera _caliperCamera;
    [SerializeField] private GameObject _caliperUI;
    [SerializeField] private GameObject _dottedLineOne, _dottedLineTwo;

    private FirstPersonControllerTim _firstPersonController;

    [SerializeField] private Image _aim;
    

        //заменить на float
    [SerializeField] private float _caliperPointMax;
    [SerializeField] private float _caliperPointMin;

    public GameObject _ballVariant;
    [SerializeField] private Transform _ballModePoint;

    private bool _playerInTrigger; //убрать беcполезный флаг
    public static bool ModeActive; //убрать беcполезный флаг


    private Vector3 ballScaleDefault;
    private float _valueScaleBall;
    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    private void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!ModeActive && ObjectMove.Instance.Target != null)
            {
                CaliperModeOn();
            }
            else if (ModeActive)
            {
                CaliperModeOff();
            }
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            if (ObjectMove.Instance.Target != null)
            {
                _firstPersonController = other.GetComponent<FirstPersonControllerTim>();
                _playerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            _playerInTrigger = false;
        }
    }


    private void CaliperModeOn()
    {
        SetCursorLock(false);
        ModeActive = true;
        _firstPersonController.enabled = false;
        _aim.enabled = false;
        
        //calliperUIOn
        _caliperCamera.depth = 1;
        _caliperUI.SetActive(true);
        _dottedLineOne.SetActive(true);
        _dottedLineTwo.SetActive(true);

        var transformCaliper = _caliper.transform;
        transformCaliper.position = _caliperModePoint.position;
        transformCaliper.rotation = _caliperModePoint.rotation;

        Interactable.Instance.is2DRay = true;

        _ballVariant = ObjectMove.Instance.Target;
        _ballVariant.layer = 2;
        MovingBallIntoCaliper(_ballVariant);

    }

    private void CaliperModeOff()
    {

        SetCursorLock(true);
        ModeActive = false;
        _firstPersonController.enabled = true;
        _aim.enabled = true;

        //calliperUIOff
        _caliperCamera.depth = -2;
        _caliperUI.SetActive(false);
        _dottedLineOne.SetActive(false);
        _dottedLineTwo.SetActive(false);
        
        var transformCaliper = _caliper.transform;
        transformCaliper.position = _caliperDefaultPoint.position;
        transformCaliper.rotation = _caliperDefaultPoint.rotation;

        Interactable.Instance.is2DRay = false;

        _ballVariant.layer = 0;
        MovinBallDefault(_ballVariant);

        _ballVariant = null;
    }





    private void MovingBallIntoCaliper(GameObject ActiveBall)
    {
        ObjectMove.Instance.DropObject();
        ActiveBall.GetComponent<Rigidbody>().isKinematic = true;

        NewScaleBall(ActiveBall);

        var position = _ballModePoint.position;
        ActiveBall.transform.position = new Vector3(position.x - ActiveBall.transform.localScale.z / 2, position.y, position.z);

    }

    private void MovinBallDefault(GameObject activeBall)
    {
        activeBall.transform.localScale = ballScaleDefault;

        ObjectMove.Instance.GrabObject(activeBall);
    }

    private void NewScaleBall(GameObject activeBall)
    {
        ballScaleDefault = activeBall.transform.localScale;

        switch (activeBall.GetComponent<GenerateScaleBallLabOne>().RandomValue)
        {

            case 1:
                _valueScaleBall = Random.Range(0.027f, 0.027527f);
                activeBall.transform.localScale = new Vector3(_valueScaleBall, _valueScaleBall, _valueScaleBall);
                break;
            case 2:
                _valueScaleBall = Random.Range(0.027899f, 0.027959f);
                activeBall.transform.localScale = new Vector3(_valueScaleBall, _valueScaleBall, _valueScaleBall);
                break;
            case 3:
                _valueScaleBall = Random.Range(0.0281055f, 0.028669f);
                activeBall.transform.localScale = new Vector3(_valueScaleBall, _valueScaleBall, _valueScaleBall);
                break;
        }
    }



}
