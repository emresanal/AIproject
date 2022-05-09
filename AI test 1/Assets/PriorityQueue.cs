using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using State;
//using MinHeap;

public class PriorityQueue{
	private MinHeap heap;
	
	public PriorityQueue(){
		heap = new MinHeap(32);
	}
	
	public void Enqueue(State s){
		heap.Add(s);
	}
	
	public State Dequeue(){
		return heap.Pop();
	}
		
}
