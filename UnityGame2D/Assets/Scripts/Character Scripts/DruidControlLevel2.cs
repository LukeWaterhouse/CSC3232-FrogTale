using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DruidControlLevel2 : DruidControlLevelBase

{
    GameObject levelBarrier;
    Text keyText;
    public int keyCount = 0;


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

        //Level2 additions
        levelBarrier = GameObject.Find("Level2EndBarrier");
        keyText = GameObject.Find("KeyText").GetComponent<Text>();
        InvokeRepeating("reScan", 3.0f, 3.0f);  



    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (collision.gameObject.tag == "Boing")
        {
            audioManager.Play("FrogJump");
        }
     
        //If player hits kill object run death sequence and start respawn coroutine
        if ((collision.collider.tag == "killObject") || (collision.collider.tag == "EnemyBody") || (collision.collider.tag == "enemyMask"))
        {           
            anim.SetTrigger("death");
            body.velocity = new Vector2(body.velocity.x, 8);
            body.gravityScale = 5f;
            GetComponent<CapsuleCollider2D>().enabled = false;
            Camera.main.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Camera.main.GetComponent<CameraControl>().enabled = false;

            StartCoroutine(Respawn(collision.gameObject));
        }


        //If collide with melon change gravity and tint green
        if (collision.gameObject.tag == "GravPowerup")
        {
            audioManager.Play("MelonPowerup");
            collision.gameObject.SetActive(false);
            body.gravityScale = gravPower;
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(StatReset(collision.gameObject));
        }


         if(collision.gameObject.tag == "key"){
            Debug.Log("collided!");
            audioManager.Play("KeyCollection");
            keyCount += 1;
            keyText.text = $"Keys: {keyCount}/8";
            if(keyCount == 8){
                Destroy(levelBarrier);
                keyText.text = "Barrier Destroyed!";
            }
            Debug.Log(keyCount);
            Destroy(collision.gameObject);
        }    
      
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<Collider2D>().tag == "EndLevel2")
        {
            StaticLevelBools.isLevel2Unlocked = true;
            levelFinished.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Ending Level 2");
            StartCoroutine(EndLevel());
        }
    }

         void reScan(){
        AstarPath.active.Scan();
        Debug.Log("scanning");
    }
}
