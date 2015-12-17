using UnityEngine;
using System.Collections;


[System.Serializable]
public class PanelSpawn
{
	public LevelPanel	prefab;
	public float        startX;
	public float        startY;
	public float		jumpX;
	public float        jumpY;
	public int			perRow;
	public Transform    container;
}


public class LevelPageManager : MonoBehaviour
{
	public PanelSpawn	panelSpawn;
	public Level[]		levels;


	static public LevelPageManager singleton { get; private set; }
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
		Database.Initialize();	


		float currX = panelSpawn.startX;
		float currY = panelSpawn.startY;

		for ( int i = 0; i < Database.levels.Count; ++i )
		{
			LevelData lvData = Database.levels[i];

			LevelPanel lvPanel = Instantiate( panelSpawn.prefab );
			lvPanel.transform.SetParent( panelSpawn.container );
			lvPanel.transform.localPosition = new Vector3( currX, currY, 0 );

			currX += panelSpawn.jumpX;
			if ( (i % 3) == (panelSpawn.perRow - 1) )
			{
				currX = panelSpawn.startX;
				currY += panelSpawn.jumpY;
			}

			lvPanel.lvIndex = i;
			lvPanel.levelNumberText.text = "Level " + (i + 1);
			lvPanel.levelNameText.text = levels[i].name;

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
