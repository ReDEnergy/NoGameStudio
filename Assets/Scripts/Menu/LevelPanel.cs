using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LevelPanel : MonoBehaviour
{
	public GameObject[]     stars;
	public Text             comboText;
	public Text             levelNumberText;
	public Text             levelNameText;
	public GameObject       unlockedSide;
	public GameObject       lockedSide;
	public Level            level;

	[HideInInspector]
	public int              lvIndex;



	public void OnStart()
	{
		Database.currLvIndex = lvIndex;

		SceneManager.LoadScene( "Gameplay" );
	}
}
