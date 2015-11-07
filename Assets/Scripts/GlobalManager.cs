using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class GlobalManager : MonoBehaviour {
	#region GlobalManager's Things - DON'T CHANGE!!!
	private static GlobalManager _instance;
	
	public static GlobalManager instance {
		get {
			if ( _instance == null ) {
				_instance = GameObject.FindObjectOfType<GlobalManager> ( );
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad ( _instance.gameObject );
			}
			
			return _instance;
		}
	}
	
	void Awake ( ) {
		if ( _instance == null ) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad ( this );
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if ( this != _instance )
				Destroy ( this.gameObject );
		}
	//	Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.SetResolution ( Screen.currentResolution.width, Screen.currentResolution.height, true );
	//	Screen.fullScreen = true;
	}
	#endregion    
	static int printSreenCounter = 0;
	


	
	
	void Start ( ) {

	}
	

}