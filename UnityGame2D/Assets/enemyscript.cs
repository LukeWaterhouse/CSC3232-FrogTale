using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float moveSpeed = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);

    }
}
