using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DruidControlBase : MonoBehaviour

{


    public ParticleSystem dust;

    //Frog player stats
    [SerializeField] public float moveSpeed = 8;
    [SerializeField] public float jumpPower = 3;

    //Referencing Frogs object components
    public Rigidbody2D body;
    public Animator anim;   
    public CapsuleCollider2D collisionbody;

    //grounded or falling bools
    public bool grounded;
    public bool falling;   

    public AudioManager audioManager;
    public virtual void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    

        audioManager = FindObjectOfType<AudioManager>();

        
    }

    public virtual void Update()
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


    //Jump Method
    public void Jump()
    {     
        Debug.Log("JuMP");  
        CreateDust();
        body.velocity = new Vector2( body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
        grounded = false;
    }



    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }


    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }


    public void CreateDust(){
        dust.Play();
    }

}
