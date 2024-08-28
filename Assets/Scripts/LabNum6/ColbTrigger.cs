using UnityEngine;

public class ColbTrigger : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _instalationCamera;

    [SerializeField] private FirstPersonControllerTim _firstPersonController;
    [SerializeField] private GameObject _uiInstallation;
    [SerializeField] private GameObject _uiTarget;
    [SerializeField] private GameObject _boardM;

    public static bool PlayerInZone;

    private void OnEnable()
    {
        MethodStoksa.onModeMethodStoks += SwithView;
    }

    private void OnDisable()
    {
        MethodStoksa.onModeMethodStoks -= SwithView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>()) 
        {
            PlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonControllerTim>())
        {
            PlayerInZone = false;
        }
    }

    void SetCursorLock(bool isLocked)
    {
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    private void SwithView() 
    {
        float switchCount;
        switchCount = _instalationCamera.depth;
        _instalationCamera.depth = _playerCamera.depth;
        _playerCamera.depth = switchCount;

        _firstPersonController.enabled = !_firstPersonController.enabled;

        SetCursorLock(_firstPersonController.enabled);

        Interactable.Instance.is2DRay = !Interactable.Instance.is2DRay;
        
        _uiInstallation.SetActive(!_uiInstallation.activeSelf);
        _uiTarget.SetActive(!_uiTarget.activeSelf);
        _boardM.SetActive(!_boardM.activeSelf);
    }
}
