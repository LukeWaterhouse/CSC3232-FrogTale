using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{

    
    private Animator anim;

    
    public DruidControlLevel1 druidControl;

    //Checkpoint captured bools
    public bool isCaptured1 = false;
    public bool isCaptured2 = false;
    public bool isCaptured3 = false;

    //Getting Hinthandler
    public hintHandler hintHandler; 


     AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControlLevel1>();
        anim = GetComponent<Animator>();
        hintHandler = FindObjectOfType<hintHandler>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    //When player hits the checkpoints play checkpoint flag animation and set spawn point
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.tag == "checkpoint1" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            if (!isCaptured1)
            {
                audioManager.Play("Checkpoint");
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(24, 8);
                hintHandler.Checkpoint1Reached = true;
            }
        }

        if (gameObject.tag == "checkpoint2" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            if (!isCaptured2)
            {
                audioManager.Play("Checkpoint");
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(73, 8);
                hintHandler.Checkpoint2Reached = true;
            }
        }

        if (gameObject.tag == "checkpoint3" && collision.GetComponent<Collider2D>().tag == "Player")
        {
            if (!isCaptured3)
            {
                audioManager.Play("Checkpoint");
                anim.SetBool("isCaptured", true);
                druidControl.coord1 = new Vector2(112, 8);
                hintHandler.Checkpoint3Reached = true;
            }

        }
    }
}
