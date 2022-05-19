using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float cooldownTime;
    private float nextFireTime;
    public bool fireLegal;

    // Update is called once per frame
    void Update()
    {
        Fire();
    }


    void Fire()
    {
        fireLegal = Time.time > nextFireTime;
        if (fireLegal)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
