using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegalActions : MonoBehaviour
{
    public GameObject player;
    public bool[] legalActions = new bool[] { true, true, true, true, false, false, false, false };
    public float moveSpeed;
    public Rigidbody2D rb;
    public float wallDetectionRange;
    public GameObject endPoint;

    public Transform RayLF;
    public Transform RayRF;
    public Transform RayLR;
    public Transform RayRR;
    public Transform center;

    private bool fireCheck;

    private Shooting shooting;

    private static Vector3 up = new Vector3(0f, 0.25f, 0f);
    private static Vector3 right = new Vector3(0.25f, 0f, 0f);
    private static Vector3 left = new Vector3(-0.25f, 0f, 0f);
    private static Vector3 down = new Vector3(0f, -0.25f, 0f);

    private static Vector3 up2 = new Vector3(0f, 0.5f, 0f);
    private static Vector3 right2 = new Vector3(0.5f, 0f, 0f);
    private static Vector3 left2 = new Vector3(-0.5f, 0f, 0f);
    private static Vector3 down2 = new Vector3(0f, -0.5f, 0f);

    private static Vector3 zero = new Vector3(0f, 0f, 0f);

    private static Vector3[] direc = new Vector3[] { up, right, left, down };

    // Start is called before the first frame update
    void Start()
    {
        fireCheck = true;
        shooting = player.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkEnd(center.position))
            return;

        legalActions = getLegalActions(center.position);
        Debug.Log(legalActions[7]);
    }

    bool[] getLegalActions(Vector3 pos)
    {
        fireCheck = shooting.fireLegal;
        Vector3 dif = new Vector3(pos.x - center.position.x, pos.y - center.position.y, 0f);
        bool[] legalActions = new bool[] { true, true, true, true, false, false, false, false};

        if ((Physics2D.Raycast(RayLF.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLF.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRF.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLR.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRR.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log(RayLF.position);
            //Debug.Log(center.position);
            //Debug.Log(dif);
            //Debug.Log("wall up");
            legalActions[0] = false;
        }
        if ((Physics2D.Raycast(RayLF.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLF.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRF.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLR.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRR.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall right");
            legalActions[1] = false;
        }
        if ((Physics2D.Raycast(RayLF.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLF.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRF.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLR.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRR.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall left");
            legalActions[2] = false;
        }
        if ((Physics2D.Raycast(RayLF.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLF.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRF.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRF.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayLR.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayLR.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(RayRR.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(RayRR.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall down");
            legalActions[3] = false;
        }

        if (Physics2D.Raycast(center.position + dif + up2, up).collider.tag == "Player" &&
            Physics2D.Raycast(center.position + dif + up2, up).collider.tag != null &&
           fireCheck)
        {
            //Debug.Log("enemy sightline up");
            legalActions[4] = true;
        }

        if(Physics2D.Raycast(center.position + dif + right2, right).collider.tag == "Player" &&
           Physics2D.Raycast(center.position + dif + right2, right).collider.tag != null &&
           fireCheck)
        {
            //Debug.Log("enemy sightline right");
            legalActions[5] = true;
        }

        if(Physics2D.Raycast(center.position + dif + left2, left).collider.tag == "Player" &&
           Physics2D.Raycast(center.position + dif + left2, left).collider.tag != null &&
           fireCheck)
        {
            //Debug.Log("enemy sightline left");
            legalActions[6] = true;
        }

        if(Physics2D.Raycast(center.position + dif + down2, down).collider.tag == "Player" && 
           Physics2D.Raycast(center.position + dif + down2, down).collider.tag != null &&
           fireCheck)
        {
            //Debug.Log("enemy sightline down");
            legalActions[7] = true;
        }

        return legalActions;
    }

    float ManhattanDistancetoObject(Vector3 first, Vector3 second)
    {
        float distance = Mathf.Abs(first.x - second.x) + Mathf.Abs(first.y - second.y);
        return distance;
    }

    bool checkEnd(Vector3 pos)
    {
        //enemy null kontrolu ile end bakalim burada

        if (ManhattanDistancetoObject(pos, endPoint.transform.position) < 0.25)
        {
            return true;
        }

        return false;
    }
}
