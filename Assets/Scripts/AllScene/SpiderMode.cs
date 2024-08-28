using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMode : MonoBehaviour
{
    [SerializeField] private List<GameObject> _smHints;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Interactable.Instance.is2DRay)
            {
                if (!CaliperMode.ModeActive)
                {
                    _smHints[0].SetActive(true);
                }
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Interactable.Instance.is2DRay)
            {
                if (!CaliperMode.ModeActive)
                {
                    _smHints[0].SetActive(false);
                }
            }
        }
        

       
    }

    private void ShowHints(bool isView,int indexHint)
    {
        _smHints[indexHint].SetActive(isView);
    }
}