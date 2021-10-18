using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruidControl : MonoBehaviour


{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpPower = 10;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, body.velocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2( body.velocity.x, jumpPower);
        }
    }
}
