
using UnityEngine;

public class Voltmeter : Tools
{
    public static Voltmeter Instance { get; private set; }

    [SerializeField] private Transform ArrowVoltmeter;


    public bool VoltmeterModeTwo = false;
    public bool VoltmeterModeThree = false;

    private Quaternion ArrowVoltmeterRotationDefault;

    private void Awake()
    {
        Instance = this;
        ArrowVoltmeterRotationDefault = ArrowVoltmeter.rotation;

        
    }

    public void VoltmeterModeOneOn()
    {
        
        VoltmeterModeTwo = false;
        VoltmeterModeThree = false;
        RotateArrowModeDefault();
    }
    public void VoltmeterModeTwoOn()
    {
        VoltmeterModeTwo = true;
        VoltmeterModeThree = false;
        

    }

    public void VoltmeterModeThreeOn()
    {

        VoltmeterModeTwo = false;
        VoltmeterModeThree = true;


    }
    public void RotateArrowModeTwo()
    {

        ArrowVoltmeter.Rotate(0,0, Random.Range(-24f,-30f));
    }

    public void RotateArrowModeThree()
    {

        ArrowVoltmeter.Rotate(0, 0, Random.Range(-43f, -59f));

    }
    public void RotateArrowModeDefault()
    {


        ArrowVoltmeter.rotation = ArrowVoltmeterRotationDefault;

    }


}
