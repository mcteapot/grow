using UnityEngine;
using System.Collections;

public struct MinMax<T> {
	
	public T min { get; set; }
	public T max { get; set; }
	
	public MinMax(T minNew, T maxNew){
		min = minNew;
		max = maxNew;
	}

}
