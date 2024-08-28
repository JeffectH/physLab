
using UnityEngine;
using UnityEngine.UI;

public class Micrometer : MonoBehaviour
{
    [SerializeField] private GameObject _shtift;
    [SerializeField] private float _maxShift;
    [SerializeField] private float _minShift;

    [SerializeField] private float _rotateMax;
    [SerializeField] private float _rotateMin;

    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = _rotateMax;
        _slider.minValue = _rotateMin;
        _slider.onValueChanged.AddListener(MoveSthiftMicrometer);
    }
    public void MoveSthiftMicrometer(float value)
    {

        float speed = value / (_rotateMax);
        _shtift.transform.localPosition = Vector3.Lerp(new Vector3(_maxShift, _shtift.transform.localPosition.y, _shtift.transform.localPosition.z),
        new Vector3(_minShift, _shtift.transform.localPosition.y, _shtift.transform.localPosition.z), speed);


     
        _shtift.transform.localEulerAngles = new Vector3 (value,_shtift.transform.localRotation.y, _shtift.transform.localRotation.z);

    }



}
