using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int health;
    public float wallDetectionRange;
    public GameObject otherPlayer;

    public Transform RayLF;
    public Transform RayRF;
    public Transform RayLR;
    public Transform RayRR;
    public Transform firePoint;

    private bool[] legalDirections = new bool[] { true, true, true, true };
    private bool enemy = false;


    // Update is called once per frame
    void Update()
    {
        // Processing Inputs
        //if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
            ProcessInputs();
        //}
    }
    void FixedUpdate()
    {
        ManhattanDistancetoObject(otherPlayer);
        Debug.Log(SearchEnd());
        legalDirections = CheckWalls();
        //Debug.Log(legalDirections.ToString());
        enemy = CheckSightline();
        //Debug.Log(enemy);
        // Physics Calculations
        //if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
            //Move();
        //}
        //else
        //{
            //Stop();
        //}

    }

    void ProcessInputs()
    {
        moveDirection = new Vector2(1, 0).normalized;

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
        rb.velocity = new Vector2(0, 0);
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
            //Debug.Log("wall up");
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
            //Debug.Log("wall right");
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
            //Debug.Log("wall left");
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
            //Debug.Log("wall down");
            legalDirections[3] = false;
        }

        return legalDirections;
    }

    bool CheckSightline()
    {
        if (Physics2D.Raycast(firePoint.position, firePoint.up).collider.tag == "Player")
        {
            //Debug.Log("enemy sighted");
            return true;
        }

        return false;
    }

    float DistancetoObject(GameObject other)
    {
        if (other != null)
        {
            float distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
            //Debug.Log(distance);
            return distance;
        }

        return -1;
    }

    float ManhattanDistancetoObject(GameObject other)
    {
        if (other != null)
        {
            float distance = Mathf.Abs(gameObject.transform.position.x - other.transform.position.x) + Mathf.Abs(gameObject.transform.position.y - other.transform.position.y);
            //Debug.Log(distance);
            return distance;
        }

        return -1;
    }

    bool SearchEnd()
    {
        if (ManhattanDistancetoObject(otherPlayer) < 1 || otherPlayer == null)
        {
            return true;
        }

        return false;
    }

    /** for next node we can do delta time * movement speed * direction + current position to check next position given that direction is legal
     *  might need to be reworked for a new grid system
     *  idk 
     **/
}
