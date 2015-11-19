using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Level : MonoBehaviour
{
	public GameObject[]     stars;
	public Text             comboText;
	public Text             titleText;
	public GameObject       unlockedSide;
	public GameObject       lockedSide;

	[HideInInspector]
	public int              lvIndex;



	public void OnStart()
	{
		GlobalData.currLvIndex = lvIndex;

		Application.LoadLevel( "Gameplay" );
	}
}
