using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour
{
    public enum SoundEffect
    {
        POP,
        KICK,
        HIHAT,
        SNARE
    }

    public SpriteRenderer	sprite;
	public Transform        explosion;
	public GameObject       highlight;
	public int				colorIndex;
    public SoundEffect      soundEffect;


    protected int   _goodWaveCount = 0;


    void Start()
    {
        sprite.color = GameplayManager.singleton.GetColor(colorIndex).color;
    }
	

	void Update()
	{
		highlight.SetActive( _goodWaveCount > 0 );
	}


	virtual public
	void OnClick()
	{
        if ( _goodWaveCount > 0 )
		{
			_GetDestroyed();
		}
		else
		{
			GameplayManager.singleton.DotMissed();
		}
    }


	protected
	void _GetDestroyed()
	{
		Transform expl = Instantiate( explosion, transform.position, Quaternion.identity ) as Transform;
        expl.GetComponent<AudioSource>().clip = expl.GetComponent<ExplosionDot>().audioClips[(int)soundEffect];
        expl.GetComponent<AudioSource>().Play();

        GameplayManager.singleton.OnDotDestroyed( colorIndex );

		Destroy( gameObject );
	}


	virtual protected
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


	virtual protected
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
