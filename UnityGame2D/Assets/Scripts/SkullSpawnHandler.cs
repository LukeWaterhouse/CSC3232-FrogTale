using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawnHandler : MonoBehaviour
{


    [SerializeField] GameObject skullPrefab;
    [SerializeField] GameObject Frog;

    void Start()
    {
        InvokeRepeating("spawnSkull", 2.0f, 4f);
    }

    public void spawnSkull()
    {

        //if skull is dead spawn a new skull at spawn
        if (getChildren(gameObject) == 0)
        {
            GameObject newSkull = Instantiate(skullPrefab) as GameObject;
            newSkull.transform.position = gameObject.transform.position;
            newSkull.tag = "killObject";
            newSkull.transform.parent = transform;
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }
    public int getChildren(GameObject obj)
    {
        //return child count of a game object
        int count = 0;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            count++;
            counter(obj.transform.GetChild(i).gameObject, ref count);
        }


        return count;
    }


    private void counter(GameObject currentObj, ref int count)
    {
        for (int i = 0; i < currentObj.transform.childCount; i++)
        {
            count++;
            counter(currentObj.transform.GetChild(i).gameObject, ref count);
        }
    }
}
