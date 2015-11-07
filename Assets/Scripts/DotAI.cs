using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DotAI : MonoBehaviour {
	public int ID  = 0; 
	public int slot = 0;
	public Color color = Color.NONE;
	public Image image;

	public static Dictionary<Color, UnityEngine.Color> hash = new Dictionary<Color, UnityEngine.Color>() { 
		{Color.WHITE, UnityEngine.Color.white},
		{Color.RED, UnityEngine.Color.red},
		{Color.GREEN, UnityEngine.Color.green},
		{Color.BLUE, UnityEngine.Color.blue},
		{Color.BLACK, UnityEngine.Color.black}
	};

	public void OnMouseClick() {
		Debug.Log ( ID + "#" + color + " will be destroyed");
		transform.parent.GetComponent<DotSpawnerAI> ().Remove (gameObject);
		Destroy (gameObject);
	}

	public void Init(int ID, int slot, Color color) {
		this.ID = ID;
		this.color = color;
		this.slot = slot;
		image.color = hash [color];
	}

}
