using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCursor : MonoBehaviour
{
    public static bool isView;
    private void Update()
    {
        if (isView)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
