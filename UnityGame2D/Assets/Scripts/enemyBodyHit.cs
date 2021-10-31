using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class enemyBodyHit : MonoBehaviour
{
    //Body variables
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

    //Hint Handler
    public hintHandler hintHandler;

    void Awake()
    {
        //Finding things
        mainBody = GameObject.Find("enemyAnimator");
        enemyScript = FindObjectOfType<Enemy>();
        key3 = GameObject.Find("Key3");
        bossBounce = GameObject.Find("boingBossArea");
        bossBounce.SetActive(false);
        portalCover = GameObject.Find("PortalCover");
        portalCover.SetActive(false);

        //Find Hinthandler
        hintHandler = FindObjectOfType<hintHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            //Run Death function if health is 0
            if (enemyScript.Health <= 1)
            {
                Death();
            }
            else
            {                
                StartCoroutine(DamageSequence());

                //If below 30 health 1/7 chance to go into angry mode
                if (enemyScript.Health < 30 && !AngryActivated)
                {                      
                    randomNumber = rand.Next(7);

                    if (randomNumber == 1)
                    {
                        AngryMode();
                        AngryActivated = true;

                        //notify hint handler boss is angry
                        hintHandler.bossIsAngry = true;
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
    }
    
    void Death()
    {
        Transform enemyPosition = GameObject.Find("Enemy").transform;
        Destroy(GameObject.Find("Enemy"));
        key3.transform.position = enemyPosition.position;
        key3.tag = "key3";
        key3.GetComponent<Rigidbody2D>().gravityScale = 1;
        bossBounce.SetActive(true);
        portalCover.SetActive(true);
    }

    void AngryMode()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.magenta;
        DefaultColor = Color.magenta;
        enemyScript.moveSpeed *= 2;
        enemyScript.jumpSpeed = 10;
    }
}
