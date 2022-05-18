using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
	public Vector3 direction;
	public boolean isFire;
	
	public Action(Vector3 direc, boolean fire){
		direction = direc;
		fire = fire;
	}
	
	public Action(){
		direction = new Vector3;
		isFire = false;
	}
}