using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;

    // Update is called once per frame
    void Update()
    {
        Fire();
    }


    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        /*GameObject bullet1 = Instantiate(bulletPrefab, RayLF.position, RayLF.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, RayRF.position, RayRF.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab, RayLR.position, RayLR.rotation);
        GameObject bullet4 = Instantiate(bulletPrefab, RayRR.position, RayRR.rotation);*/
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        /*Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();*/
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        /*rb1.AddForce((RayLF.right*-1) * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(RayRF.right * bulletForce, ForceMode2D.Impulse);
        rb3.AddForce(RayLR.up * bulletForce, ForceMode2D.Impulse);
        rb4.AddForce(RayRR.up * bulletForce, ForceMode2D.Impulse);*/
    }
}
