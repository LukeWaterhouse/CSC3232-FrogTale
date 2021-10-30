using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isMoving = true;
    Vector2 currentEulerAngles;
    public float charFacing = 0;

    [SerializeField] public int Health = 60;

    [SerializeField] public float moveSpeed = 4;
    [SerializeField] public float jumpSpeed = 8;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        body = GetComponent<Rigidbody2D>();
        Invoke("EnemyJump", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }

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

        Debug.Log("FLIPRIGHT!"+ currentEulerAngles);

        transform.eulerAngles = new Vector3(0, 180, 0);
        //transform.localScale = new Vector2(charFacing*-1, 10);
        moveSpeed = moveSpeed * -1;
    }

    private void FlipCharacterLeft()
    {

        Debug.Log("FLIPLEFT!" + currentEulerAngles);

        transform.eulerAngles = new Vector3(0, 0, 0);
        //transform.localScale = new Vector2(charFacing*-1, 10);
        moveSpeed = moveSpeed * -1;
    }

    void EnemyJump()
    {
        float randomTime = Random.Range(3, 5);

        // do you code
        Debug.Log("JUMP");
        body.velocity = new Vector2(body.velocity.y, jumpSpeed);
        Invoke("EnemyJump", randomTime);

    }
}
