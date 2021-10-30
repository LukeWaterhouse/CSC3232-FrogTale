using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class enemyBodyHit : MonoBehaviour
{
    // Start is called before the first frame updat

    GameObject mainBody;
    [SerializeField] private float dmgAnimationDuration = 0.1f;
    public GameObject key3;
    public GameObject bossBounce;
    public Enemy enemyScript;
    public Color DefaultColor = Color.white;

    public bool AngryActivated = false;

    public GameObject portalCover;

    Random rand = new Random();
    int randomNumber;

    void Awake()
    {
        mainBody = GameObject.Find("enemyAnimator");
        enemyScript = FindObjectOfType<Enemy>();
        key3 = GameObject.Find("Key3");
        bossBounce = GameObject.Find("boingBossArea");
        bossBounce.SetActive(false);

        portalCover = GameObject.Find("PortalCover");
        portalCover.SetActive(false);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("HIITT");
            if (enemyScript.Health <= 1)
            {
                Debug.Log("DEATHHH");
                Death();
            }
            else
            {
                StartCoroutine(DamageSequence());
                if (enemyScript.Health < 30 && !AngryActivated)
                {                      
                    randomNumber = rand.Next(7);
                    Debug.Log("Random number:" + randomNumber);

                    if (randomNumber == 1)
                    {
                        AngryMode();
                        AngryActivated = true;
                    }                   
                }
            }
        }
    }

    IEnumerator DamageSequence()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = DefaultColor;
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
        bossBounce.SetActive(true);
        portalCover.SetActive(true);
    }


    void AngryMode()
    {

        Debug.Log("ANGRYMODE");

        mainBody.GetComponent<SpriteRenderer>().color = Color.magenta;
        DefaultColor = Color.magenta;

        enemyScript.moveSpeed *= 2;
        enemyScript.jumpSpeed = 10;

    }
}
