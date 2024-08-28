using System;
using System.Collections.Generic;
using UnityEngine;


public class ViewNameTools : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _studentModeText = new List<GameObject>(); //объекты которые необходимо отобразить

    [SerializeField] private GameObject _player; //игрок

    [SerializeField] private int _mode;

    private void Start()
    {
        ShowName(false);
        _mode = 1;
    }

    private void Update()
    {
        if (!Interactable.Instance.is2DRay)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (_mode == 1)
                {
                    ShowName(true);
                    LookToPlayer();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                ShowName(false);
            }  
        }

     
    }

    private void LookToPlayer() //функция поворота текста на игрока
    {
        var position = _player.transform.position;

        foreach (var count in _studentModeText)
        {
            var toInstalation = _studentModeText[0].transform.position - position;
            var toInstalationXZ = new Vector3(toInstalation.x, 0, toInstalation.z);
            count.transform.rotation = Quaternion.LookRotation(toInstalationXZ);
        }
    }

    public void ShowName(bool view) //функция отображения текста
    {
        foreach (var count in _studentModeText)
        {
            count.SetActive(view);
        }
    }
}