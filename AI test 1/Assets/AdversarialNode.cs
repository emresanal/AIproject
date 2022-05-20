using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversarialNode
{
	public List<Action> sequence;
	public double score;
	
	public AdversarialNode(){
		sequence = new List<Action>();
		score = 0.0;
	}
	
	public AdversarialNode(List<Action> lst, double scr){

        sequence = new List<Action>();
        for (int i = 0; i < lst.Count; i++)
        {
            Action temp = new Action();
            temp.direction = lst[i].direction;
            temp.isFire = lst[i].isFire;
            sequence.Add(temp);
            
        }
		
		score = scr;
	}
}