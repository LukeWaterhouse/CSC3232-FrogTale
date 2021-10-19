using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            this.enabled = false;
            StartCoroutine(ResetPowerup());
        }
    }

    private IEnumerator ResetPowerup()
    {
        yield return new WaitForSeconds(3);
        this.enabled = true;

    }

    private void Timer()
    {

    }
}



