using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    private float random;

    private static Vector3 up = new Vector3(0f, 0.25f, 0f);
    private static Vector3 right = new Vector3(0.25f, 0f, 0f);
    private static Vector3 left = new Vector3(-0.25f, 0f, 0f);
    private static Vector3 down = new Vector3(0f, -0.25f, 0f);

    public Transform playerFirePoint;

    public GameObject player;

    private bool fireCheck;

    private Shooting shooting;

    public Script1 playerScript;

    // Start is called before the first frame update
    void Start()
    {
        fireCheck = true;
        shooting = player.GetComponent<Shooting>();

        playerScript = player.GetComponent<Script1>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        random = Random.Range(0, 10.0f);
        if (random < 2.5f)
        {
            playerScript.Move(up);
        }
        else if (random > 2.5f && random <= 5f)
        {
            playerScript.Move(down);
        }
        else if (random > 5f && random <= 7.5f)
        {
            playerScript.Move(left);
        }
        else
        {
            playerScript.Move(right);
        }

        if (CheckSightline())
        {
            //playerScript.Fire();
            int a =2 + 2;
        }

    }

    bool CheckSightline()
    {
        if (Physics2D.Raycast(playerFirePoint.position, playerFirePoint.up).collider.tag == "Player")
        {
            //Debug.Log("enemy sighted");
            return true;
        }

        return false;
    }
}
