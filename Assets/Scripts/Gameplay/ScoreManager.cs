using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour 
{
	public float	inactiveComboTime;
	public Text		comboText;
	
	int		_combo = 0;
	int     _bestCombo = 0;
	float	_comboTimer;


	static public ScoreManager singleton { get; private set; }
	void Awake()
	{
		singleton = this;
	}
	void OnDestroy()
	{
		singleton = null;
	}


	void Start ()
	{
	
	}
	

	void Update () 
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


	public
	void AddCombo()
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
	int GetBestCombo()
	{
		return _bestCombo;
	}


	void _ChangeComboAlpha( float amount )
	{
		Color textColor = comboText.color;
		textColor.a = amount;
		comboText.color = textColor;
	}
}
