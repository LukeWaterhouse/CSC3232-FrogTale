using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWall : MonoBehaviour
{
    [SerializeField] public bool isTransparent = false;

    SpriteRenderer sprite;
    BoxCollider2D wallCollider;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<BoxCollider2D>();

        Invoke("ChangeWallStatus", 3);
    }


    void ChangeWallStatus()
    {
        if (!isTransparent)
        {
            StartCoroutine(changeToTransparent());

        }
        else
        {
            StartCoroutine(changeToSolid());
        }
    }

    private IEnumerator changeToTransparent()

    //Flash the walls transparency before changing
    {
        for (var n = 0; n < 13; n++)
        {
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1f, 1f, 1f, .6f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1f, 1f, 1f, 1f);

        }

        //change the walls status, change after random time
        float randomTime = Random.Range(13, 22);
        Debug.Log("change colour");
        sprite.color = new Color(1f, 1f, 1f, .2f);
        gameObject.layer = 0;
        wallCollider.enabled = false;
        isTransparent = true;
        Invoke("ChangeWallStatus", randomTime);

        //updates the pathfinding graph with new wall bounds
        AstarPath.active.UpdateGraphs(gameObject.GetComponent<Collider2D>().bounds);
    }


    private IEnumerator changeToSolid()

    //Flash the walls transparency before changing
    {
        for (var n = 0; n < 13; n++)
        {
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1f, 1f, 1f, .2f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1f, 1f, 1f, 0.6f);

        }
        //change the walls status, change after random time
        float randomTime = Random.Range(13, 22);
        Debug.Log("change colour");
        sprite.color = new Color(1f, 1f, 1f, 1f);
        gameObject.layer = 12;
        wallCollider.enabled = true;
        isTransparent = false;
        Invoke("ChangeWallStatus", randomTime);


        //updates the pathfinding graph with new wall bounds
        AstarPath.active.UpdateGraphs(gameObject.GetComponent<Collider2D>().bounds);
    }
}
