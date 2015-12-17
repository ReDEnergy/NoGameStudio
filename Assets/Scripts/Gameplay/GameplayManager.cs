using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[System.Serializable]
public class DEBUG
{
	public Text		missesText;
}



public class GameplayManager : MonoBehaviour
{
	public GameObject       lvDonePanel;
	public GameObject[]     stars;
	public Text             maxComboText;
	public float[]          starMissCritaria;  // maximum (misses / total dots) ratio per star accepted
	public Level[]			levels;
		
	public DEBUG            __debug;

	int     _missesCount = 0;
	int     _totalDots = 0;
	int     _starsReceived;

	Dictionary<int, int>    _colorsLeft = new Dictionary<int, int>();

	public Level currLevel { get; private set; }


	static public GameplayManager singleton { get; private set; }

	void Awake()
	{
		singleton = this;

		Database.Initialize();	

		Database.gameON = true;

		currLevel = Instantiate( levels[ Database.currLvIndex ] );

		foreach ( ColorData data in currLevel.colors )
		{
			_colorsLeft.Add( data.colorIndex, 0 );
		}

		foreach ( Transform child in currLevel.transform )
		{
			_colorsLeft[child.GetComponent<Dot>().colorIndex] ++;
			_totalDots ++;
		}
	}


	void OnDestroy()
	{
		singleton = null;
	}


	void Update()
	{
		if ( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

			RaycastHit2D hit = Physics2D.Raycast( ray.origin, ray.direction, 
												  Mathf.Infinity, LayerMask.GetMask("Dots") );

			if ( hit.collider != null )
			{
				hit.collider.GetComponent<Dot>().OnClick();
			}
		}
	}


	public 
	void OnDotDestroyed( int colorIndex )
	{
		ColorData data = GetColor( colorIndex );
		_colorsLeft[colorIndex]--;

		ScoreManager.singleton.AddCombo();

		if ( _colorsLeft[colorIndex] <= 0 )
		{
			currLevel.colors.Remove( data );

			if ( currLevel.colors.Count == 0 )
			{
				Database.gameON = false;
				_LevelComplete();
			}
		}
	}


	public 
	ColorData GetColor( int colorIndex )
	{
		foreach ( ColorData data in currLevel.colors )
		{
			if ( data.colorIndex == colorIndex )
			{
				return data;
			}
		}

		return null;
	}

	public 
	void DotMissed()
	{
		_missesCount++;
		__debug.missesText.text = "misses: " + _missesCount;
	}


	void _LevelComplete()
	{
		lvDonePanel.SetActive( true );
		maxComboText.text = "Combo x" + ScoreManager.singleton.GetBestCombo();

		float missRatio = (float) _missesCount / _totalDots;

		_starsReceived = 0;
		for ( int i = 0; i < 3; ++i )
		{
			if ( missRatio <= starMissCritaria[i] )
			{
				_starsReceived++;
			}

			stars[i].SetActive( (missRatio <= starMissCritaria[i]) );
		}

		{
			LevelData lvData = Database.levels[ Database.currLvIndex ];
			lvData.starsCount = _starsReceived;
			lvData.bestCombo = ScoreManager.singleton.GetBestCombo();
		}

		if ( Database.currLvIndex < Database.levels.Count - 1 )
		{
			LevelData lvData = Database.levels[ Database.currLvIndex + 1 ];
			lvData.isUnlocked = true;
		}
	}


	public 
	void OnPlayAgain()
	{
		SceneManager.LoadScene( "Gameplay" );
	}


	public 
	void OnGoToLevels()
	{
		SceneManager.LoadScene( "Levels" );
	}


	public 
	void OnNextLevel()
	{
		// if last level
		if ( Database.currLvIndex == Database.levels.Count - 1 )
		{
			SceneManager.LoadScene( "Levels" );
			return;
		}

		Database.currLvIndex++;
		SceneManager.LoadScene( "Gameplay" );
	}
}
