using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawnHandler : MonoBehaviour
{


    [SerializeField] GameObject skullPrefab;
    [SerializeField] GameObject Frog;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnSkull", 2.0f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void spawnSkull(){

        Debug.Log("children:");
        Debug.Log(getChildren(gameObject));
        if(getChildren(gameObject)==0){
        Debug.Log("SPAWNING");
        GameObject newSkull = Instantiate(skullPrefab) as GameObject;
        newSkull.transform.position = gameObject.transform.position;
        newSkull.tag = "killObject";
        newSkull.transform.parent = transform;
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }
    public int getChildren(GameObject obj)
{
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
