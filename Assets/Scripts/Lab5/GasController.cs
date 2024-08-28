using Lab5Physycs.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GasController : MonoBehaviour
{
    public DropGasAnimateController dropGasController;
    public Dropdown selectGas;
    private WaterControl waterControl;
    [SerializeField]
    private List<Gas> gases = new List<Gas>();
    private bool isClear = true;
    private int gasIndex = 0;

    public UnityEvent changeGas;

    public void Awake()
    {
        changeGas = new UnityEvent();
    }
    void Start()
    {
        waterControl = GetComponent<WaterControl>();

        if (gases.Count <= 1)
        {
            selectGas.gameObject.SetActive(false);
        }
        else
        {
            selectGas.options = gases.Select((Gas gas) => new Dropdown.OptionData(gas.name)).ToList();
            selectGas.onValueChanged.AddListener(ChangeGas);
        }
    }

    private void ChangeGas(int gasIndex)
    {
        if (this.gasIndex != gasIndex)
        {
            this.gasIndex = gasIndex;
            DropFullGas();
            changeGas.Invoke();
        }
    }

    public void DropFullGas()
    {
        isClear = true;
        dropGasController.StartDropGas();
        waterControl.ResetAnimWaterAndMoveToDelta();
    }

    public void DropPartialGas()
    {
        Gas gas = gases[gasIndex];
        dropGasController.StartDropGas();
        waterControl.ResetAnimWaterAndMoveToDelta(gas.valueAfterDrop + RandomUtils.GetRandomValueAndZnak(gas.deltaMaxRandomValueAfterDrop), gas.timeAfterDrop);
    }

    public Gas GetGas()
    {
        return gases[gasIndex];
    }

    public bool IsClear()
    {
        return isClear;
    }

    public void FillTube()
    {
        isClear = false;
    }

    [Serializable]
    public class Gas
    {
        public string name;
        [Header("�������� ������� �������")]
        public float maxValue;
        [Header("���������� �������� ������ ����")]
        public float percentStop;
        [Header("�������� ����� ������ ����")]
        public float valueAfterDrop;
        [Header("������������ ���������� (+-Random(value))")]
        public float deltaMaxRandomValue = 10;
        [Header("������������ ���������� ������(��������) (+-Random(value))")]
        public float deltaMaxRandomPercent;
        [Header("������������ ���������� ����� ������ (+-Random(value))")]
        public float deltaMaxRandomValueAfterDrop;
        [Header("����� ��� ���������� ���������")]
        public float timeChange = 10;
        [Header("����� ��� ������ ����� ���������� �����������")]
        public float timeBeforeDrop = 10;
        [Header("����� ��� ��������� ����� ������, �� ����� ������")]
        public float timeAfterDrop = 10;
    }
}
