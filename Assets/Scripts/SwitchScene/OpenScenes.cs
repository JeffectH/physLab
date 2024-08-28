using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScenes : MonoBehaviour
{
    public void LoadScene(int index) 
    {
       // if (index == 5)
        //{
        //    MyVariables.lr_num = 9;
        //}
        SceneManager.LoadSceneAsync(index);
    }

    public void set_type_mech()
    {
        MyVariables.lr_type = 0;
        SceneManager.LoadSceneAsync(2);
    }
    public void set_type_mol()
    {
        MyVariables.lr_type = 1;
        SceneManager.LoadSceneAsync(3);
    }
    public void set_type_el()
    {
        MyVariables.lr_type = 2;
        SceneManager.LoadSceneAsync(4);
    }
    public void back_for_sel() 
    {
        SceneManager.LoadSceneAsync(MyVariables.lr_type+2);
    }
    public void create_level(int type_env) 
    {
        MyVariables.env_type= type_env;
        SceneManager.LoadSceneAsync(MyVariables.env_type+6);
    }
    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
