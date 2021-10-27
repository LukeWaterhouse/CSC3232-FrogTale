using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "enemyMask")
        {
            Debug.Log("MASK");
            gameObject.tag = "Untagged";
            body.gravityScale = 3;
            Destroy(gameObject, 1.5f);
        }
        else
        {
            Debug.Log("NOT MASK");
            Destroy(gameObject);
        }
    }
}
