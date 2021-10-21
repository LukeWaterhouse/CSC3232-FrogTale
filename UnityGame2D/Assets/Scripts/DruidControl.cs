using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruidControl : MonoBehaviour


{

    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float jumpPower = 3;
    [SerializeField] private float gravPower = 0.1f;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool falling;
    private CapsuleCollider2D collisionbody;

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

        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.collider.name);

        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if(collision.collider.tag == "killObject")
        {
            
            anim.SetTrigger("death");
            body.velocity = new Vector2(body.velocity.x, 8);
            body.gravityScale = 5f;
            GetComponent<CapsuleCollider2D>().enabled = false;
            Camera.main.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Camera.main.GetComponent<CameraControl>().enabled = false;
            StartCoroutine(Respawn(collision.gameObject));
        }


        if (collision.gameObject.tag == "GravPowerup")
        {
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
        Debug.Log("Respawning");
        yield return new WaitForSeconds(2);
        body.velocity = new Vector2(0, 0);
        GetComponent<CapsuleCollider2D>().enabled = true;
        body.gravityScale = 1.8f;
        Camera.main.GetComponent<CameraControl>().enabled = true;
        gameObject.transform.position = new Vector2(-8.35f, 1f);
        anim.SetTrigger("jump");



    }
}
