using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[System.Serializable]
public class ColorData
{
	public int      colorIndex;
	public Color	color;
	public int      dotsLeft;
}



[System.Serializable]
public class DEBUG
{
	public Text		missesText;
}



public class GameManager : MonoBehaviour
{
	static public bool gameON = true;

	public List<ColorData>	colors;
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


	static public GameManager singleton { get; private set; }

	void Awake()
	{
		singleton = this;
	}


	void Start()
	{
		gameON = true;

		if ( GlobalData.currLevel != null )
		{
			Instantiate( GlobalData.currLevel );
		}

		foreach ( ColorData data in colors )
		{
			_totalDots += data.dotsLeft;
		}
	}


	void Update()
	{
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


	public void OnDotDestroyed( int colorIndex )
	{
		ColorData data = GetColor( colorIndex );
		data.dotsLeft--;
		_AddCombo();

		if ( data.dotsLeft <= 0 )
		{
			colors.Remove( data );

			if ( colors.Count == 0 )
			{
				gameON = false;
				_LevelComplete();
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


	public void DotMissed()
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
			LevelData lvData = GlobalData.levels[ GlobalData.currLvIndex ];
			lvData.starsCount = _starsReceived;
			lvData.bestCombo = _bestCombo;
		}

		if ( GlobalData.currLvIndex < GlobalData.levels.Length - 1 )
		{
			LevelData lvData = GlobalData.levels[ GlobalData.currLvIndex + 1 ];
			lvData.isUnlocked = true;
		}
	}


	public void OnPlayAgain()
	{
		Application.LoadLevel( "Gameplay" );
	}


	public void OnGoToLevels()
	{
		Application.LoadLevel( "Levels" );
	}


	public void OnNextLevel()
	{
		// if last level
		if ( GlobalData.currLvIndex == GlobalData.levels.Length - 1 )
		{
			Application.LoadLevel( "Levels" );
			return;
		}

		GlobalData.currLvIndex++;
		Application.LoadLevel( "Gameplay" );
	}
}
