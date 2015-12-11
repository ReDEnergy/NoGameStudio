using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour
{
	public SpriteRenderer	sprite;
	public Transform        explosion;
	public GameObject       highlight;

	public int				colorIndex;


	int         _goodWaveCount = 0;


	void Start()
	{
		sprite.color = GameManager.singleton.GetColor( colorIndex ).color;
	}
	

	void Update()
	{
		highlight.SetActive( _goodWaveCount > 0 );
	}


	void OnMouseDown()
	{
		if ( _goodWaveCount > 0 )
		{
			_GetDestroyed();
		}
		else
		{
			GameManager.singleton.DotMissed();
		}
    }


	void _GetDestroyed()
	{
		Instantiate( explosion, transform.position, Quaternion.identity );

		GameManager.singleton.OnDotDestroyed( colorIndex );

		Destroy( gameObject );
	}


	void OnTriggerEnter2D( Collider2D other )
	{
		if ( other.tag == "Wave" )
		{
			Wave wave = other.GetComponent<Wave>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount++;
			}
		}
    }

	void OnTriggerExit2D( Collider2D other )
	{
		if ( other.tag == "Wave" )
		{
			Wave wave = other.GetComponent<Wave>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount--;
			}
		}
    }
}
