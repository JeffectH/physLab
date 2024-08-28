using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Scene(int index) 
    {
        StartCoroutine(LoadYourAsyncScene(index)); 
    }
    
    IEnumerator LoadYourAsyncScene(int index)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Exit() 
    {
        Application.Quit();
    }

}
