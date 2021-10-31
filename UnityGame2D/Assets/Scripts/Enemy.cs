using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isMoving = true;
    Vector2 currentEulerAngles;
    public float charFacing = 0;
    private Rigidbody2D body;

    //Enemy Stats
    [SerializeField] public int Health = 60;
    [SerializeField] public float moveSpeed = 4;
    [SerializeField] public float jumpSpeed = 8;
  
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Invoke("EnemyJump", 0.5f);
    }

    void Update()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }

    //Flip enemy if they hit a post
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "rightPost")
        {
            FlipCharacterRight();            
        }

        if (collision.gameObject.tag == "leftPost")
        {
            FlipCharacterLeft();
        }
    }

    private void FlipCharacterRight()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        moveSpeed = moveSpeed * -1;
    }

    private void FlipCharacterLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        moveSpeed = moveSpeed * -1;
    }

    void EnemyJump()
    {
        float randomTime = Random.Range(3, 5);
        body.velocity = new Vector2(body.velocity.y, jumpSpeed);
        Invoke("EnemyJump", randomTime);
    }
}
