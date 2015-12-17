using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Dot_OneWave : Dot 
{
	List<Wave>  _wavesToDestroy = new List<Wave>();
	


	override public
	void OnClick()
	{
		if ( _goodWaveCount > 0 )
		{
			// bug here when a wave touches multiple dots
			foreach ( Wave wave in _wavesToDestroy )
			{
				Destroy( wave.gameObject );
			}
			_wavesToDestroy.Clear();

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
			Wave wave = other.GetComponent<Wave>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount++;
				_wavesToDestroy.Add( wave );
			}
		}
    }


	override protected
	void OnTriggerExit2D( Collider2D other )
	{
		if ( other.tag == "Wave" )
		{
			Wave wave = other.GetComponent<Wave>();

			if ( wave.colorIndex == colorIndex )
			{
				_goodWaveCount--;
				_wavesToDestroy.Remove( wave );
			}
		}
    }
}
