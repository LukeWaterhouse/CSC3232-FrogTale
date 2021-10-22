using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform firePoint;
    public GameObject leafProjectile;
    public float projectileForce = 20f;

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        
    }

    void Fire()
    {
        GameObject projectile = Instantiate(leafProjectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
    }
}
