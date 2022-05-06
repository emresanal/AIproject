using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int health;
    public float wallDetectionRange;

    public Transform RayLF;
    public Transform RayRF;
    public Transform RayLR;
    public Transform RayRR;
    public Transform firePoint;


    // Update is called once per frame
    void Update()
    {
        // Processing Inputs
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            ProcessInputs();
        } 
    }
    void FixedUpdate()
    {

        //Debug.Log(CheckWalls().ToString());
        //Debug.Log(CheckSightline());
        // Physics Calculations
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Move();
        }
        else
        {
            Stop();
        }
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        //transform.Rotate(0, 0, angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Stop()
    {
        float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        //transform.Rotate(0, 0, angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = new Vector2(0,0);
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

    bool[] CheckWalls()
    {
        bool[] legalDirections = new bool[] { true, true, true, true };

        if ((Physics2D.Raycast(RayLF.position, Vector3.up, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLF.position, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position, Vector3.up, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRF.position, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position, Vector3.up, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLR.position, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position, Vector3.up, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRR.position, Vector3.up, wallDetectionRange).collider.tag == "Tilemap"))
        {
            Debug.Log("wall up");
            legalDirections[0] = false;
        }
        if ((Physics2D.Raycast(RayLF.position, Vector3.right, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLF.position, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position, Vector3.right, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRF.position, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position, Vector3.right, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLR.position, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position, Vector3.right, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRR.position, Vector3.right, wallDetectionRange).collider.tag == "Tilemap"))
        {
            Debug.Log("wall right");
            legalDirections[1] = false;
        }
        if ((Physics2D.Raycast(RayLF.position, Vector3.left, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLF.position, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position, Vector3.left, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRF.position, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position, Vector3.left, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLR.position, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position, Vector3.left, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRR.position, Vector3.left, wallDetectionRange).collider.tag == "Tilemap"))
        {
            Debug.Log("wall left");
            legalDirections[2] = false;
        }
        if ((Physics2D.Raycast(RayLF.position, Vector3.down, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLF.position, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position, Vector3.down, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRF.position, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position, Vector3.down, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayLR.position, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position, Vector3.down, wallDetectionRange).collider != null &&
            Physics2D.Raycast(RayRR.position, Vector3.down, wallDetectionRange).collider.tag == "Tilemap"))
        {
            Debug.Log("wall down");
            legalDirections[3] = false;
        }

        return legalDirections;
    }

    bool CheckSightline()
    {
        if (Physics2D.Raycast(firePoint.position, firePoint.up).collider.tag == "Player")
        {
            Debug.Log("enemy sighted");
            return true;
        }

        return false;
    }

}
