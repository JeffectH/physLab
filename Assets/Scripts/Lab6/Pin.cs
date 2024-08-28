using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject cargo;

    [SerializeField] private GameObject CylinderOne;
    [SerializeField] private GameObject CylinderTwo;
    [SerializeField] private GameObject CylinderThree;
    [SerializeField] private GameObject CylinderFour;

    [Header("Стартовое положение цилиндров")] [SerializeField]
    private float MoveStartOne;

    [SerializeField] private float MoveStartTwo;
    [SerializeField] private float MoveStartThree;
    [SerializeField] private float MoveStartFour;

    [Header("Конечно положение цилиндров")] [SerializeField]
    private float MoveEndOne;

    [SerializeField] private float MoveEndTwo;
    [SerializeField] private float MoveEndThree;
    [SerializeField] private float MoveEndFour;

    [Header("Фантомные цилиндры")] [SerializeField]
    private MeshRenderer FantomOne;

    [SerializeField] private MeshRenderer FantomTwo;
    [SerializeField] private MeshRenderer FantomThree;
    [SerializeField] private MeshRenderer FantomFour;

    [SerializeField] private Slider _sliderOne;
    [SerializeField] private Slider _sliderTwo;
    [SerializeField] private Slider _sliderThree;
    [SerializeField] private Slider _sliderFour;

    private bool _isCylinder;

    private int _selectValue;

    private void OnEnable()
    {
        InstalationThree.ConnectCargo += UpdateCargoActive;
    }

    private void OnDisable()
    {
        InstalationThree.ConnectCargo -= UpdateCargoActive;
    }

    private void Update()
    {
        
        if (_isCylinder && !InstalationThree.instance._isMoveCargoToMagnit && InstalationThree.instance._isCargo)
        {
            transform.Rotate(Vector3.forward * cargo.GetComponent<Rigidbody>().velocity.y);
        }


        if (_selectValue == 0)
        {
            MoveCylinder(MoveEndOne, MoveEndTwo, MoveEndThree, MoveEndFour);
        }

        if (_selectValue == 1)
        {
            MoveCylinder(MoveStartOne, MoveStartTwo, MoveStartThree, MoveStartFour);
        }
    }

    private void UpdateCargoActive()
    {
        cargo = InstalationThree.instance._activeCargo;

        _isCylinder = InstalationThree.instance._activeCargo != null;
    }


    public void SelectOffset(int value)
    {
        if (value == 0)
        {
            _selectValue = value;
        }

        if (value == 1)
        {
            _selectValue = value;
        }
    }

    public void MoveCylinder(float MoveEndOne, float MoveEndTwo, float MoveEndThree, float MoveEndFour)
    {
        var localPosition = CylinderOne.transform.localPosition;
        localPosition = Vector3.MoveTowards(
            localPosition,
            new Vector3(localPosition.x, MoveEndOne, localPosition.z),
            2f * Time.deltaTime);
        CylinderOne.transform.localPosition = localPosition;

        var position = CylinderTwo.transform.localPosition;
        position = Vector3.MoveTowards(
            position,
            new Vector3(MoveEndTwo, position.y, position.z),
            2f * Time.deltaTime);
        CylinderTwo.transform.localPosition = position;

        var localPosition1 = CylinderThree.transform.localPosition;
        localPosition1 = Vector3.MoveTowards(
            localPosition1,
            new Vector3(localPosition1.x, MoveEndThree, localPosition1.z),
            2f * Time.deltaTime);
        CylinderThree.transform.localPosition = localPosition1;

        var position1 = CylinderFour.transform.localPosition;
        position1 = Vector3.MoveTowards(
            position1,
            new Vector3(MoveEndFour, position1.y, position1.z),
            2f * Time.deltaTime);
        CylinderFour.transform.localPosition = position1;
    }


    public void InteractiveSlider(bool IsInterect)
    {
        _isCylinder = IsInterect;
        if (IsInterect)
        {
            _sliderOne.interactable = true;
            _sliderTwo.interactable = true;
            _sliderThree.interactable = true;
            _sliderFour.interactable = true;
        }
        else
        {
            _sliderOne.interactable = false;
            _sliderTwo.interactable = false;
            _sliderThree.interactable = false;
            _sliderFour.interactable = false;
        }
    }
}