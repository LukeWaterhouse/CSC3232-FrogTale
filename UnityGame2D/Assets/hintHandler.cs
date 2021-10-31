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
    public GameObject thisIsHeavy; //done
    public GameObject bossIsAngryHint; //done

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

    //Leafslinger picked up/has the player shot yet?
    public bool LeafSlingerAcquired = false;
    public bool HasShotYet = false;
    public bool ShownShootLeafSlingerHintYet = false;

    //EnteredPurplePortal with or without weapon?
    public bool EnteredPurplePortal = false;
    public bool HasShownnoWeaponHintYet = false;
    public bool HasShownBossWithWeaponHintYet = false;

    //Ice Hint bools
    public bool HitStoneWall = false;
    public bool HasIcePowerup = false;
    public bool HasShownIceHintYet = false;

    //Angry Boss bools
    public bool bossIsAngry = false;
    public bool hasShownAngryHint = false;




    // Start is called before the first frame update
    void Awake()
    {
        druidControl = FindObjectOfType<DruidControl>();

        //Find hint objects
        areaComplete = GameObject.Find("AreaComplete"); //done
        hasntShotYet = GameObject.Find("ShotLeafYet"); // done
        noWeaponYet = GameObject.Find("noWeapon"); // done
        bossWithWeapon = GameObject.Find("bossWithWeapon"); //done
        thisIsHeavy = GameObject.Find("ThisIsHeavy"); //done
        bossIsAngryHint = GameObject.Find("AngryBossMessage"); //done
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

        //Hit StoneWallWithoutIcePowerup
        if (HitStoneWall && !HasIcePowerup && !HasShownIceHintYet)
        {
            StartCoroutine(ShowBlockTooHeavyHint(thisIsHeavy));
            HasShownIceHintYet = true;

        }

        //Boss becomes Angry
        if (bossIsAngry && !hasShownAngryHint)
        {
            Debug.Log("ANGRY HINT");
            StartCoroutine(ShowAngryBossHint(bossIsAngryHint));
            hasShownAngryHint = true;
        }

        


    }

    //Boss is angry method:

    public IEnumerator ShowAngryBossHint(GameObject hint)
    {
        ResetHints();
        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(5);
        hint.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Hit stone wall without ice powerup

    public IEnumerator ShowBlockTooHeavyHint(GameObject hint)
    {
        ResetHints();
        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(10);
        hint.GetComponent<SpriteRenderer>().enabled = false;
    }


    //Entering Boss area with weapon Method:

    public IEnumerator ShowBossWithWeaponHint(GameObject hint)
    {
        ResetHints();      
        yield return new WaitForSeconds(2);
        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(6);
        hint.GetComponent<SpriteRenderer>().enabled = false;
    }


    //Entering Boss area without a weapon Method:

    public IEnumerator ShowNoWeaponYetHint(GameObject hint)
    {
        ResetHints();    
        yield return new WaitForSeconds(2);       
        hint.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(12);
        hint.GetComponent<SpriteRenderer>().enabled = false;      
    }



    //Has LeafSlinger but isn't shooting it for a while Method:

    public IEnumerator ShowShootLeafSlingerHint(GameObject hint)
    {        
        ResetHints();       
        yield return new WaitForSeconds(4);
        if (!HasShotYet)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(10);
            hint.GetComponent<SpriteRenderer>().enabled = false;
            //Break out of loop
            ShownShootLeafSlingerHintYet = false;
        }
    }


    //Area Complete Hint Methods: 

    public IEnumerator ShowAreaComplete1Hint(GameObject hint)
    {
        ResetHints();
        yield return new WaitForSeconds(15);
        if (!Checkpoint1Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
            //Break out of loop
            hasShownAreaComplete1Hint = false;
        }
    }

    public IEnumerator ShowAreaComplete2Hint(GameObject hint)
    {
        ResetHints();
        yield return new WaitForSeconds(15);
        if (!Checkpoint2Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
            //Break out of loop
            hasShownAreaComplete2Hint = false;
        }
    }

    public IEnumerator ShowAreaComplete3Hint(GameObject hint)
    {
        ResetHints();
        yield return new WaitForSeconds(10);
        if (!Checkpoint3Reached)
        {
            hint.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(6);
            hint.GetComponent<SpriteRenderer>().enabled = false;
            //Break out of loop
            hasShownAreaComplete3Hint = false;
        }
    }

    


    //Reset hint visibility Method 

    public void ResetHints()
    {
        areaComplete.GetComponent<SpriteRenderer>().enabled = false;
        hasntShotYet.GetComponent<SpriteRenderer>().enabled = false;
        noWeaponYet.GetComponent<SpriteRenderer>().enabled = false;
        bossWithWeapon.GetComponent<SpriteRenderer>().enabled = false;
        thisIsHeavy.GetComponent<SpriteRenderer>().enabled = false;
        bossIsAngryHint.GetComponent<SpriteRenderer>().enabled = false;
}

    // Update is called once per frame
    
}
