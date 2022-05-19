using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeta: MonoBehaviour
{
	private static Vector3 up = new Vector3(0f, 0.25f, 0f);
    private static Vector3 right = new Vector3(0.25f, 0f, 0f);
    private static Vector3 left = new Vector3(-0.25f, 0f, 0f);
    private static Vector3 down = new Vector3(0f, -0.25f, 0f);
    private static Vector3 zero = new Vector3(0f, 0f, 0f);
    private static Vector3[] direc = new Vector3[] { up, right, left, down };
	
	private static int max_depth = 4;
	
	public AdversarialNode select_max(List<AdversarialNode> evals){
		AdversarialNode temp = new AdversarialNode();
		temp.score = -7.997e307 + -9.985e307;
        foreach (AdversarialNode node in evals) {
            if (node.score >= temp.score)
            {
                temp = node;
            }
        }
        return temp;
    }
	
	public AdversarialNode select_min(List<AdversarialNode> evals){
		AdversarialNode temp = new AdversarialNode();
		temp.score = 200000000;
		foreach(AdversarialNode node in evals){
            if (node.score <= temp.score)
                temp = node;
		}
		return temp;
	}
	
	public AdversarialNode maximize(GameState gamestate, int cur_depth, AdversarialNode cur_node, int object_no, double alpha, double beta){
		
		if( gamestate.gameWin || cur_depth >= max_depth){
			cur_node.score += evaluationFunction(gamestate);// BU LAZIM
			return cur_node;
		}
			
		
		//List<AdversarialNode> evals = new List<AdversarialNode>();
		
		// 0 : ID, gamestate: gameState
		bool[] legalActions = getLegalActions(0, gamestate); // BU LAZIM
		
		double cur_max = -7.997e307 + -9.985e307;
		
		AdversarialNode return_node = new AdversarialNode();
        return_node.sequence = cur_node.sequence;
        return_node.score = cur_node.score;
        AdversarialNode temp_node = new AdversarialNode();

        for ( int i = 0; i < 8; i++){
			
			if(!legalActions[i])
				continue;
			
			bool isFire = (i >= 4) ? true : false;
			
			Action new_action =  new Action(direc[i%4],isFire);

            // 0: ID, new_action: action
            GameState new_gamestate = gamestate.generateSuccessor(0, new_action); // BU LAZIM
			
			AdversarialNode new_node = new AdversarialNode(cur_node.sequence,cur_node.score -1.0);
			new_node.sequence.Add(new_action);
			
			// object_no = 1
			temp_node = minimize(new_gamestate,cur_depth,new_node,1,alpha,beta);
			
			cur_max = (temp_node.score > cur_max) ? temp_node.score : cur_max;
			
			if(cur_max > beta){
				return_node.sequence = cur_node.sequence;
				return_node.score = cur_max;
				return return_node;
			}
            if (cur_max > alpha) {
                alpha = cur_max;
                return_node.sequence = temp_node.sequence;
            }
            //evals.Add();	
            


        }
        if(cur_max != -7.997e307 + -9.985e307)
            return_node.score = cur_max;
        return return_node;

    }
	
	public AdversarialNode minimize(GameState gamestate, int cur_depth, AdversarialNode cur_node, int object_no,double alpha,double beta){
		
		if( gamestate.gameWin || cur_depth >= max_depth){
			cur_node.score += evaluationFunction(gamestate);// BU LAZIM
			return cur_node;
		}
		
		double cur_min = 200000000;
		
		AdversarialNode return_node = new AdversarialNode();
        return_node.sequence = cur_node.sequence;
        return_node.score = cur_node.score;
        AdversarialNode temp_node = new AdversarialNode();
        //List<AdversarialNode> evals = new List<AdversarialNode>();

        // object_no : ID, gamestate: gameState
        bool[] legalActions = getLegalActions(object_no, gamestate); // BU LAZIM
		
		for( int i = 0; i < 8; i++){
			
			if(!legalActions[i])
				continue;
			
			bool isFire = (i >= 4) ? true : false;
			
			Action new_action =  new Action(direc[i%4],isFire);
    
            // object_no: ID, new_action: action
            GameState new_gamestate = gamestate.generateSuccessor(object_no, new_action); // BU LAZIM
			
			AdversarialNode new_node = new AdversarialNode(cur_node.sequence,cur_node.score -1.0);
			new_node.sequence.Add(new_action);

            temp_node = maximize(new_gamestate, cur_depth + 1, new_node, 0, alpha, beta);
			
			cur_min = (temp_node.score <= cur_min) ? temp_node.score : cur_min;
			
			if(cur_min < alpha){
				return_node.sequence = cur_node.sequence;
				return_node.score = cur_min;
				return return_node;
			}

            if (cur_min <= beta) {
                beta = cur_min;
                return_node.sequence = temp_node.sequence;
            } 
			
		}
        if (cur_min != 200000000)
            return_node.score = cur_min;

        return return_node;
	}

    double evaluationFunction(GameState gamestate)
    {
        double finalscore = 0;
        finalscore += (double)(gamestate.playerScore);

        if (gamestate.playerHealth > gamestate.enemyHealth)
        {
            finalscore += 1;
        }

        finalscore += 5 / (double)ManhattanDistancetoObject(gamestate.playerPos, gamestate.enemyPos);

        if (ManhattanDistancetoObject(gamestate.playerPos, gamestate.enemyPos) < 4)
        {
            finalscore -= 1;
        }

        if (gamestate.playerActions[4] || gamestate.playerActions[5] || gamestate.playerActions[6] || gamestate.playerActions[7])
        {
            finalscore += 5;
        }

        return finalscore;
    }

    float ManhattanDistancetoObject(Vector3 first, Vector3 second)
    {
        float distance = Mathf.Abs(first.x - second.x) + Mathf.Abs(first.y - second.y);
        return distance;
    }

    bool[] getLegalActions(int ID, GameState gamestate)
    {
        if (ID == 0)
        {
            return gamestate.playerActions;
        }

        else
        {
            return gamestate.enemyActions;
        }

    }

}