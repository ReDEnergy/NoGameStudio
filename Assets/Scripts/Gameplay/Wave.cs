using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
	public LineRenderer graphics;
	public float		moveSpeed;
	public float		timeToLive;
	public float		timeToReady;
	public float		previewAmount;


	[HideInInspector]	public int		colorIndex;

	Vector3 _moveDirection;
	bool    _isReady = false;


	void Start ()
	{
		Color colorStart = GameplayManager.singleton.GetColor( colorIndex ).color;
		Color colorEnd = new Color( colorStart.r, colorStart.g, colorStart.b, 0 );
		graphics.SetColors( colorStart, colorEnd );

		 _moveDirection = transform.rotation * Vector3.left;

		StartCoroutine( _GetReady() );

		Destroy( gameObject, timeToLive );
	}
	

	void Update ()
	{
		if ( _isReady )
		{
			transform.Translate( _moveDirection * moveSpeed * Time.deltaTime, Space.World );
		}
	}


	IEnumerator _GetReady()
	{
		transform.Translate( _moveDirection * previewAmount, Space.World );

		yield return new WaitForSeconds( timeToReady );

		_isReady = true;
	}
}
