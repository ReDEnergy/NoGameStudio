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



public class GameManager : MonoBehaviour
{
	static public bool gameON = true;

	public List<ColorData>	colors;
	public float            inactiveComboTime;
	public Text             comboText;
	public GameObject       lvDonePanel;
	public GameObject[]     stars;
	public Text             maxComboText;
	public float[]          missCritaria;

	int     _bestCombo = 0;
	int		_combo = 0;
	float	_comboTimer;
	int     _missesCount = 0;


	void Start()
	{
		gameON = true;
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
	}


	public void OnPlayAgain()
	{
		Application.LoadLevel( "Gameplay" );
	}
}
