using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Dot_OTW : Dot 
{
	List<Wave_OTW>  _waves = new List<Wave_OTW>();
	


	override public
	void OnClick()
	{
		if ( _goodWaveCount > 0 )
		{
			for ( int i = 0; i < _waves.Count; ++i )
			{
				Wave_OTW wave = _waves[i];

				for ( int j = 0; j < wave.dots.Count; ++j )
				{
					if ( wave.dots[j] != this )
					{
						wave.dots[j].OnLostWave( wave );
					}
				}

				Destroy( wave.gameObject );
			}
			_waves.Clear();

			_GetDestroyed();
		}
		else
		{
			GameplayManager.singleton.DotMissed();
		}
    }


	override protected
	void OnTriggerEnter2D( Collider2D other )
	{
		if ( other.tag == "Wave" )
		{
			Wave_OTW wave = other.GetComponent<Wave_OTW>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount++;
				_waves.Add( wave );
				wave.dots.Add( this );
			}
		}
    }


	override protected
	void OnTriggerExit2D( Collider2D other )
	{
		if ( other.tag == "Wave" )
		{
			Wave_OTW wave = other.GetComponent<Wave_OTW>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount--;
				_waves.Remove( wave );
				wave.dots.Remove( this );
			}
		}
    }


	public
	void OnLostWave( Wave_OTW wave )
	{
		_goodWaveCount--;
		_waves.Remove( wave );
	}
}
