using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DruidControlLevel1 : DruidControlLevelBase

{  

    //Picked up LeafSlingerCheck
    public bool leafSlingerPickedUp = false;

    //LeafSlinger aim object
    GameObject shootingObject;
    
    //Referencing physics materials
    [SerializeField] public PhysicsMaterial2D iceBlockMaterial;
    [SerializeField] public PhysicsMaterial2D highFrictionMaterial;

    GameObject iceBlock;

   

    public override void Awake()
    {

        //base class stuff
        audioManager = FindObjectOfType<AudioManager>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Initially disable the shooting scripts
        shootingObject = GameObject.Find("Aim");
        shootingObject.GetComponent<Shooting>().enabled = false;
        shootingObject.GetComponent<AimScript>().enabled = false;

        //respawn point
        coord1 = new Vector2(-8, 1);

        //Find Hinthandler
        hintHandler = FindObjectOfType<hintHandler>();

        //Get iceblock and set material to high friction
        iceBlock = GameObject.Find("iceBlock");
        iceBlock.GetComponent<BoxCollider2D>().sharedMaterial = highFrictionMaterial;

        //find level finished method;
        levelFinished = GameObject.Find("FinishedLevel");



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "purplePortal")
        {
            hintHandler.EnteredPurplePortal = true;
        }

        if (collision.GetComponent<Collider2D>().tag == "EndLevel1")
        {
            audioManager.Play("HomeSweetHome");
            StaticLevelBools.isLevel2Unlocked = true;
            levelFinished.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Ending Level 1");
            StartCoroutine(EndLevel());
        }


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

        if (collision.collider.tag == "greenPortal")
        {
            gameObject.transform.position = new Vector2(59.5f, 32f);
        }

        if(collision.collider.tag == "LeafSlinger")
        {
            audioManager.Play("WeaponPickup");
            leafSlingerPickedUp = true;
            //enable shooting scripts
            shootingObject.GetComponent<Shooting>().enabled = true;
            shootingObject.GetComponent<AimScript>().enabled = true;
            Destroy(collision.gameObject);
            //notify hint handler
            hintHandler.LeafSlingerAcquired = true;
            hintHandler.EnteredPurplePortal = false;
        }

        if (collision.collider.tag == "IcePowerup")
        {
            audioManager.Play("IcePowerup");
            Destroy(collision.gameObject);
            //change ice block physics material
            iceBlock.GetComponent<BoxCollider2D>().sharedMaterial = iceBlockMaterial;
            iceBlock.GetComponent<SpriteRenderer>().color = Color.white;
            hintHandler.HasIcePowerup = true;
        }

        if (collision.collider.tag == "StoneWallCollider")
        {
            hintHandler.HitStoneWall = true;
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
    }

}
