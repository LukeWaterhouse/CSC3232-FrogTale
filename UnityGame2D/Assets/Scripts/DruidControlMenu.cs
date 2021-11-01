using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DruidControlMenu : MonoBehaviour

{
    //Frog player stats
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpPower = 3;

    //Referencing Frogs object components
    private Rigidbody2D body;
    private Animator anim;   
    private CapsuleCollider2D collisionbody;

    //grounded or falling bools
    private bool grounded;
    private bool falling;   

    GameObject iceBlock;

    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();       
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

        if (collision.gameObject.tag == "Level1")
        {
            Debug.Log("Load Level 1");
            loadlevel("Level1");
        }
    }


    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }

}
