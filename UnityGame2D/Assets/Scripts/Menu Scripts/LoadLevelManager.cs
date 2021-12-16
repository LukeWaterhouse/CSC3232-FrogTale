using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevelManager : MonoBehaviour
{


    GameObject level2Barrier;
    GameObject level3Barrier;

    //Static level bools
    GameObject staticLevelBools;


    void Awake()
    {
        level2Barrier = GameObject.Find("Level2Barrier");
        level3Barrier = GameObject.Find("Level3Barrier");

        //find level bools
        staticLevelBools = GameObject.Find("StaticLevelBools");
    }

    void Update()
    {

        Debug.Log(StaticLevelBools.isLevel2Unlocked);


        //destory level 2 barrier if static bool true
        if (StaticLevelBools.isLevel2Unlocked)
        {
            Destroy(level2Barrier);
        }


        if (StaticLevelBools.isLevel3Unlocked)
        {
            Destroy(level3Barrier);
        }
        
    }


    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);

    }
}
