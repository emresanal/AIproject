using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeta : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Vector3 playerPos;
    public float playerHealth;
    public Vector3 enemyPos;
    public float enemyHealth;

    private static Vector3 up = new Vector3(0f, 0.25f, 0f);
    private static Vector3 right = new Vector3(0.25f, 0f, 0f);
    private static Vector3 left = new Vector3(-0.25f, 0f, 0f);
    private static Vector3 down = new Vector3(0f, -0.25f, 0f);
    private static Vector3 zero = new Vector3(0f, 0f, 0f);
    private static Vector3 up2 = new Vector3(0f, 0.5f, 0f);
    private static Vector3 right2 = new Vector3(0.5f, 0f, 0f);
    private static Vector3 left2 = new Vector3(-0.5f, 0f, 0f);
    private static Vector3 down2 = new Vector3(0f, -0.5f, 0f);
    private static Vector3[] direc = new Vector3[] { up, right, left, down };

    public Script1 playerScript;
    public Script1 enemyScript;

    private static int max_depth = 2;
    public List<Action> sequence = new List<Action>();
    public int frame_cnt = 0;
    public int lst_cnt = 0;
    //public GameState gstate = new GameState(player, enemy);

    public bool[] playerLegalActions = new bool[] { true, true, true, true, false, false, false, false };
    public bool[] enemyLegalActions = new bool[] { true, true, true, true, false, false, false, false };
    public float moveSpeed;
    public float wallDetectionRange;
    public GameObject endPoint;

    public Transform playerRayLF;
    public Transform playerRayRF;
    public Transform playerRayLR;
    public Transform playerRayRR;
    public Transform playercenter;
    public Transform playerFirePoint;

    public Transform enemyRayLF;
    public Transform enemyRayRF;
    public Transform enemyRayLR;
    public Transform enemyRayRR;
    public Transform enemycenter;
    public Transform enemyFirePoint;

    private bool fireCheck;

    private Shooting shooting;

    void Start()
    {
        fireCheck = true;
        shooting = player.GetComponent<Shooting>();

        playerScript = player.GetComponent<Script1>();
        enemyScript = enemy.GetComponent<Script1>();
    }

    void FixedUpdate()
    {
        //if (frame_cnt >= 2)
        //return;
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;

        //GameState gstate = new GameState(player, enemy);//---------------------------------------------- gamestate var
        if (frame_cnt % max_depth == 0)
        {
            Debug.Log("Cagirdi");
            AdversarialNode input = new AdversarialNode();

            AdversarialNode temp = maximize(playerPos, enemyPos, 0, input, 0, -2000000, 20000000);//---------------------------------------------- gamestate var

            sequence = temp.sequence;
            lst_cnt = 0;
        }
        if (lst_cnt >= sequence.Count)
        {
            lst_cnt++;
            frame_cnt++;
            return;
        }


        Action act = sequence[lst_cnt];

        if (CheckSightline())
        {
            playerScript.Fire(act.direction);
        }
        else
        {
            playerScript.Move(act.direction);
        }

        lst_cnt++;
        frame_cnt++;


    }

    public AdversarialNode select_max(List<AdversarialNode> evals)
    {
        AdversarialNode temp = new AdversarialNode();
        temp.score = -200000000;
        foreach (AdversarialNode node in evals)
        {
            if (node.score >= temp.score)
            {
                temp = node;
            }
        }
        return temp;
    }

    public AdversarialNode select_min(List<AdversarialNode> evals)
    {
        AdversarialNode temp = new AdversarialNode();
        temp.score = 200000000;
        foreach (AdversarialNode node in evals)
        {
            if (node.score <= temp.score)
                temp = node;
        }
        return temp;
    }

    public AdversarialNode maximize(Vector3 cur_pos, Vector3 enemy_pos, int cur_depth, AdversarialNode cur_node, int object_no, double alpha, double beta)
    {

        if (cur_depth >= max_depth)
        {
            cur_node.score += evaluationFunction(cur_pos, enemy_pos);// BU LAZIM
            return cur_node;
        }


        //List<AdversarialNode> evals = new List<AdversarialNode>();

        // 0 : ID, gamestate: gameState
        bool[] legalActions = getLegalActions(0, cur_pos, enemy_pos); // BU LAZIM

        double cur_max = -200000001;

        AdversarialNode return_node = new AdversarialNode();
        return_node.sequence = cur_node.sequence;
        return_node.score = cur_node.score;
        AdversarialNode temp_node = new AdversarialNode();

        for (int i = 0; i < 8; i++)
        {

            if (!legalActions[i])
                continue;

            bool isFire = (i >= 4) ? true : false;

            Action new_action = new Action(direc[i % 4], isFire);
            Vector3 new_pos = cur_pos + new_action.direction;
            // 0: ID, new_action: action
            //GameState new_gamestate = gamestate.generateSuccessor(0, new_action); // BU LAZIM

            AdversarialNode new_node = new AdversarialNode(cur_node.sequence, cur_node.score);
            new_node.sequence.Add(new_action);

            // object_no = 1
            temp_node = minimize(new_pos, enemy_pos, cur_depth, new_node, 1, alpha, beta);

            cur_max = (temp_node.score > cur_max) ? temp_node.score : cur_max;

            if (cur_max > beta)
            {
                return_node.sequence = cur_node.sequence;
                return_node.score = cur_max;
                return return_node;
            }
            if (cur_max > alpha)
            {
                alpha = cur_max;
                return_node.sequence = temp_node.sequence;
            }
            //evals.Add();	



        }
        if (cur_max != -200000001)
            return_node.score = cur_max;
        return return_node;

    }

    public AdversarialNode minimize(Vector3 cur_pos, Vector3 enemy_pos, int cur_depth, AdversarialNode cur_node, int object_no, double alpha, double beta)
    {

        if (cur_depth >= max_depth)
        {
            cur_node.score += evaluationFunction(cur_pos, enemy_pos);// BU LAZIM
            return cur_node;
        }

        double cur_min = 200000001;

        AdversarialNode return_node = new AdversarialNode();
        return_node.sequence = cur_node.sequence;
        return_node.score = cur_node.score;
        AdversarialNode temp_node = new AdversarialNode();
        //List<AdversarialNode> evals = new List<AdversarialNode>();

        // object_no : ID, gamestate: gameState
        bool[] legalActions = getLegalActions(object_no, enemy_pos, cur_pos); // BU LAZIM

        for (int i = 0; i < 8; i++)
        {

            if (!legalActions[i])
                continue;

            bool isFire = (i >= 4) ? true : false;

            Action new_action = new Action(direc[i % 4], isFire);

            // object_no: ID, new_action: action
            //GameState new_gamestate = gamestate.generateSuccessor(object_no, new_action); // BU LAZIM
            Vector3 new_pos = cur_pos + new_action.direction;
            AdversarialNode new_node = new AdversarialNode(cur_node.sequence, cur_node.score - 1.0);
            new_node.sequence.Add(new_action);

            temp_node = maximize(cur_pos, new_pos, cur_depth + 1, new_node, 0, alpha, beta);

            cur_min = (temp_node.score <= cur_min) ? temp_node.score : cur_min;

            if (cur_min < alpha)
            {
                return_node.sequence = cur_node.sequence;
                return_node.score = cur_min;
                return return_node;
            }

            if (cur_min <= beta)
            {
                beta = cur_min;
                return_node.sequence = temp_node.sequence;
            }

        }
        if (cur_min != 200000001)
            return_node.score = cur_min;

        return return_node;
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

    double evaluationFunction(Vector3 cur_pos, Vector3 enemy_pos)
    {
        enemy_pos = enemy.transform.position;
        Debug.Log("cur_pos " + cur_pos + "  enemy_pos " + enemy_pos);
        double finalscore = 0;
        //finalscore += (double)(gamestate.playerScore);

        //if (gamestate.playerHealth > gamestate.enemyHealth)
        //{
        ///   finalscore += 1;
        //}

        finalscore += (double)(10000 / (ManhattanDistancetoObject(cur_pos, enemy_pos)));
        //Debug.Log("manhattan score = " + 10000 / (ManhattanDistancetoObject(cur_pos, enemy_pos)));

        /**if (gamestate.playerActions[4] || gamestate.playerActions[5] || gamestate.playerActions[6] || gamestate.playerActions[7])
        {
            finalscore += 5;
        }**/

        return finalscore;
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

    public bool[] getLegalActions(int ID, Vector3 pos, Vector3 enemy_pos)
    {
        fireCheck = shooting.fireLegal;
        Vector3 dif = new Vector3(pos.x - playercenter.position.x, pos.y - playercenter.position.y, 0f);
        bool[] legalActions = new bool[] { true, true, true, true, false, false, false, false };

        if ((Physics2D.Raycast(playerRayLF.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLF.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRF.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRF.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayLR.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLR.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRR.position + dif, Vector3.up, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRR.position + dif, Vector3.up, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log(RayLF.position);
            //Debug.Log(center.position + dif);
            //Debug.Log(dif);
            //Debug.Log("wall up");
            legalActions[0] = false;
        }
        if ((Physics2D.Raycast(playerRayLF.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLF.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRF.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRF.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayLR.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLR.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRR.position + dif, Vector3.right, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRR.position + dif, Vector3.right, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall right");
            legalActions[1] = false;
        }
        if ((Physics2D.Raycast(playerRayLF.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLF.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRF.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRF.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayLR.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLR.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRR.position + dif, Vector3.left, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRR.position + dif, Vector3.left, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall left");
            legalActions[2] = false;
        }
        if ((Physics2D.Raycast(playerRayLF.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLF.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRF.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRF.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayLR.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayLR.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap") ||
            (Physics2D.Raycast(playerRayRR.position + dif, Vector3.down, wallDetectionRange).collider != null &&
             Physics2D.Raycast(playerRayRR.position + dif, Vector3.down, wallDetectionRange).collider.tag == "Tilemap"))
        {
            //Debug.Log("wall down");
            legalActions[3] = false;
        }
        /*
                if (Physics2D.Raycast(center.position + dif + up2, up).collider.tag != null && 
                    Physics2D.Raycast(center.position + dif + up2, up).collider.tag == "Player" &&            
                    fireCheck)
                {
                    //Debug.Log("enemy sightline up");
                    legalActions[4] = true;
                }

                if (Physics2D.Raycast(center.position + dif + right2, right).collider.tag != null &&
                    Physics2D.Raycast(center.position + dif + right2, right).collider.tag == "Player" &&
                    fireCheck)
                {
                    //Debug.Log("enemy sightline right");
                    legalActions[5] = true;
                }

                if (Physics2D.Raycast(center.position + dif + left2, left).collider.tag != null &&
                    Physics2D.Raycast(center.position + dif + left2, left).collider.tag == "Player" &&
                    fireCheck)
                {
                    //Debug.Log("enemy sightline left");
                    legalActions[6] = true;
                }

                if (Physics2D.Raycast(center.position + dif + down2, down).collider.tag != null &&
                    Physics2D.Raycast(center.position + dif + down2, down).collider.tag == "Player" &&
                    fireCheck)
                {
                    //Debug.Log("enemy sightline down");
                    legalActions[7] = true;
                }*/

        return legalActions;
    }

}