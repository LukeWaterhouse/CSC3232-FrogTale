using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isMoving = true;
    [SerializeField] private float moveSpeed = 8;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Debug.Log("isMoving");
            Movement();
        }
        

    }

    void Movement()
    {
        body.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, body.velocity.y);

    }
}
