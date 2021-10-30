using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintHandler : MonoBehaviour
{

    public DruidControl druidControl;

    //Hint game objects
    public GameObject areaComplete; //done
    public GameObject hasntShotYet; //done
    public GameObject noWeaponYet; //done
    public GameObject bossWithWeapon;  //done

    //Area Complete message shown yet?
    public bool hasShownAreaComplete1Hint = false;
    public bool hasShownAreaComplete2Hint = false;
    public bool hasShownAreaComplete3Hint = false;

    //Key Captured yet?
    public bool Key1Captured = false;
    public bool Key2Captured = false;
    public bool Key3Captured = false;


    // Checkpoints reached yet?
    public bool Checkpoint1Reached = false;
    public bool Checkpoint2Reached = false;
    public bool Checkpoint3Reached = false;

    //Leafslinger picked up?
    public bool LeafSlingerAcquired = false;
    public bool HasShotYet = false;
    public bool ShownShootLeafSlingerHintYet = false;

    //EnteredPurplePortal
    public bool EnteredPurplePortal = false;
    public bool HasShownnoWeaponHintYet = false;
    public bool HasShownBossWithWeaponHintYet = false;

    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControl>();

        //Find hint objects
        areaComplete = GameObject.Find("AreaComplete"); //done
        hasntShotYet = GameObject.Find("ShotLeafYet"); // done
        noWeaponYet = GameObject.Find("noWeapon"); // done
        bossWithWeapon = GameObject.Find("bossWithWeapon"); //done
    }

    void Update()
    {

        //Staying too long in 1st area after key captured
        if (Key1Captured && !hasShownAreaComplete1Hint)
        {
            StartCoroutine(ShowAreaComplete1Hint(areaComplete));
            hasShownAreaComplete1Hint = true;
        }

        //Staying too long in 2nd area after key captured
        if (Key2Captured && !hasShownAreaComplete2Hint)
        {
            StartCoroutine(ShowAreaComplete2Hint(areaComplete));
            hasShownAreaComplete2Hint = true;
        }

        //Staying too long in 3rd area after key captured
        if (Key3Captured && !hasShownAreaComplete3Hint)
        {
            StartCoroutine(ShowAreaComplete3Hint(areaComplete));
            hasShownAreaComplete3Hint = true;
        }

        //Has LeafSlinger but isn't shooting it for a while
        if(LeafSlingerAcquired && !ShownShootLeafSlingerHintYet)
        {
            StartCoroutine(ShowShootLeafSlingerHint(hasntShotYet));
            ShownShootLeafSlingerHintYet = true;
        }

        //Entering Boss area without a weapon
        if(!LeafSlingerAcquired && EnteredPurplePortal && !HasShownnoWeaponHintYet)
        {
            StartCoroutine(ShowNoWeaponYetHint(noWeaponYet));
            HasShownnoWeaponHintYet = true;
        }

        //Entering Boss area with weapon
        if (LeafSlingerAcquired && EnteredPurplePortal && !HasShownBossWithWeaponHintYet)
        {
            StartCoroutine(ShowBossWithWeaponHint(bossWithWeapon));
            HasShownBossWithWeaponHintYet = true;
        }


    }

    public IEnumerator ShowBossWithWeaponHint(GameObject hint)
    {
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("Boss With Weapon");

        yield return new WaitForSeconds(2);


        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(6);
        hint.GetComponent<SpriteRenderer>().enabled = false;

    }

    public IEnumerator ShowNoWeaponYetHint(GameObject hint)
    {
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("show no weapon yet");

        yield return new WaitForSeconds(2);

        
        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(12);
        hint.GetComponent<SpriteRenderer>().enabled = false;
        
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
            hasShownAreaComplete1Hint = false;
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
            hasShownAreaComplete2Hint = false;
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
            hasShownAreaComplete3Hint = false;
        }
    }

    public IEnumerator ShowShootLeafSlingerHint(GameObject hint)
    {
        Debug.Log("in show leaf function");
        ResetHints();
        //Show hint passed to function for 10 seconds
        Debug.Log("Show hint function");


        yield return new WaitForSeconds(8);

        Debug.Log(HasShotYet);
        if (!HasShotYet)
        {
            Debug.Log("Showing Leaf Hint");
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(12);
            hint.GetComponent<SpriteRenderer>().enabled = false;
            ShownShootLeafSlingerHintYet = false;
        }
    }




    public void ResetHints()
    {
        //Reset hint visibilities
        areaComplete.GetComponent<SpriteRenderer>().enabled = false;
        hasntShotYet.GetComponent<SpriteRenderer>().enabled = false;
        noWeaponYet.GetComponent<SpriteRenderer>().enabled = false;
        bossWithWeapon.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    
}
