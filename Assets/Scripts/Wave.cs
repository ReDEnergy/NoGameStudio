using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
	const float MAX_LIFE_TIME = 5;

	public LineRenderer graphics;

	[HideInInspector]	public int		colorIndex;
	[HideInInspector]	public Vector3	moveDirection;
	[HideInInspector]	public float	moveSpeed;


	void Start ()
	{
		Color color = GameManager.singleton.GetColor( colorIndex ).color;
		graphics.SetColors( color, color );

		Destroy( gameObject, MAX_LIFE_TIME );
	}
	

	void Update ()
	{
		transform.Translate( moveDirection * moveSpeed * Time.deltaTime, Space.World );
	}
}
