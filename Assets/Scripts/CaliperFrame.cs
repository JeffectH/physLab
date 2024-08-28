using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperFrame : MonoBehaviour
{
    //Restriction of movement
    [SerializeField] private float _rodShiftMax;
    [SerializeField] private float _rodShiftMin;

    public void MoveFrame(float move) 
    {

        //float xPos = Mathf.Clamp(transform.localPosition.x, _rodShiftMin, _rodShiftMax);




        transform.localPosition = Vector3.MoveTowards(new Vector3(_rodShiftMax, transform.localPosition.y, transform.localPosition.z), new Vector3(_rodShiftMin, transform.localPosition.y, transform.localPosition.z), move*40);


    }
}
