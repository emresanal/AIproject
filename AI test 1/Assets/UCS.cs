using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCS : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int health;
    public float wallDetectionRange;
    public GameObject endPoint;

    public Transform RayLF;
    public Transform RayRF;
    public Transform RayLR;
    public Transform RayRR;
    public Transform firePoint;
    public Transform center;

    private bool[] legalDirections = new bool[] { true, true, true, true };
    private bool enemy = false;

    private static Vector3 up = new Vector3(0f, 0.25f, 0f);
    private static Vector3 right = new Vector3(0.25f, 0f, 0f);
    private static Vector3 left = new Vector3(-0.25f, 0f, 0f);
    private static Vector3 down = new Vector3(0f, -0.25f, 0f);
    private static Vector3 zero = new Vector3(0f, 0f, 0f);
    private static Vector3[] direc = new Vector3[] { up, right, left, down };

    private List<Vector3> path; //= new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        var temp = ucs_search(center.position);
        path = temp;
    }

    int count = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkEnd(center.position))
            return;

        if (count >= path.Count)
        {
            return;
        }

        //Debug.Log(count);
        Move(path[count]);
        count++;
    }

    void ProcessInputs(Vector3 direction)
    {
        moveDirection = direction;

    }

    void Move(Vector3 direction)
    {
        //Debug.Log(direction);
        transform.position += direction;
        float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public List<Vector3> ucs_search(Vector3 pos)
    {

        PriorityQueue q = new PriorityQueue();
        //List<Vector3> visited = new List<Vector3>();
        //List<State> visited = new List<State>();
        List<Vector3> visited = new List<Vector3>();
        List<Vector3> v = new List<Vector3>();

        if (checkEnd(pos))
        {
            v.Add(Vector3.zero);
            return v;
        }

        State cur_state = new State();
        cur_state.pos = pos;
        cur_state.path = new List<Vector3>();
        cur_state.cost = 0;
        cur_state.act_cost = 0;

        //visited.Add(pos)
        Vector3 cur_pos = pos;
        q.Enqueue(cur_state);

        while (q.Count != 0)
        {
            //Debug.Log("bas");
            cur_state = q.Dequeue();
            cur_pos = cur_state.pos;

            //while(q.Count != 0){
            bool flag = false;
            for (int i = 0; i < visited.Count; i++)
            {
                if (visited[i].Equals(cur_pos))
                {
                    flag = true;
                    break;
                }
            }
            /*if (visited.Contains(cur_pos))
            {
                //Debug.Log("found");
                //Debug.Log(cur_pos);
                if (visited.Contains(new Vector3(-4.0f, 1.0f,0.0f)))
                    //Debug.Log("YEEEEAAA");
                continue;
            }*/
            if (flag)
                continue;


            visited.Add(cur_pos);

            //Debug.Log(cur_pos);
            var legals = getLegalActions(cur_pos);

            for (int i = 0; i < 4; i++)
            {

                if (!legals[i])
                    continue;

                if (checkEnd(cur_pos))
                {
                    Debug.Log("Exit1");
                    Debug.Log(cur_pos);
                    Debug.Log(cur_state.path.Count);
                    Debug.Log(cur_state.cost);
                    //for (int j =0; j<cur_state.path.Count;j++)
                    //   Debug.Log(cur_state.path[j]);

                    return cur_state.path;
                    //return v;
                }



                State next_state = new State();

                next_state.cost = cur_state.act_cost + 1;
                next_state.act_cost = cur_state.act_cost + 1;
                for (int j = 0; j < cur_state.path.Count; j++)
                {
                    next_state.path.Add(cur_state.path[j]);
                }
                //next_state.path = cur_state.path;
                //Debug.Log(cur_pos);
                Vector3 new_pos = cur_pos + direc[i];
                next_state.pos = new_pos;



                next_state.path.Add(direc[i]);
                //Debug.Log("Enqued");
                //Debug.Log(next_state.pos);
                //Debug.Log(next_state.cost);
                //Debug.Log(next_state.path.Count);
                q.Enqueue(next_state);

            }
            //if (q.Count == 0)
            //    return cur_state.path;
            //cur_state = q.Dequeue();
            //Debug.Log("Dequed");
            //Debug.Log(cur_state.pos);
            //cur_pos = cur_state.pos;
        }
        if (checkEnd(cur_pos))
        {
            Debug.Log("Exit2");
            return cur_state.path;
            //return v;
        }
        return null;
    }

    bool[] getLegalActions(Vector3 inp)
    {
        Vector3 dif = new Vector3(inp.x - center.position.x, inp.y - center.position.y, 0f);
        bool[] legalDirections = new bool[] { true, true, true, true };

        //Debug.Log("Pseudo Grid location = " + Mathf.Floor(center.position.x * 4) / 4 + "," + Mathf.Floor(center.position.y * 4) / 4);
        //Debug.Log(Mathf.Floor(center.position.y * 4) / 4);

        Debug.Log(dif);

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
            Debug.Log("wall up");
            legalDirections[0] = false;
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
            Debug.Log("wall right");
            legalDirections[1] = false;
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
            Debug.Log("wall left");
            legalDirections[2] = false;
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
            Debug.Log("wall down");
            legalDirections[3] = false;
        }

        return legalDirections;
    }


    float ManhattanDistancetoObject(Vector3 first, Vector3 second)
    {
        float distance = Mathf.Abs(first.x - second.x) + Mathf.Abs(first.y - second.y);
        return distance;
    }

    bool checkEnd(Vector3 pos)
    {
        if (ManhattanDistancetoObject(pos, endPoint.transform.position) < 0.25)
        {
            return true;
        }

        return false;
    }


}
