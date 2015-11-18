using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour
{
	public SpriteRenderer	sprite;
	public Transform        explosion;
	public GameObject       highlight;

	public int				colorIndex;


	GameManager _GM;
	int         _goodWaveCount = 0;


	void Start()
	{
		_GM = FindObjectOfType<GameManager>();
		sprite.color = _GM.GetColor( colorIndex ).color;
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
    }


	void _GetDestroyed()
	{
		Instantiate( explosion, transform.position, Quaternion.identity );

		_GM.OnDotDestroyed( colorIndex );

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
