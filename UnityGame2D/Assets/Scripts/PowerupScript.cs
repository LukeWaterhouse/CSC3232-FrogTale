using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            StartCoroutine(ResetPowerup());
        }
    }

    private IEnumerator ResetPowerup()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(true);

    }

    private void Timer()
    {

    }
}



