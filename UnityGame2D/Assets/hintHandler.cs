using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintHandler : MonoBehaviour
{

    public DruidControl druidControl;

    //Hint game objects
    public GameObject areaComplete; //done
    public GameObject diedToBoss;
    public GameObject hasntGotKeyYet;
    public GameObject hasntShotYet;
    public GameObject notDamagedEnemyYet;


    public bool hasShownAreaComplete1Hint = false;
    public bool hasShownAreaComplete2Hint = false;
    public bool hasShownAreaComplete3Hint = false;

    public bool Key1Captured = false;
    public bool Key2Captured = false;
    public bool Key3Captured = false;

    public bool Checkpoint1Reached = false;
    public bool Checkpoint2Reached = false;
    public bool Checkpoint3Reached = false;    

    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControl>();

        //Find hint objects
        areaComplete = GameObject.Find("AreaComplete");
        diedToBoss = GameObject.Find("DiedToBoss");
        hasntGotKeyYet = GameObject.Find("HasntGotKeyYet");
        hasntShotYet = GameObject.Find("HasShotYet");
        notDamagedEnemyYet = GameObject.Find("NotDamagedEnemyYet");
    }

    void Update()
    {
        if (Key1Captured && !hasShownAreaComplete1Hint)
        {
            StartCoroutine(ShowAreaComplete1Hint(areaComplete));
            hasShownAreaComplete1Hint = true;
        }

        if (Key2Captured && !hasShownAreaComplete2Hint)
        {
            StartCoroutine(ShowAreaComplete2Hint(areaComplete));
            hasShownAreaComplete2Hint = true;
        }

        if (Key3Captured && !hasShownAreaComplete3Hint)
        {
            StartCoroutine(ShowAreaComplete3Hint(areaComplete));
            hasShownAreaComplete3Hint = true;
        }
    }


    public IEnumerator ShowAreaComplete1Hint(GameObject hint)
    {
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("Show hint function");
        
        yield return new WaitForSeconds(10);

        if (!Checkpoint1Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
        } 
    }

    public IEnumerator ShowAreaComplete2Hint(GameObject hint)
    {
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("Show hint function");

        yield return new WaitForSeconds(10);

        if (!Checkpoint2Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public IEnumerator ShowAreaComplete3Hint(GameObject hint)
    {
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("Show hint function");

        yield return new WaitForSeconds(10);

        if (!Checkpoint3Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }




    public void ResetHints()
    {
        //Reset hint visibilities
        areaComplete.GetComponent<SpriteRenderer>().enabled = false;
        diedToBoss.GetComponent<SpriteRenderer>().enabled = false;
        hasntGotKeyYet.GetComponent<SpriteRenderer>().enabled = false;
        hasntShotYet.GetComponent<SpriteRenderer>().enabled = false;
        notDamagedEnemyYet.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    
}
