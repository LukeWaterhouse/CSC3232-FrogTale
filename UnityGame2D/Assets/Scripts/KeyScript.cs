using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public DruidControl druidControl;

    //Getting Hinthandler
    public hintHandler hintHandler;

    //Getting Barrier bodies to destroy
    GameObject barrier1Body;
    GameObject barrier2Body;
    GameObject barrier3Body;


    AudioManager audioManager;
    void Awake()
    {
        //Finding things
        druidControl = FindObjectOfType<DruidControl>();
        barrier1Body = GameObject.Find("Barrier1");
        barrier2Body = GameObject.Find("Barrier2");
        barrier3Body = GameObject.Find("Barrier3");
        hintHandler = FindObjectOfType<hintHandler>();
        audioManager = FindObjectOfType<AudioManager>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "key1" && collision.collider.tag == "Player")
        {
            audioManager.Play("KeyCollection");
            Destroy(gameObject);
            Destroy(barrier1Body);
            hintHandler.Key1Captured = true;
        }

        if (gameObject.tag == "key2" && collision.collider.tag == "Player")
        {
            audioManager.Play("KeyCollection");
            Destroy(gameObject);
            Destroy(barrier2Body);
            hintHandler.Key2Captured = true;
        }

        if (gameObject.tag == "key3" && collision.collider.tag == "Player")
        {
            audioManager.Play("KeyCollection");
            Destroy(gameObject);
            Destroy(barrier3Body);
            hintHandler.Key3Captured = true;
        }
    }


}
