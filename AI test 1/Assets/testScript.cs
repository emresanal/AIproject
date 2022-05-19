using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{

    public GameObject player;
    private PlayerMovement movements;

    // Start is called before the first frame update
    void Start()
    {
        movements = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(movements.health);
        Debug.Log(player.transform.position);
    }
}
