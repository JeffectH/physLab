using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrometerMode : MonoBehaviour
{
    [SerializeField] private Micrometer _micrometer;
    [SerializeField] private Transform _micrometerModePoint;
    [SerializeField] private Transform _micrometerDefaultPoint;

    [SerializeField] private Camera _micrometerCamera;
    [SerializeField] private GameObject _micrometerUI;

    private FirstPersonControllerTim _firstPersonController;


    [SerializeField] private GameObject _ballVariant;
    [SerializeField] private Transform _ballModePoint;

    private bool _playerInTrigger; //убрать беcполезный флаг
    public static bool ModeActive; //убрать беcполезный флаг

    private Vector3 BallScaleDefault;


    private void Start()
    {
        // _micrometerDefaultPoint = _micrometer.transform;
    }

    private void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!ModeActive)
            {
                MicrometerModeOn();
            }
            else
            {
                MicrometerModeOff();
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


    private void MicrometerModeOn()
    {
        SetCursorLock(false);

        ModeActive = true;
        _firstPersonController.enabled = false;

        //calliperUIOn
        _micrometerCamera.depth = 1;
        _micrometerUI.SetActive(true);

        _micrometer.transform.position = _micrometerModePoint.position;
        _micrometer.transform.rotation = _micrometerModePoint.rotation;


        _ballVariant = ObjectMove.Instance.Target;
        _ballVariant.layer = 2;
        MovingBallIntoMicrometer(_ballVariant);
    }

    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    private void MicrometerModeOff()
    {
        SetCursorLock(true);
        ModeActive = false;
        _firstPersonController.enabled = true;

        //calliperUIOff
        _micrometerCamera.depth = -2;
        _micrometerUI.SetActive(false);

        _micrometer.transform.position = _micrometerDefaultPoint.position;
        _micrometer.transform.rotation = _micrometerDefaultPoint.rotation;


        _ballVariant.layer = 0;
        MovinBallDefault(_ballVariant);

        _ballVariant = null;
    }


    private void MovingBallIntoMicrometer(GameObject ActiveBall)
    {
        ObjectMove.Instance.DropObject();
        ActiveBall.GetComponent<Rigidbody>().isKinematic = true;

        RandomScaleBall(ActiveBall);
        ActiveBall.transform.position = new Vector3(_ballModePoint.position.x + ActiveBall.transform.localScale.x / 2,
            _ballModePoint.position.y, _ballModePoint.position.z);
    }

    private void MovinBallDefault(GameObject ActiveBall)
    {
        ActiveBall.transform.localScale = BallScaleDefault;

        ObjectMove.Instance.GrabObject(ActiveBall);
    }

    private void RandomScaleBall(GameObject ActiveBall)
    {
        BallScaleDefault = ActiveBall.transform.localScale;
        switch (ActiveBall.GetComponent<ScaleBallLabSix>().NumScale
               )
        {
            case 2:
                ActiveBall.transform.localScale = new Vector3(0.0055f, 0.0055f, 0.0055f); //меняем масштаб
                break;

            case 3:
                ActiveBall.transform.localScale = new Vector3(0.00828f, 0.00828f, 0.00828f); //меняем масштаб
                break;

            case 4:
                ActiveBall.transform.localScale = new Vector3(0.0111f, 0.0111f, 0.0111f); //меняем масштаб
                break;
        }
    }
}