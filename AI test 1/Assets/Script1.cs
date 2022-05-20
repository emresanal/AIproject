using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script1 : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public int health;
    public bool isWin;
    public float playerScore;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float cooldownTime;
    private float nextFireTime;
    public bool fireLegal;


    void FixedUpdate()
    {
        isWin = false;
        //playerScore -= 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Bullet(Clone)")
        {
            health = health - 1;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
        float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += direction;
    }

    public void Fire(Vector3 direction)
    {
        fireLegal = Time.time > nextFireTime;
        if (fireLegal)
        {
            moveDirection = direction;
            float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Shoot();
            nextFireTime = Time.time + cooldownTime;
        }
        
    }

    public void Fire()
    {
        fireLegal = Time.time > nextFireTime;
        if (fireLegal)
        {
            Shoot();
            nextFireTime = Time.time + cooldownTime;
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
