using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "enemyMask")
        {
            //If hit mask bounce bullet off
            Debug.Log("MASK");
            gameObject.tag = "Untagged";
            body.gravityScale = 3;
            Destroy(gameObject, 1.5f);
        }
        else
        {
            //Destroy bullet on impact unless mask ^^
            Debug.Log("NOT MASK");
            Destroy(gameObject);
        }
    }
}
