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
        key3 = GameObject.Find("Key3");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            Debug.Log("HIITT");


            if (enemyScript.Health == 1)
            {
                Debug.Log("DEATHHH");
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
        Transform enemyPosition = GameObject.Find("Enemy").transform;
        Debug.Log(enemyPosition.position);
        Destroy(GameObject.Find("Enemy"));
        key3.transform.position = enemyPosition.position;
        key3.tag = "key3";
        key3.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
