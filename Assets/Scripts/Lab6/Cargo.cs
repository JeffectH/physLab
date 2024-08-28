using System;
using UnityEngine;
public class Cargo : MonoBehaviour
{
    public static Action offFixStopWatch;

    private void OnCollisionEnter(Collision collision)
    {
        if (InstalationThree.instance._isMagnitActiv)
        {
            if (collision.transform.GetComponent<FloorLabThree>())
            {
                offFixStopWatch.Invoke();
            }
        }
    }

}
