using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //grounded or falling bools
    private bool grounded;
    private bool falling;


    //Picked up LeafSlingerCheck
    public bool leafSlingerPickedUp = false;

    //LeafSlinger aim object
    GameObject shootingObject;

    //Respawn Location(Used for checkpoints)
    public Vector2 coord1;

    //Hint game objects
    public GameObject areaComplete;
    public GameObject diedToBoss;
    public GameObject hasntGotKeyYet;
    public GameObject hasntShotYet;
    public GameObject notDamagedEnemyYet;

    private void Awake()
    {   


        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shootingObject = GameObject.Find("Aim");
        coord1 = new Vector2(-8, 1);

        shootingObject.GetComponent<Shooting>().enabled = false;
        shootingObject.GetComponent<AimScript>().enabled = false;


        //Find hint objects
        areaComplete = GameObject.Find("AreaComplete");
        diedToBoss = GameObject.Find("DiedToBoss");
        hasntGotKeyYet = GameObject.Find("HasntGotKeyYet");
        hasntShotYet = GameObject.Find("HasShotYet");
        notDamagedEnemyYet = GameObject.Find("NotDamagedEnemyYet");

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

        if (collision.collider.tag == "greenPortal")
        {
            gameObject.transform.position = new Vector2(59.5f, 32f);
        }

        if(collision.collider.tag == "LeafSlinger")
        {
            leafSlingerPickedUp = true;
            shootingObject.GetComponent<Shooting>().enabled = true;
            shootingObject.GetComponent<AimScript>().enabled = true;
            Destroy(collision.gameObject);
        }

        if((collision.collider.tag == "killObject") || (collision.collider.tag == "EnemyBody") || (collision.collider.tag == "enemyMask"))
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
        Debug.Log(coord1);
        gameObject.transform.position = coord1;
        anim.SetTrigger("jump");
    }

    
}
