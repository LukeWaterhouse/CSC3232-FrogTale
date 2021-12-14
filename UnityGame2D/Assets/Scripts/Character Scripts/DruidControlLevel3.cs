using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DruidControlLevel3 : DruidControlLevelBase

{


    public int tadpoleCount = 0;
    Text tadpoleText;
    GameObject levelBarrier;

    public override void Awake()
    {

        //base class stuff
        audioManager = FindObjectOfType<AudioManager>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //respawn point
        coord1 = new Vector2(-8, 1);

        //Find Hinthandler
        hintHandler = FindObjectOfType<hintHandler>();

        //find level finished method;
        levelFinished = GameObject.Find("FinishedLevel");

        //Level3 additions
        tadpoleText = GameObject.Find("tadpoleText").GetComponent<Text>();
        levelBarrier = GameObject.Find("Level3EndBarrier");

    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (collision.collider.tag == "Tadpole")
        {


            if (tadpoleCount < 200)
            {

                audioManager.Play("Pop");
                tadpoleCount += 1;
                Debug.Log(tadpoleCount);
                tadpoleText.text = $"Tadpole Count: {tadpoleCount}/200";
                collision.gameObject.SetActive(false);

            }

            if(tadpoleCount>=200){

                tadpoleText.text = "Thats enough! Let's go home";
                Destroy(levelBarrier);
                
            }



        }




    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

    }


}
