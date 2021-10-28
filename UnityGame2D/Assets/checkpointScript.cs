using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{

    GameObject playerObject;
    public DruidControl druidControl;
    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControl>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {


        Debug.Log("Collision?");

        if (gameObject.tag == "checkpoint1" && collision.collider.tag == "Player")
        {
            Debug.Log("Checkpoint1");
            druidControl.coord1 = -10.56625f;
            druidControl.coord2 = 1.1f;

        }

        if (gameObject.tag == "checkpoint2" && collision.collider.tag == "Player")
        {
            Debug.Log("Checkpoint2");
        }

        if (gameObject.tag == "checkpoint3" && collision.collider.tag == "Player")
        {
            Debug.Log("Checkpoint3");
            
        }
    }
}
