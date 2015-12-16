using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[System.Serializable]
public class DEBUG
{
	public Text		missesText;
	public Level    level;
}



public class GameplayManager : MonoBehaviour
{
	public float            inactiveComboTime;
	public Text             comboText;
	public GameObject       lvDonePanel;
	public GameObject[]     stars;
	public Text             maxComboText;
	public float[]          starMissCritaria;  // maximum (misses / total dots) ratio per star accepted
	public DEBUG            __debug;

	int     _bestCombo = 0;
	int		_combo = 0;
	float	_comboTimer;
	int     _missesCount = 0;
	int     _totalDots = 0;
	int     _starsReceived;

	Dictionary<int, int>    _colorsLeft = new Dictionary<int, int>();

	public Level currLevel { get; private set; }


	static public GameplayManager singleton { get; private set; }

	void Awake()
	{
		singleton = this;

		Database.gameON = true;

		if ( Database.currLevel != null )
		{
			currLevel = Instantiate( Database.currLevel );
		}
		else
		{
			currLevel = __debug.level;
		}

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


		if ( _combo > 0 )
		{
			_comboTimer += Time.deltaTime;

			float alpha = Mathf.Lerp( 1, 0, _comboTimer / inactiveComboTime );
			_ChangeComboAlpha( alpha );

			if ( _comboTimer >= inactiveComboTime )
			{
				_combo = 0;
			}
		}
	}


	public 
	void OnDotDestroyed( int colorIndex )
	{
		ColorData data = GetColor( colorIndex );
		_colorsLeft[colorIndex]--;
		_AddCombo();

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


	void _AddCombo()
	{
		_combo++;
		_comboTimer = 0;

		comboText.text = "Combo x" + _combo;
		_ChangeComboAlpha( 1 );

		if ( _combo > _bestCombo )
		{
			_bestCombo = _combo;
		}
	}


	public 
	void DotMissed()
	{
		_missesCount++;
		__debug.missesText.text = "misses: " + _missesCount;
	}


	void _ChangeComboAlpha( float amount )
	{
		Color textColor = comboText.color;
		textColor.a = amount;
		comboText.color = textColor;
	}


	void _LevelComplete()
	{
		lvDonePanel.SetActive( true );
		maxComboText.text = "Combo x" + _bestCombo;

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
			lvData.bestCombo = _bestCombo;
		}

		if ( Database.currLvIndex < Database.levels.Length - 1 )
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


	public void OnNextLevel()
	{
		// if last level
		if ( Database.currLvIndex == Database.levels.Length - 1 )
		{
			SceneManager.LoadScene( "Levels" );
			return;
		}

		Database.currLvIndex++;
		SceneManager.LoadScene( "Gameplay" );
	}
}
