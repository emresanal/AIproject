using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversarialNode: MonoBehaviour
{
	public List<Action> sequence;
	public double score;
	
	public AdversarialNode(){
		sequence = new List<Action>();
		score = 0;
	}
	
	public AdversarialNode(List<Action> lst, double scr){
		sequence = lst;
		score = scr;
	}
}