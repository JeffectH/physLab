using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera mainCamera;
    private Camera customCamera;
    private FirstPersonControllerTim firstPersonController;
    private bool isEnableControl = true;
    [SerializeField] private GameObject _hints;

    void Start()
    {
        firstPersonController = GetComponent<FirstPersonControllerTim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnableControl)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.rotation * Vector3.forward,
                        out hit, 100, LayerMask.GetMask("LaboratoryMech"), QueryTriggerInteraction.Collide))
                {
                    OffPlayer();
                    MechControl control = hit.collider.gameObject.GetComponent<MechControl>();
                    control.OffControlEvent.AddListener(OnPlayer);
                    control.OnControl();
                    _hints.SetActive(false);
                }
            }
        }
    }

    private void OnPlayer()
    {
        firstPersonController.cameraCanMove = true;
        isEnableControl = true;
    }

    private void OffPlayer()
    {
        firstPersonController.cameraCanMove = false;
        isEnableControl = false;
    }
}