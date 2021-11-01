using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{

    // Start is called before the first frame update

    void OnMouseDown()
    {
        Debug.Log("CLICKED");
        loadlevel("MainMenu");

    }

   
    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }
}
