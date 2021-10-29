using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{
    private Animator anim;
    public DruidControl druidControl;
    public bool isCaptured1 = false;
    public bool isCaptured2 = false;
    public bool isCaptured3 = false;

    //Getting Hinthandler
    public hintHandler hintHandler; 



    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControl>();
        anim = GetComponent<Animator>();
        hintHandler = FindObjectOfType<hintHandler>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {


        Debug.Log("Collision?");

        if (gameObject.tag == "checkpoint1" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            Debug.Log(" HELLOOO CHECKPOINT1");

            if (!isCaptured1)
            {
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(24, 8);
                hintHandler.Checkpoint1Reached = true;
            }
        }

        if (gameObject.tag == "checkpoint2" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            Debug.Log("Checkpoint2");
            if (!isCaptured2)
            {
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(73, 8);
            }
        }

        if (gameObject.tag == "checkpoint3" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            Debug.Log("Checkpoint3");
            if (!isCaptured3)
            {
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(112, 8);
            }

        }
    }
}
