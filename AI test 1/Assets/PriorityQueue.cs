using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using State;
//using MinHeap;

public class PriorityQueue{
	private MinHeap heap;
	public int Count;

	public PriorityQueue(){
		heap = new MinHeap(32);
        Count = heap.getSize();
	}
	
	public void Enqueue(State s){
		heap.Add(s);
        Count = heap.getSize();
    }
	
	public State Dequeue(){
		return heap.Pop();
        Count = heap.getSize();
    }
		
}
