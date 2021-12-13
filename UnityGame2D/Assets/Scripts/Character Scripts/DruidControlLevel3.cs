using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DruidControlLevel3 : MonoBehaviour

{
    //Frog player stats
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpPower = 3;


    GameObject levelBarrier;

     GameObject levelFinished;
    Text keyText;

    public int keyCount = 0;



    //Referencing Frogs object components
    private Rigidbody2D body;
    private Animator anim;   
    private CapsuleCollider2D collisionbody;

    //Instantiate Hinthandler
    //public hintHandler hintHandler;

    //grounded or falling bools
    private bool grounded;
    private bool falling;        

    //Respawn Location(Used for checkpoints)
    public Vector2 coord1;   

    //Referencing physics materials
    [SerializeField] public PhysicsMaterial2D iceBlockMaterial;
    [SerializeField] public PhysicsMaterial2D highFrictionMaterial;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //respawn point
        coord1 = new Vector2(-8, 1);

        //Find Hinthandler
        //hintHandler = FindObjectOfType<hintHandler>();  

        InvokeRepeating("reScan", 3.0f, 3.0f);  
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
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


        
      
    }


    private void OnTriggerEnter2D(Collider2D collision){
       
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



        private IEnumerator EndLevel2()
        {
            yield return new WaitForSeconds(7);
            loadlevel("MainMenu");
        }

         public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    } 
}
