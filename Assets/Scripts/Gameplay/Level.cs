using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class ColorData
{
	public int      colorIndex;
	public Color	color;
}


public class Level : MonoBehaviour
{
	public List<ColorData>	colors;
	public Wave             wavePrefab;
	public string           name;
	
}
