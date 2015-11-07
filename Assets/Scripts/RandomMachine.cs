using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RandomMachine : MonoBehaviour {
	private static System.Random colorGenerator = new System.Random();
	private static System.Random generator = new System.Random();
	private static Dictionary<int, Color> hash = new  Dictionary<int, Color>(){
		{0, Color.BLUE},
		{1, Color.GREEN},
		{2, Color.RED},
		{3, Color.WHITE},
		{4, Color.BLACK},
	}; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Color RandomColor(){
		return hash[colorGenerator.Next (hash.Count)];
	}

	public static int NextInt(int n){
		return generator.Next (n);
	}

}
