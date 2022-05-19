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


    void FixedUpdate()
    {
        isWin = false;
        ProcessInputs();
        playerScore -= 1;
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

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }
}
