using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
	public LevelPanel[]  levelPanels;


	static public LevelsManager singleton { get; private set; }

	void Awake()
	{
		singleton = this;
	}

	void Start ()
	{
		if ( !GlobalData.isInitialized )
		{
			GlobalData.isInitialized = true;
			GlobalData.levels = new LevelData[levelPanels.Length];

			for ( int i = 0; i < levelPanels.Length; ++i )
			{
				LevelData lvData = new LevelData();
				GlobalData.levels[i] = lvData;
				lvData.isUnlocked = (i == 0);
				lvData.starsCount = 0;
				lvData.bestCombo = 0;
	        }
		}

		for ( int i = 0; i < levelPanels.Length; ++i )
		{
			LevelPanel lvPanel = levelPanels[i];
			LevelData lvData = GlobalData.levels[i];

			lvPanel.lvIndex = i;
			lvPanel.titleText.text = "Level " + (i + 1);

			for ( int j = 0; j < 3; ++j )
			{
				lvPanel.stars[j].SetActive( (j < lvData.starsCount) );
			}

			if ( lvData.bestCombo > 0 )
			{
				lvPanel.comboText.text = "Combo x" + lvData.bestCombo;
			}
			else
			{
				lvPanel.comboText.text = "No combo";
			}

			lvPanel.unlockedSide.SetActive( lvData.isUnlocked );
			lvPanel.lockedSide.SetActive( !lvData.isUnlocked );
		}
	}
	
}
