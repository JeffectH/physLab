using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptylr : MonoBehaviour
{
 public FirstPersonControllerTim  player;
    public Camera Maincamera;
    public GameObject crosshair;
    public GameObject using_text;
    public GameObject using_pause;

    
void Start()
{
    
    using_text.SetActive(false);
    using_pause.SetActive(false);
}
    void SetCursorLock(bool isLocked)
    {
        
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }
    // Update is called once per frame
    void Update()
    {
        
        

        if (Input.GetKeyDown(KeyCode.E)&&!using_pause.activeSelf)
        {
            SetCursorLock(true);


            Maincamera.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
           

        }
    }
}
