using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class skullScript : MonoBehaviour
{

    [SerializeField] public float skullHealth = 6;
    [SerializeField] private float dmgAnimationDuration = 0.1f;

    GameObject parentObject;
    GameObject playerObject;

    AudioManager audioManager;

    ParticleSystem skullExplosion;

    GameObject mainBody;
    void Start()
    {

        //Getting components and objects        
        playerObject = GameObject.Find("Frog");
        mainBody = this.gameObject.transform.GetChild(0).gameObject;
        gameObject.GetComponent<AIDestinationSetter>().target = playerObject.transform;
        audioManager = FindObjectOfType<AudioManager>();
        skullExplosion = this.GetComponentInChildren<ParticleSystem>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //reduce skull health
            skullHealth -= 1;

            if (skullHealth <= 0)
            {
                StartCoroutine(DeathSequence());
            }
            else
            {
                StartCoroutine(DamageSequence());
            }
        }
    }

    IEnumerator DamageSequence()
    {
        //Flash the skull red
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = Color.white;
    }


    public IEnumerator DeathSequence()
    {
        //play explosion and death noise, disbale collider
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        audioManager.Play("SkullExplode");
        audioManager.Play("SkullDeath");
        skullExplosion.Play();

        //wait 2 seconds then destroy (to allow particle effect to finish)
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }



}
