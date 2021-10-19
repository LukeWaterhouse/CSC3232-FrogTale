using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruidControl : MonoBehaviour


{

    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpPower = 3;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        //changes velocity based on key inputs
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
        //jump

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(5,5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-5,5);
        }
        
        
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }


        //animation

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

        if (collision.gameObject.tag == "GravPowerup")
        {
            //Destroy(collision.gameObject);
            body.gravityScale = 0.4f;
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(StatReset());
        }
    }

    private IEnumerator StatReset()
    {
        yield return new WaitForSeconds(7);
        body.gravityScale = 1;
        GetComponent<SpriteRenderer>().color = Color.white;

    }
}
