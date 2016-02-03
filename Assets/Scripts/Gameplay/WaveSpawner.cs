using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	public float	spawnDelay;
	public float	cornerOffset;

    private int     currentWave = 0;
    private int[]   colors = { 0, 1 };
    private int[]   directions = { 1, 0 };

	Vector3[]		_startPoints	= new Vector3[8];
	Quaternion[]	_rotations		= new Quaternion[8];


	void Start ()
	{
		Vector3 rightSide = new Vector3( Screen.width, Screen.height / 2, 0 );
		Vector3 mostRight = Camera.main.ScreenToWorldPoint( rightSide );
		mostRight.z = 0;

		Vector3 topSide = new Vector3( Screen.width / 2, Screen.height, 0 );
		Vector3 mostTop = Camera.main.ScreenToWorldPoint( topSide );
		mostTop.z = 0;

		_startPoints[0] = mostRight;
		_startPoints[1] = - mostRight;
		_startPoints[2] = mostTop;
		_startPoints[3] = - mostTop;
		_startPoints[4] = mostTop + mostRight + new Vector3( - cornerOffset, - cornerOffset, 0 );
		_startPoints[5] = mostTop - mostRight + new Vector3( cornerOffset, - cornerOffset, 0 );
		_startPoints[6] = - mostTop + mostRight + new Vector3( - cornerOffset, cornerOffset, 0 );
		_startPoints[7] = - mostTop - mostRight + new Vector3( cornerOffset, cornerOffset, 0 );

		_rotations[0] = Quaternion.Euler( 0, 0, 0 );
		_rotations[1] = Quaternion.Euler( 0, 0, 180 );
		_rotations[2] = Quaternion.Euler( 0, 0, 90 );
		_rotations[3] = Quaternion.Euler( 0, 0, -90 );
		_rotations[4] = Quaternion.Euler( 0, 0, 45 );
		_rotations[5] = Quaternion.Euler( 0, 0, 135 );
		_rotations[6] = Quaternion.Euler( 0, 0, -45 );
		_rotations[7] = Quaternion.Euler( 0, 0, -135 );

		StartCoroutine( _SpawnLoop() );
	}
	

	IEnumerator _SpawnLoop()
	{
		while ( true )
		{
			yield return new WaitForSeconds( spawnDelay );
			
			if ( Database.gameON )
			{
				_SpawnWave();
			}
		}
	}


	void _SpawnWave()
	{
		int chosenStart = Random.Range( 0, _startPoints.Length );
		int chosenColor = Random.Range( 0, GameplayManager.singleton.currLevel.colors.Count );

        chosenStart = directions[currentWave%2];
        chosenColor = colors[currentWave%2];
        currentWave++;

		Wave wave = Instantiate( GameplayManager.singleton.currLevel.wavePrefab );
		wave.colorIndex			= GameplayManager.singleton.currLevel.colors[chosenColor].colorIndex;
		wave.transform.position = _startPoints[chosenStart];
		wave.transform.rotation = _rotations[chosenStart];
		wave.transform.SetParent( transform );
	}
}
