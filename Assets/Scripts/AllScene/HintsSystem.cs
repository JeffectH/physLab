using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class HintsSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<String> _hintsText;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayDistance = 100;


    private void Update()
    {
        if (!Interactable.Instance.is2DRay)
        {
            if (RayCast())
            {
                if (RayCast().layer == 9)
                {
                    _text.text = _hintsText[0];
                }

                if (SceneManager.GetActiveScene().name == "LabNum1")
                {
                    if (RayCast().layer == 10)
                    {
                      
                        if (ObjectMove.Instance.Target == null)
                        {
                            _text.text = _hintsText[1];

                        }
                        
                        if (InstallationSimulationOne.Activate)
                        {
                            _text.text = _hintsText[3];

                        }  
                       
                    }
                    if (ObjectMove.Instance.Target != null)
                    {
                        _text.text = _hintsText[2];
                    }
                }
                else
                {
                    if (RayCast().layer == 10)
                    {
                        _text.text = _hintsText[1];
                    }
                }

                if (RayCast().layer == 0)
                {
                    _text.text = "";
                }
            }
        }
        else
        {
            _text.text = "";
        }
    }

    private GameObject RayCast()
    {
        RaycastHit hit;
        Ray ray;
        ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }
}