using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensController : MonoBehaviour
{ 
    public Camera lensCamera;
    public Camera mechCamera;
    public float minLensSize = 0.02f;
    public float maxLensSize = 0.05f;


    void Update()
    {
        MoveLensRenderAndCamera();
    }

    private void MoveLensRenderAndCamera()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        MoveLensCamera();
        MoveLensRender();
    }

    private void MoveLensRender()
    {
        Vector3 mousePosition = Input.mousePosition;
        gameObject.transform.position = mousePosition;
    }

    private void MoveLensCamera()
    {
        Vector3 worldPosition = mechCamera.ScreenToWorldPoint(Input.mousePosition);
        lensCamera.transform.position = worldPosition;
    }

    public void SwitchLensView()
    {
        SetActiveLens(!gameObject.activeSelf);
    }

    public void SetActiveLens(bool state)
    {
        gameObject.SetActive(state);
        Cursor.visible = !state;
    }

    public void SwitchMaxZoom()
    {
        lensCamera.orthographicSize = minLensSize;
    }
    public void SwitchMinZoom()
    {
        lensCamera.orthographicSize = maxLensSize;
    }
}


