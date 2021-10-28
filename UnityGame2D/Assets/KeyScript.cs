using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    GameObject barrier1Body;
    GameObject barrier2Body;
    GameObject barrier3Body;

    void Awake()
    {
        barrier1Body = GameObject.Find("Barrier1");
        barrier2Body = GameObject.Find("Barrier2");
        barrier3Body = GameObject.Find("Barrier3");

    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {


        Debug.Log("Collision?");

        if (gameObject.tag == "key1" && collision.collider.tag == "Player")
        {
            Debug.Log("KEY");
            Destroy(gameObject);
            Destroy(barrier1Body);
        }

        if (gameObject.tag == "key2" && collision.collider.tag == "Player")
        {
            Debug.Log("KEY");
            Destroy(gameObject);
            Destroy(barrier2Body);
        }

        if (gameObject.tag == "key3" && collision.collider.tag == "Player")
        {
            Debug.Log("KEY");
            Destroy(gameObject);
            Destroy(barrier3Body);
        }
    }


}