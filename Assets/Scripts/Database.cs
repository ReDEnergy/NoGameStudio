using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class LevelData
{
	public bool isUnlocked;
	public int	starsCount;
	public int	bestCombo;

	public
	LevelData( bool unlocked )
	{
		isUnlocked = unlocked;
	}
}


public class Database
{
	static bool _isInitialized = false;

	static public int currLvIndex = 2;

	static public bool gameON = true;

	static public List<LevelData> levels = new List<LevelData>();



	static public
	void Initialize()
	{
		if ( !_isInitialized )
		{
			levels.Add( new LevelData( true ) );
			levels.Add( new LevelData( true ) );
			levels.Add( new LevelData( true ) );
			levels.Add( new LevelData( true ) );
		}
	}

}
