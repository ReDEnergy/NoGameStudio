using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour
{
	public SpriteRenderer	sprite;
	public Transform        explosion;

	public Color			myColor;


	void Start()
	{
		sprite.color = myColor;
	}
	
	void OnMouseDown()
	{
		_GetDestroyed();
    }


	void _GetDestroyed()
	{
		Instantiate( explosion, transform.position, Quaternion.identity );

		Destroy( gameObject );
	}
}
