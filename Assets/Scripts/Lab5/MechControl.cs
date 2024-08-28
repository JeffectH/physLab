using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Xml.Serialization;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class MechControl : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject mechCanvas;
    public TimeController timeController;
    [Header("Лупа")]
    public LensController lensController;

    [Header("Держатели трубок")]
    public GameObject holdersTube;

    [Header("Управление")]
    [SerializeField]
    private KeyCode lensButton = KeyCode.L;
    [SerializeField]
    private KeyCode powerButton = KeyCode.Space;
    [SerializeField]
    private KeyCode backButton = KeyCode.E;
    [SerializeField]
    private KeyCode dropGasButton = KeyCode.R;
    [SerializeField]
    private KeyCode timeNextButton = KeyCode.N;
    [SerializeField]
    private KeyCode hideOrShowHoldersTubeButton = KeyCode.H;

    [Header("Камера")]
    public Camera mechCamera;
    private Camera mainCamera;

    private bool isLook = false;
    private ProcessControl control;

    public UnityEvent OffControlEvent;

    public Image Aim;

    [SerializeField] private GameObject _hints;
    public void Start()
    {
        control = GetComponent<ProcessControl>();
    }

    public void Update()
    {

        if (isLook)
        {
            if (Input.GetKeyDown(powerButton))
                PowerButton();
            if (Input.GetKeyDown(backButton))
                OffControl();
            if (Input.GetKeyDown(lensButton))
                LensButton();
            if (Input.GetKeyDown(dropGasButton))
                DropGasButton();
            if (Input.GetKeyDown(timeNextButton))
                SwitchTimeScale();
            if (Input.GetKeyDown(hideOrShowHoldersTubeButton))
                SwitchViewHoldersTube();


            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                lensController.SwitchMaxZoom();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                lensController.SwitchMinZoom();
            }
        }
    }

    public void PowerButton()
    {
        control.StartSwitchActivity();
    }

    public void SwitchViewHoldersTube()
    {
        holdersTube.SetActive(!holdersTube.activeSelf);
    }
    public void LensButton()
    {
        lensController.SwitchLensView();
    }

    public void DropGasButton()
    {
        control.ControllDropGas();
    }

    public void SwitchTimeScale()
    {
        timeController.NextTime();
    }

    public void OnControl()
    {
        ConfigureBaseParameters();
        SwapCamera();
        SwapStatusCamera();
        
    }
    public void OffControl()
    {
        SwapCamera();
        SwapStatusCamera();
        ResetBaseParameters();
        OffControlEvent.Invoke();
        _hints.SetActive(true);


        
    }

    private void ConfigureBaseParameters()
    {
        mainCamera = Camera.main;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        mechCanvas.SetActive(true);
    }

    private void ResetBaseParameters()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lensController.SetActiveLens(false);
        timeController.ResetTime();
        mechCanvas.SetActive(false);
    }

    private void SwapCamera()
    {
        mechCamera.enabled = !mechCamera.enabled;
        mainCamera.enabled = !mainCamera.enabled;
        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;
        Aim.enabled = !Aim.enabled;

    }

    private void SwapStatusCamera()
    {
        isLook = !isLook;
    }
}
