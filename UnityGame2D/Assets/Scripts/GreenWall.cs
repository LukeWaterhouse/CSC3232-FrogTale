using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWall : MonoBehaviour
{




    [SerializeField] public bool isTransparent = false;

    SpriteRenderer sprite;
    BoxCollider2D wallCollider;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<BoxCollider2D>();
        Invoke("ChangeWallStatus", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ChangeWallStatus()
    {

        if(!isTransparent){
           StartCoroutine(changeToTransparent());

        }else{
           StartCoroutine(changeToSolid());
        }
        

    }


    void FlashChange(){
       
    sprite.color = Color.white;
    }


    private IEnumerator changeToTransparent()
        {
             for(var n = 0; n < 13; n++)
        {
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color (1f,1f,1f,.2f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color (1f,1f,1f,1f);
          
        }
         float randomTime = Random.Range(13, 22);
            Debug.Log("change colour");
            sprite.color = new Color (1f,1f,1f,.2f);
            gameObject.layer = 0;
            wallCollider.enabled = false;
            isTransparent = true;
            Invoke("ChangeWallStatus", randomTime);
        }


    private IEnumerator changeToSolid()
        {
             for(var n = 0; n < 13; n++)
        {
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color (1f,1f,1f,.2f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color (1f,1f,1f,1f);
          
        }
         float randomTime = Random.Range(13, 22);
            Debug.Log("change colour");
            sprite.color = new Color (1f,1f,1f,1f);
            gameObject.layer = 12;
            wallCollider.enabled = true;
            isTransparent = false;
            Invoke("ChangeWallStatus", randomTime);
        }
}
