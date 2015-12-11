using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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
		GlobalData.currLvIndex	= lvIndex;
		GlobalData.currLevel    = level;

		Application.LoadLevel( "Gameplay" );
	}
}
