using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DruidControl : MonoBehaviour

{
    //Frog player stats
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpPower = 3;
    [SerializeField] private float gravPower = 0.1f;

    //Referencing Frogs object components
    private Rigidbody2D body;
    private Animator anim;   
    private CapsuleCollider2D collisionbody;

    //Instantiate Hinthandler
    public hintHandler hintHandler;

    //grounded or falling bools
    private bool grounded;
    private bool falling;


    //Picked up LeafSlingerCheck
    public bool leafSlingerPickedUp = false;

    //LeafSlinger aim object
    GameObject shootingObject;

    //Respawn Location(Used for checkpoints)
    public Vector2 coord1;

    //Instantiate Level Finished message
    GameObject level1Finished;
    

    //Referencing physics materials
    [SerializeField] public PhysicsMaterial2D iceBlockMaterial;
    [SerializeField] public PhysicsMaterial2D highFrictionMaterial;

    GameObject iceBlock;


    AudioManager audioManager;

    private void Awake()
    {

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
        level1Finished = GameObject.Find("FinishedLevel1");



    }

    private void Update()
    {
        //Character Controller
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        //Flip character on direction change
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(5,5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-5,5);
        }
        
        //Jumping
        if ((Input.GetKey(KeyCode.Space))  && grounded)
        {
            audioManager.Play("FrogJump");
            Jump();
        }

        //Setting animation based on state
        anim.SetBool("run", horizontalInput !=0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {       
        body.velocity = new Vector2( body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "purplePortal")
        {
            hintHandler.EnteredPurplePortal = true;
        }

        if (collision.GetComponent<Collider2D>().tag == "EndLevel1")
        {
            StaticLevelBools.isLevel2Unlocked = true;
            level1Finished.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Ending Level 1");
            StartCoroutine(EndLevel1());
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            audioManager.Play("FrogDeath1");        
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

        private IEnumerator StatReset(GameObject collision)
        {
            yield return new WaitForSeconds(3);
            body.gravityScale = 1.8f;
            GetComponent<SpriteRenderer>().color = Color.white;
            collision.SetActive(true);
        }

        private IEnumerator Respawn(GameObject collision)
        {
            yield return new WaitForSeconds(2);
            body.velocity = new Vector2(0, 0);
            GetComponent<CapsuleCollider2D>().enabled = true;
            body.gravityScale = 1.8f;
            Camera.main.GetComponent<CameraControl>().enabled = true;
            Debug.Log(coord1);
            gameObject.transform.position = coord1;
            anim.SetTrigger("jump");
        }

        private IEnumerator EndLevel1()
        {
            yield return new WaitForSeconds(7);
            loadlevel("MainMenu");
        }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }


}
