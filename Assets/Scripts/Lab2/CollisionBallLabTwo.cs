using System.Collections.Generic;
using UnityEngine;

public class CollisionBallLabTwo : MonoBehaviour
{
    public float AddForce = 50;
    public float AddForce2 = 50;
    public static bool HitOne;
    public bool Hit;
    public bool Hit2;

    private List<Vector3> _posBall = new List<Vector3>();
    private List<Vector3> _posBall2 = new List<Vector3>();
    private GameObject _ballTwo;
    private int index = 0;
    private int index2 = 0;


    private void OnCollisionEnter(Collision collision)
    {

        if (!InstallationSimulationTwo.Activate)
        {
            HitOne = false;

            if (collision.transform.GetComponent<InteractableObjects>())
            {
                if (collision.transform.GetComponent<InteractableObjects>().NumSphereBall == 2)
                {

                    if (!HitOne)
                    {
                        Vector3 direction = InstallationSimulationTwo.Instance.DirectionForce.position - collision.transform.position;
                        Vector3 direction2 = InstallationSimulationTwo.Instance.MagnetPoint.position - transform.position;



                        if (GetComponent<InteractableObjects>().MaterialSphere == "Steel")
                        {
                            AddForce = Random.Range(26, 61);
                            AddForce2 = Random.Range(0, 21);
                        }
                        else if (GetComponent<InteractableObjects>().MaterialSphere == "Wood") 
                        {
                            AddForce = Random.Range(35, 71);
                            AddForce2 = Random.Range(0, 31);
                        }
                      


                        collision.transform.GetComponent<Rigidbody>().AddForce(direction * AddForce);

                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GetComponent<Rigidbody>().AddForce(direction2 * AddForce2);


                        HitOne = true;

                        Hit = true;
                        Hit2 = true;


                        _ballTwo = collision.gameObject;

                        _posBall.Add(_ballTwo.transform.position);
                        _posBall2.Add(transform.position);
                    }
                }
            }

        }
    }


    private void Update()
    {
        if (Hit)
        {
            AddPosition();

        }

        if (Hit2)
        {
            AddPosition2();

        }

    }

    private void AddPosition()
    {

        _posBall.Add(_ballTwo.transform.position);

        if (Vector3.Distance(_posBall[index], InstallationSimulationTwo.Instance.DirectionForce.position) < Vector3.Distance(_posBall[index + 1], InstallationSimulationTwo.Instance.DirectionForce.position))
        {
            _ballTwo.GetComponent<Rigidbody>().isKinematic = true;
            Hit = false;
            index = -1;
            _posBall.Clear();

        }

        index++;

    }
    private void AddPosition2()
    {

        _posBall2.Add(transform.position);

        //Debug.Log(Vector3.Distance(_posBall2[index2], InstallationSimulationTwo.Instance.MagnetPoint.position));

        if (Vector3.Distance(_posBall2[index2], InstallationSimulationTwo.Instance.MagnetPoint.position) < Vector3.Distance(_posBall2[index2 + 1], InstallationSimulationTwo.Instance.MagnetPoint.position))
        {

            Debug.Log("WORK2");
            GetComponent<Rigidbody>().isKinematic = true;
            Hit2 = false;
            index2 = -1;
            _posBall2.Clear();

        }
        index2++;

    }
}


