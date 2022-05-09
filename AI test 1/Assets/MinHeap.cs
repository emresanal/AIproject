//using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap
        {
            private State[] _elements;
            private int _size;
			private int max_size;
            public MinHeap(int size)
            {
                _elements = new State[size];
				max_size = size;
            }

            private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
            private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
            private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

            private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
            private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
            private bool IsRoot(int elementIndex) => elementIndex == 0;

            private State GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
            private State GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
            private State GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

            private void Swap(int firstIndex, int secondIndex)
            {
                var temp = _elements[firstIndex];
                _elements[firstIndex] = _elements[secondIndex];
                _elements[secondIndex] = temp;
            }

            public bool IsEmpty()
            {
                return _size == 0;
            }
			
			public int getSize(){
				return _size;
			}

            public State Peek()
            {
                if (_size == 0)
                    return null;

                return _elements[0];
            }

            public State Pop()
            {
                if (_size == 0)
                    return null;

                var result = _elements[0];
                _elements[0] = _elements[_size - 1];
                _size--;

                ReCalculateDown();

                return result;
            }

            public void Add(State element)
            {
                if (_size == _elements.Length)
                    resize();

                _elements[_size] = element;
                _size++;

                ReCalculateUp();
            }

            private void ReCalculateDown()
            {
                int index = 0;
                while (HasLeftChild(index))
                {
                    var smallerIndex = GetLeftChildIndex(index);
                    if (HasRightChild(index) && GetRightChild(index).cost < GetLeftChild(index).cost)
                    {
                        smallerIndex = GetRightChildIndex(index);
                    }

                    if (_elements[smallerIndex].cost >= _elements[index].cost)
                    {
                        break;
                    }

                    Swap(smallerIndex, index);
                    index = smallerIndex;
                }
            }

            private void ReCalculateUp()
            {
                var index = _size - 1;
                while (!IsRoot(index) && _elements[index].cost < GetParent(index).cost)
                {
                    var parentIndex = GetParentIndex(index);
                    Swap(parentIndex, index);
                    index = parentIndex;
                }
            }
			private void resize(){
				int new_size = max_size *2;
                State[] temp = new State[new_size];
				for(int i = 0; i < max_size;i++){
					temp[i] = _elements[i];
				}
				_elements = temp;
				max_size = new_size;
			}
        }