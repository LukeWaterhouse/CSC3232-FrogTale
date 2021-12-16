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

    // Start is called before the first frame update

    GameObject mainBody;
    void Start()
    {
      

        
        playerObject = GameObject.Find("Frog");
        mainBody = this.gameObject.transform.GetChild(0).gameObject;
        gameObject.GetComponent<AIDestinationSetter>().target = playerObject.transform;
        audioManager = FindObjectOfType<AudioManager>();

        skullExplosion = this.GetComponentInChildren<ParticleSystem>();

        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bullet"){
            skullHealth -=1;
             


            if(skullHealth <= 0){
            StartCoroutine(DeathSequence());
            }else{

                StartCoroutine(DamageSequence());

            }


            

            Debug.Log(skullHealth);

        }
    }

     IEnumerator DamageSequence()
    {
        mainBody.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(dmgAnimationDuration);
        mainBody.GetComponent<SpriteRenderer>().color = Color.white;
    }


     public IEnumerator DeathSequence()
        {
            
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            audioManager.Play("SkullExplode");
            audioManager.Play("SkullDeath");
            
            playExplosion();


            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }


    public void playExplosion(){

        if(skullExplosion==null){
            Debug.Log("particle null");
            Debug.Log(skullExplosion);
        }
        Debug.Log("EXPLODEEEEEEEEEEEEEEEEEEEE!");


        Debug.Log("name: " + skullExplosion.name);
        Debug.Log("isPlaying Before?" + skullExplosion.isPlaying);
        Debug.Log("isPaused Before?" + skullExplosion.isPaused);
        Debug.Log("isStopped Before?" + skullExplosion.isStopped);
        
        if(skullExplosion.isPlaying) skullExplosion.Stop();
        if(!skullExplosion.isPlaying) skullExplosion.Play();

        Debug.Log("isPlaying After?" + skullExplosion.isPlaying);
        Debug.Log("isPaused After?" + skullExplosion.isPaused);
        Debug.Log("isStopped After?" + skullExplosion.isStopped);
    }


    
}
