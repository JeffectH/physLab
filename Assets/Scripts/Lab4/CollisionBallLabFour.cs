using System;

using System.Collections.Generic;
using UnityEngine;

public class CollisionBallLabFour : MonoBehaviour
{


    private float _force = 165;
    private List<Vector3> _posBall = new List<Vector3>();
    private GameObject _ballTwo;
    private int index = 0;
    public static bool Hit;
    public static Action UpdateLegth;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.GetComponent<InteractableObjects>())
        {
            if (collision.transform.GetComponent<InteractableObjects>().NumSphereBall == 2)
            {
                _posBall.Add(transform.position);

                collision.transform.GetComponent<Rigidbody>().AddForce(Vector3.right * 50);

                switch (SwitchInstalation._index)
                {
                    case 0:
                        _force = UnityEngine.Random.Range(165f, 210f);
                        break;
                    case 1:
                        _force = UnityEngine.Random.Range(165f,210f);
                        break;
                    case 2:
                        _force = UnityEngine.Random.Range(150f, 185f);
                        break;
                    case 3:
                        _force = UnityEngine.Random.Range(150f, 175f);
                        break;
                }
                GetComponent<Rigidbody>().AddForce(Vector3.left * _force);
                _ballTwo = collision.gameObject;

                Hit = true;

            }
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

        _posBall.Add(transform.position);

        if (Vector3.Distance(_posBall[index], SwitchInstalation.PointInstalation.position) < Vector3.Distance(_posBall[index + 1], SwitchInstalation.PointInstalation.position))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            _ballTwo.GetComponent<Rigidbody>().isKinematic = true;
            UpdateLegth.Invoke();
            Hit = false;
            index = -1;
            _posBall.Clear();

        }
        index++;

    }
}
