using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class skullScript : MonoBehaviour
{


    [SerializeField] public float skullHealth = 10;
    [SerializeField] private float dmgAnimationDuration = 0.1f;

    GameObject parentObject;
    GameObject playerObject;

    // Start is called before the first frame update

    GameObject mainBody;
    void Start()
    {
      
        playerObject = GameObject.Find("Frog");
        mainBody = this.gameObject.transform.GetChild(0).gameObject;
        gameObject.GetComponent<AIDestinationSetter>().target = playerObject.transform;

        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bullet"){
            skullHealth -=1;


            if(skullHealth <= 0){
                Destroy(gameObject);
            }
            StartCoroutine(DamageSequence());

            Debug.Log(skullHealth);

        }
    }

     IEnumerator DamageSequence()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = Color.white;
    }


    
}
