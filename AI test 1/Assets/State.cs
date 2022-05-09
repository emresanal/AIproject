using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
	public Vector3 pos;
	public List<Vector3> path;
	public int cost;	
	public int act_cost;
	
	public State(){
		//pos = new Vector3;
		path = new List<Vector3>();
		cost = 0;
		act_cost = 0;
	}
}