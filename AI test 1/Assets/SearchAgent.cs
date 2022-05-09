using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PlayerMovements;
//using State;
//using PriorityQueue;

public class SearchAgent{
	
	private static int search_method = 0; // 0 - BFS :: 1 - DFS :: 2 - UCS :: 3 - A_star
	
	private static Vector3 up = new Vector3(0f,0.25f,0f);
	private static Vector3 right = new Vector3(0.25f,0f,0f);
	private static Vector3 left = new Vector3(-0.25f,0f,0f);
	private static Vector3 down = new Vector3(0f,-0.25f,0f);
	private static Vector3 zero = new Vector3(0f,0f,0f);
	private static Vector3[] direc = new Vector3[] { up, right, left, down };
	
	/*private class State{
		Vector3 position;
		List<Vector3> path;
		int cost;
	}*/
	
	public List<Vector3> search(Vector3 pos){

        if (search_method == 0)
            return bfs_search(pos);
        else if (search_method == 1)
            return dfs_search(pos);
        else if (search_method == 2)
            return ucs_search(pos);
        else if (search_method == 3)
            return a_star_search(pos);
        else
            return null;
		
		
	}
	
	// BFS search
	public List<Vector3> bfs_search(Vector3 pos){
		
		Queue<State> q = new Queue<State>();
		List<Vector3> visited = new List<Vector3>();

        List<Vector3> v = new List<Vector3>();
		
		if(PlayerMovements.checkEnd(pos)){
			v.Add(Vector3.zero);
			return v;
		}
		
		State cur_state = new State();
		cur_state.pos = pos;
		cur_state.path = new List<Vector3>();
		cur_state.cost = 0;

        //visited.Add(pos)
        Vector3 cur_pos = pos;
		
		while(!PlayerMovements.checkEnd(cur_pos)){
		//while(q.Count != 0){
			
			var legals = PlayerMovements.getLegalActions(cur_pos);
		    
			for(int i = 0; i < 4; i++){
				
				if(!legals[i])
					continue;
				
				if(PlayerMovements.checkEnd(cur_pos)){
                    return cur_state.path;
					//return v;
				}
				if(visited.Contains(cur_pos))
					break;
				visited.Add(cur_pos);
				
				State next_state = new State();
				
				next_state.cost =  cur_state.cost + 1; 
				next_state.act_cost = cur_state.act_cost + 1;
				next_state.path = cur_state.path;
                Vector3 new_pos = cur_state.pos + direc[i];
				next_state.pos = new_pos;
				
				next_state.path.Add(direc[i]);
				q.Enqueue(next_state);
			
			}
			cur_state = q.Dequeue();
			cur_pos = cur_state.pos;
		}
		if(PlayerMovements.checkEnd(cur_pos)){
            return cur_state.path;
					//return v;
				}
		return null;
	}

	
	// DFS search
	public List<Vector3> dfs_search(Vector3 pos){
		
		Stack<State> q = new Stack<State>();
		List<Vector3> visited = new List<Vector3>();

        List<Vector3> v = new List<Vector3>();
		
		if(PlayerMovements.checkEnd(pos)){
			v.Add(Vector3.zero);
			return v;
		}
		
		State cur_state = new State();
		cur_state.pos = pos;
		cur_state.path = new List<Vector3>();
		cur_state.cost = 0;

        //visited.Add(pos)
        Vector3 cur_pos = pos;
		
		while(!PlayerMovements.checkEnd(cur_pos)){
		//while(q.Count != 0){
			
			var legals = PlayerMovements.getLegalActions(cur_pos);
		
			for(int i = 0; i < 4; i++){
				
				if(!legals[i])
					continue;
				
				if(PlayerMovements.checkEnd(cur_pos)){
                    return cur_state.path;
					//return v;
				}
				if(visited.Contains(cur_pos))
					break;
				visited.Add(cur_pos);

                State next_state = new State();
				
				next_state.cost =  cur_state.cost + 1;
				next_state.act_cost = cur_state.act_cost + 1;
				next_state.path = cur_state.path;
                Vector3 new_pos = cur_state.pos + direc[i];
				next_state.pos = new_pos;
				
				next_state.path.Add(direc[i]);
				q.Push(next_state);
			
			}
			cur_state = q.Pop();
			cur_pos = cur_state.pos;
		}
		if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
		}
		if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
				}
		return null;
	}
	
	// UCS Search
	public List<Vector3> ucs_search(Vector3 pos){
		
		PriorityQueue q = new PriorityQueue();
		List<Vector3> visited = new List<Vector3>();

        List<Vector3> v = new List<Vector3>();
		
		if(PlayerMovements.checkEnd(pos)){
			v.Add(Vector3.zero);
			return v;
		}
		
		State cur_state = new State();
		cur_state.pos = pos;
		cur_state.path = new List<Vector3>();
		cur_state.cost = 0;

        //visited.Add(pos)
        Vector3 cur_pos = pos;
		
		while(!PlayerMovements.checkEnd(cur_pos)){
		//while(q.Count != 0){
			
			var legals = PlayerMovements.getLegalActions(cur_pos);
		
			for(int i = 0; i < 4; i++){
				
				if(!legals[i])
					continue;
				
				if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
				}
				if(visited.Contains(cur_pos))
					break;
				visited.Add(cur_pos);

                State next_state = new State();
				
				next_state.cost =  cur_state.cost + 1;
				next_state.act_cost = cur_state.act_cost + 1;
				next_state.path = cur_state.path;
                Vector3 new_pos = cur_state.pos + direc[i];
				next_state.pos = new_pos;
				
				next_state.path.Add(direc[i]);
				q.Enqueue(next_state);
			
			}
			cur_state = q.Dequeue();
			cur_pos = cur_state.pos;
		}
		if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
			}
		return null;
		
	}
	
	
	// A star search
	public List<Vector3> a_star_search(Vector3 pos){
		
		PriorityQueue q = new PriorityQueue();
		List<Vector3> visited = new List<Vector3>();

        List<Vector3> v = new List<Vector3>();
		
		if(PlayerMovements.checkEnd(pos)){
			v.Add(Vector3.zero);
			return v;
		}
		
		State cur_state = new State();
		cur_state.pos = pos;
		cur_state.path = new List<Vector3>();
		cur_state.cost = 0;

        //visited.Add(pos)
        Vector3 cur_pos = pos;
		
		while(!PlayerMovements.checkEnd(cur_pos)){
		//while(q.Count != 0){
			
			var legals = PlayerMovements.getLegalActions(cur_pos);
		
			for(int i = 0; i < 4; i++){
				
				if(!legals[i])
					continue;
				
				if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
				}
				if(visited.Contains(cur_pos))
					break;
				visited.Add(cur_pos);

                State next_state = new State();
				
				next_state.path = cur_state.path;
				Vector3 new_pos = cur_state.pos + direc[i];
				next_state.pos = new_pos;
				
				next_state.cost =  cur_state.act_cost + 1 + PlayerMovements.heuristic(new_pos);
				next_state.act_cost = cur_state.act_cost + 1;
				
				next_state.path.Add(direc[i]);
				q.Enqueue(next_state);
			
			}
			cur_state = q.Dequeue();
			cur_pos = cur_state.pos;
		}
		if(PlayerMovements.checkEnd(cur_pos)){
					return cur_state.path;
					//return v;
				}
		return null;	
	}
}



