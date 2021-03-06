using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;



public class DruidControlLevelBase : DruidControlBase

{
    //Instantiate Hinthandler
    public hintHandler hintHandler;


    //Respawn Location(Used for checkpoints)
    public Vector2 coord1;

    //Instantiate Level Finished message
    public GameObject levelFinished;

    //grav powerup strength
    [SerializeField] public float gravPower = 0.1f;


    public int randomNumber;
    public Random rand = new Random();

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
            GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetTrigger("death");
            body.velocity = new Vector2(body.velocity.x, 8);
            body.gravityScale = 5f;

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
    }

    public IEnumerator StatReset(GameObject collision)
    {
        yield return new WaitForSeconds(3);
        body.gravityScale = 1.8f;
        GetComponent<SpriteRenderer>().color = Color.white;
        collision.SetActive(true);
    }

    public IEnumerator Respawn(GameObject collision)
    {

        //Randomly play a death sound 
        randomNumber = rand.Next(4);

        switch (randomNumber)
        {
            case 0:
                audioManager.Play("FrogDeath1");
                break;
            case 1:
                audioManager.Play("FrogDeath2");
                break;
            case 2:
                audioManager.Play("FrogDeath3");
                break;
            case 3:
                audioManager.Play("FrogDeath4");
                break;
            default:
                break;
        }



        yield return new WaitForSeconds(2);
        body.velocity = new Vector2(0, 0);
        GetComponent<CapsuleCollider2D>().enabled = true;
        body.gravityScale = 1.8f;
        Camera.main.GetComponent<CameraControl>().enabled = true;
        gameObject.transform.position = coord1;
        anim.SetTrigger("jump");
    }

    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(7);
        loadlevel("MainMenu");
    }


}
