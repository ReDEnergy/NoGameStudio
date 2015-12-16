using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
	public LineRenderer graphics;

	[HideInInspector]	public int		colorIndex;
	[HideInInspector]	public float	moveSpeed;


	Vector3 _moveDirection;


	void Start ()
	{
		Color colorStart = GameplayManager.singleton.GetColor( colorIndex ).color;
		Color colorEnd = new Color( colorStart.r, colorStart.g, colorStart.b, 0 );
		graphics.SetColors( colorStart, colorEnd );

		 _moveDirection = transform.rotation * Vector3.left;
	}
	

	void Update ()
	{
		transform.Translate( _moveDirection * moveSpeed * Time.deltaTime, Space.World );
	}
}
