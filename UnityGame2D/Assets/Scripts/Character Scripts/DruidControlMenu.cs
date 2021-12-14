using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DruidControlMenu : DruidControlBase

{
    public override void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        //Load level 1 if level 1 portal hit
        if (collision.gameObject.tag == "Level1")
        {
            Debug.Log("Load Level 1");
            loadlevel("Level1");
        }


        //Load level 2 if level 2 portal hit
        if (collision.gameObject.tag == "Level2")
        {
            Debug.Log("Load Level 2");
            loadlevel("Level2");
        }
    }
}
