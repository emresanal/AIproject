using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax: MonoBehaviour{

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
	
	public AdversarialNode maximize(GameState gamestate, int cur_depth, AdversarialNode cur_node, int object_no){
		
		if( gamestate.gameWin || cur_depth >= max_depth){
			cur_node.score += evaluationFunction(gamestate);// BU LAZIM
			return cur_node;
		}
			
		
		List<AdversarialNode> evals = new List<AdversarialNode>();
		
		// 0 : ID, gamestate: gameState
		bool[] legalActions = getLegalActions(0, gamestate); // BU LAZIM
		
		for( int i = 0; i < 8; i++){
			
			if(!legalActions[i])
				continue;
			
			bool isFire = (i >= 4) ? true : false;
			
			Action new_action =  new Action(direc[i%4],isFire);

            // 0: ID, new_action: action
            GameState new_gamestate = gamestate.generateSuccessor(0, new_action); // BU LAZIM
			
			AdversarialNode new_node = new AdversarialNode(cur_node.sequence,cur_node.score -1.0);
			new_node.sequence.Add(new_action);
			
			// object_no = 1
			evals.Add(minimize(new_gamestate,cur_depth,new_node,1));	
			
		}	
		return select_max(evals);
	}
	
	public AdversarialNode minimize(GameState gamestate, int cur_depth, AdversarialNode cur_node, int object_no){
		
		if( gamestate.gameWin || cur_depth >= max_depth){
			cur_node.score += evaluationFunction(gamestate);// BU LAZIM
			return cur_node;
		}
		
		List<AdversarialNode> evals = new List<AdversarialNode>();
		
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
			
			
			evals.Add(maximize(new_gamestate,cur_depth + 1,new_node,0));	
			
		}	
		return select_min(evals);
	}

    double evaluationFunction(GameState gamestate)
    {
        double finalscore = 0;
        finalscore += (double)(gamestate.playerscore);

        if (gamestate.playerHealth > gamestate.enemyHealth)
        {
            finalscore += 1;
        }

        if ()

        return finalscore;
    }
}