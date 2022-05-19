using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float cost;

    private Vector3 playerPos;
    private float playerHealth;
    private Vector3 enemyPos;
    private float enemyHealth;
    public bool gameWin;
    private Vector2 playerDirection;
    private Vector2 enemyDirection;
    private bool[] playerActions = new bool[] { true, true, true, true, false, false, false, false };
    private bool[] enemyActions = new bool[] { true, true, true, true, false, false, false, false };
    private float playerScore;
    private float enemyScore;

    private Script1 playerScript;
    private Script1 enemyScript;

    private LegalActions playerLegal;
    private LegalActions enemyLegal;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Script1>();
        enemyScript = enemy.GetComponent<Script1>();

        playerLegal = player.GetComponent<LegalActions>();
        enemyLegal = enemy.GetComponent< LegalActions>();

        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;

        playerHealth = playerScript.health;
        enemyHealth = enemyScript.health;

        playerDirection = playerScript.moveDirection;
        enemyDirection = enemyScript.moveDirection;

        playerActions = playerLegal.legalActions;
        enemyActions = enemyLegal.legalActions;

        playerScore = playerScript.playerScore;
        enemyScore = playerScript.playerScore;

        gameWin = playerScript.isWin;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        Debug.Log("Player Position: " + playerPos + " Enemy Position: " + enemyPos);

        playerHealth = playerScript.health;
        enemyHealth = enemyScript.health;
        Debug.Log("Player Health: " + playerHealth + " Enemy Health: " + enemyHealth);

        playerDirection = playerScript.moveDirection;
        enemyDirection = enemyScript.moveDirection;
        Debug.Log("Player Direction: " + playerDirection + " Enemy Direction: " + enemyDirection);

        playerActions = playerLegal.legalActions;
        enemyActions = enemyLegal.legalActions;

        playerScore = playerScript.playerScore;
        enemyScore = playerScript.playerScore;
        Debug.Log("Player Score: " + playerScore + " Enemy Score: " + enemyScore);

        gameWin = playerScript.isWin;
        Debug.Log("Win Status: " + gameWin);
    }

    /**public GameState getSuccessorState(Action action)
    {
        if (action.isFire)
        { }
        else
        { }
    }**/
}
