using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
	public Level[]  levels;


	void Start ()
	{
		if ( !GlobalData.isInitialized )
		{
			GlobalData.isInitialized = true;
			GlobalData.levels = new LevelData[levels.Length];

			for ( int i = 0; i < levels.Length; ++i )
			{
				LevelData lvData = new LevelData();
				GlobalData.levels[i] = lvData;
				lvData.isUnlocked = (i == 0);
				lvData.starsCount = 0;
				lvData.bestCombo = 0;
	        }
		}

		for ( int i = 0; i < levels.Length; ++i )
		{
			Level lv = levels[i];
			LevelData lvData = GlobalData.levels[i];

			lv.lvIndex = i;
			lv.titleText.text = "Level " + (i + 1);

			for ( int j = 0; j < 3; ++j )
			{
				lv.stars[j].SetActive( (j < lvData.starsCount) );
			}

			if ( lvData.bestCombo > 0 )
			{
				lv.comboText.text = "Combo x" + lvData.bestCombo;
			}
			else
			{
				lv.comboText.text = "No combo";
			}

			lv.unlockedSide.SetActive( lvData.isUnlocked );
			lv.lockedSide.SetActive( !lvData.isUnlocked );
		}
	}
	
	void Update () {
	
	}
}
