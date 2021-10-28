using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBodyHit : MonoBehaviour
{
    // Start is called before the first frame updat

    GameObject mainBody;
    [SerializeField] private float dmgAnimationDuration = 0.1f;
    public GameObject key3;

    public Enemy enemyScript;

    void Awake()
    {
        mainBody = GameObject.Find("enemyAnimator");
        enemyScript = FindObjectOfType<Enemy>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            Debug.Log("HIITT");


            if (enemyScript.Health == 0)
            {
                Debug.Log("DEATHHH");
                Destroy(GameObject.Find("Enemy"));
                Death();
            }
            else
            {
                StartCoroutine(DamageSequence());

            }


        }


    }


    IEnumerator DamageSequence()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = Color.white;
        enemyScript.Health -= 1;
        Debug.Log(enemyScript.Health);
    }
    
    void Death()
    {
        Debug.Log("DEATHHH");
        Transform enemyPosition = gameObject.transform;
        Debug.Log(enemyPosition);
        Destroy(GameObject.Find("Enemy"));
        GameObject key3Object = Instantiate(key3, enemyPosition);
        key3Object.tag = "key3";
    }
}
