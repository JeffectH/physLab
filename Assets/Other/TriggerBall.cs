using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBall : MonoBehaviour
{

    public float AddForce = 100;
    public Transform MagnetPoint;
    public static bool  HitOne;

    private List<Vector3> _posBall = new List<Vector3>();
    private GameObject _ballTwo;
    private int index = 0;

    public static bool Hit;
    public static bool RebootHitBox;

    private void OnCollisionEnter(Collision collision)
    {
        if (RebootHitBox)
        {
             if (!InstallationSimulationOne.Activate) 
                    {
                        HitOne = false;
                        if (collision.transform.GetComponent<InteractableObjects>()) 
                        {
            
                            if (!HitOne) 
                            {
                                if (Voltmeter.Instance.VoltmeterModeTwo) 
                                {
                                    Voltmeter.Instance.RotateArrowModeTwo();
                                }
                                else
                                if (Voltmeter.Instance.VoltmeterModeThree)
                                {
                                    Voltmeter.Instance.RotateArrowModeThree();
                                }
                               
                                Vector3 direction = MagnetPoint.position - collision.transform.position;
                                switch (collision.transform.GetComponent<GenerateScaleBallLabOne>().RandomValue) 
                                {
                                    case 1:
                                        AddForce = Random.Range(100, 138);
                                        break;
                                    case 2:
                                        AddForce = Random.Range(95,128);
                                        break;
                                    case 3:
                                        AddForce = Random.Range(84, 101);
                                        break;
                                }
                                collision.transform.GetComponent<Rigidbody>().AddForce(direction * AddForce);
                                HitOne = true;
            
                                _ballTwo = collision.gameObject;
            
                                _posBall.Add(_ballTwo.transform.position);
            
            
                                Hit = true;
                            }
                        }
                    }

             RebootHitBox = false;
        }

       
    }


    private void Update()
    {
        if (Hit) 
        {
            AddPosition();

        }
    }

    private void AddPosition()
    {

        _posBall.Add(_ballTwo.transform.position);

        if (Vector3.Distance(_posBall[index], MagnetPoint.position) < Vector3.Distance(_posBall[index + 1], MagnetPoint.position))
        {
            _ballTwo.GetComponent<Rigidbody>().isKinematic = true;
            Hit = false;
            index = -1;
            _posBall.Clear();

        }
        index++;

    }
}
