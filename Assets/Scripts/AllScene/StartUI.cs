using UnityEngine;
using UnityEngine.Serialization;

public class StartUI : MonoBehaviour
{

    [SerializeField] private FirstPersonControllerTim _firstPersonControllerTim;
    [SerializeField] private GameObject _hints;
    [SerializeField] private GameObject _aim;
    private Canvas _canvasObject; // Assign in inspector
    private bool _isNotActivated = true;
    
    
    void Start()
    {
        _firstPersonControllerTim.enabled = false;
        _canvasObject = GetComponent<Canvas>();
        _canvasObject.enabled = true;
        _hints.SetActive(false);
        _aim.SetActive(false);
    }

    void Update()
    {
        
        if (Input.anyKey && _isNotActivated)
        {
            _canvasObject.enabled = false;
            _firstPersonControllerTim.enabled = true;
            _hints.SetActive(true);
            _aim.SetActive(true);
            _isNotActivated = false;

        }
    }
}