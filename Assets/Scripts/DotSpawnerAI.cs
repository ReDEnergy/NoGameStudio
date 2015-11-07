using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class DotSpawnerAI : MonoBehaviour {
	public GameObject dotPrefab;

	private int H, W, h ,w, noOfSlots,n,m;
	private List<int> available = new List<int>();
	public int MAXDOTS = 6;
	public int ID = 0;
	public float spawnTime = 3.0f;

	private List<GameObject> dots = new List<GameObject>();

	// Use this for initialization
	void Start () {
		RectTransform rectSpawner = transform.GetComponent (typeof (RectTransform)) as RectTransform;
		RectTransform rectDot = dotPrefab.GetComponent (typeof (RectTransform)) as RectTransform;

		H = Math.Abs((int)rectSpawner.rect.height);
		W = Math.Abs((int)rectSpawner.rect.width);
		h = Math.Abs((int)rectDot.rect.height);
		w = Math.Abs((int)rectDot.rect.width);

		//Debug.Log (H + " " + W + " " + " " + h + " " + w);
		n = (W/w);
		m= (H/h);

		noOfSlots =  n*m;
		Debug.Log ("MAX NUMBERS OF SLOTS = "+ n + " * " + m);
		Debug.Log ("MAX NUMBERS OF DOTS = "+ MAXDOTS);


		for (int i = 0; i < noOfSlots; i++) {
			available.Add (i);
			//Debug.Log(GetSlot(i));
		}

		StartCoroutine (Spawn (spawnTime));
	}

	private Vector2 GetSlot(int i) {
		return  new Vector2 ((i / n) * (h+10) + 10, (i % m) * (w+10) + 10)- new Vector2(H/4, W/4);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			if (dots.Count < MAXDOTS)
				dots.Add(RandomDot());
		}
	}

	public void Remove(GameObject dot) {
		dots.Remove (dot);
		available.Add (dot.GetComponent<DotAI> ().slot);
		//Debug.Log (dots.Count + " dots remaining");
	}

	IEnumerator Spawn(float time) {
		yield return new WaitForSeconds (time);

		if (dots.Count < MAXDOTS)
			dots.Add(RandomDot());
		StartCoroutine (Spawn (time));
	}


	private GameObject RandomDot() {
		++ID;

		available.Sort ();
		int index = RandomMachine.NextInt (available.Count);
		int slot = available[index];
		available.RemoveAt (index);

		Color color = RandomMachine.RandomColor ();

		Vector2 pos = GetSlot(slot);

//		Debug.Log ("New dot " + ID + "#" + color);

		return GetDot(ID, slot,color, pos);
	}

	private GameObject GetDot(int ID, int slot,  Color color,Vector2 position) {
		GameObject dot = (GameObject) Instantiate(dotPrefab);
		dot.transform.parent = transform;

		dot.transform.localPosition = position;
		dot.transform.localScale = new Vector3(0.75f, 0.75f,0.75f);

		RectTransform rectDot = dot.GetComponent<RectTransform> ();
		rectDot.sizeDelta = new Vector2 (150, 150);
		//rectDot.offsetMin = new Vector2(rectDot.offsetMin.x, position.x);
		//rectDot.offsetMax = new Vector2(rectDot.offsetMax.x, position.y);

		dot.GetComponent<DotAI>().Init(ID, slot, color);
		return dot;
	}
}