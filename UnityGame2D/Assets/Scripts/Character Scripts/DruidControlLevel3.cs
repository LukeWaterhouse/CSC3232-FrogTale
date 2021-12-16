using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DruidControlLevel3 : DruidControlLevelBase

{


    public AudioSource underwaterAudio;

    public int tadpoleCount = 0;
    Text tadpoleText;
    GameObject levelBarrier;

    public ParticleSystem.MainModule dustSettings;

    public ParticleSystem splash;

    public float underwaterLevel = -5;

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
        dustSettings = dust.main;

    }

    public override void Update()
    {
        //Character Controller
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        //Flip character on direction change
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        //Jumping
        if ((Input.GetKey(KeyCode.Space)) && grounded)
        {

            audioManager.Play("FrogJump");
            Jump();

        }


        if (transform.position.y <= underwaterLevel)
        {

            //when below y value set character to underwater mode
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
            dustSettings.startColor = new Color(0, 1, 1, 1);
            dustSettings.duration = 5f;
            body.gravityScale = 0.35f;
            underwaterAudio.volume = 1;

        }
        else
        {
            //when above y value set character to normal mode
            GetComponent<SpriteRenderer>().color = Color.white;
            dustSettings.startColor = new Color(0.5188f, 0.5188f, 0.5188f, 1);
            dustSettings.duration = 0.1f;
            body.gravityScale = 1.6f;
            underwaterAudio.volume = 0;
        }

        //Setting animation based on state
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //play splash noise and particle effect when entering/leaving water
        if (collision.GetComponent<Collider2D>().tag == "Splash")
        {

            audioManager.Play("Splash");
            splash.Play();
            Debug.Log("SPLASH!");
        }


        //end level 3
        if (collision.GetComponent<Collider2D>().tag == "EndLevel3")
        {
            levelFinished.GetComponent<SpriteRenderer>().enabled = true;
            audioManager.Play("ChildrenSafe");
            Debug.Log("Ending Level 3");
            StartCoroutine(EndLevel());
        }


    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (collision.collider.tag == "Tadpole")
        {

            //disable tadpole, increase count and update ui on collision
            if (tadpoleCount < 200)
            {

                audioManager.Play("Pop");
                tadpoleCount += 1;
                Debug.Log(tadpoleCount);
                tadpoleText.text = $"Tadpoles: {tadpoleCount}/200";
                collision.gameObject.SetActive(false);

            }

            if (tadpoleCount >= 200)
            {

                audioManager.Play("Pop");
                collision.gameObject.SetActive(false);
                tadpoleText.text = "That'll do!'";
                Destroy(levelBarrier);

            }



        }




    }


}
