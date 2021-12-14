using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{

    void OnMouseDown()
    {
        loadlevel("MainMenu");
    }
   
    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }
}
