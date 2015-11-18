using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class ColorData
{
	public int      colorIndex;
	public Color	color;
	public int      dotsLeft;
}



public class GameManager : MonoBehaviour
{
	static public bool gameON = true;

	public List<ColorData>	colors;


	public void OnDotDestroyed( int colorIndex )
	{
		ColorData data = GetColor( colorIndex );
		data.dotsLeft--;

		if ( data.dotsLeft <= 0 )
		{
			colors.Remove( data );

			if ( colors.Count == 0 )
			{
				gameON = false;
			}
		}
	}


	public ColorData GetColor( int colorIndex )
	{
		foreach ( ColorData data in colors )
		{
			if ( data.colorIndex == colorIndex )
			{
				return data;
			}
		}

		return null;
	}
}
