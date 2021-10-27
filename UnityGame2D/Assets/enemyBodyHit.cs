using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBodyHit : MonoBehaviour
{
    // Start is called before the first frame updat

    GameObject mainBody;
    [SerializeField] private float dmgAnimationDuration = 0.1f;

    void Awake()
    {
        mainBody = GameObject.Find("enemyAnimator");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("HIITT");
            StartCoroutine(DamageSequence());


        }
    }


    IEnumerator DamageSequence()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
