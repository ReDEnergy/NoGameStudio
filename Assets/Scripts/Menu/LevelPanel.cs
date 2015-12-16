using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LevelPanel : MonoBehaviour
{
	public GameObject[]     stars;
	public Text             comboText;
	public Text             titleText;
	public GameObject       unlockedSide;
	public GameObject       lockedSide;
	public Level            level;

	[HideInInspector]
	public int              lvIndex;



	public void OnStart()
	{
		Database.currLvIndex	= lvIndex;
		Database.currLevel    = level;

		SceneManager.LoadScene( "Gameplay" );
	}
}
