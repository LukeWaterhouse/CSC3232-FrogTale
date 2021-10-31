using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] public Transform firePoint;
    public GameObject leafProjectile;
    public float projectileForce = 20f;

    public hintHandler hintHandler;

    void Awake()
    {
        hintHandler = FindObjectOfType<hintHandler>();
    }

    void Update()
    {       
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            hintHandler.HasShotYet = true;
        }        
    }

    void Fire()
    {
        GameObject projectile = Instantiate(leafProjectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
    }
}
