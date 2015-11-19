using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	public Wave		wavePrefab;
	public float	spawnDelay;
	public float    moveSpeed;


	Vector3[]		_startPoints	= new Vector3[4];
	Vector3[]		_moveDirections	= new Vector3[4];
	Quaternion[]	_rotations		= new Quaternion[4];

	GameManager _GM;


	void Start ()
	{
		_GM = FindObjectOfType<GameManager>();

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

			_moveDirections[0] = Vector3.left;
			_moveDirections[1] = Vector3.right;
			_moveDirections[2] = Vector3.down;
			_moveDirections[3] = Vector3.up;

			_rotations[0] = Quaternion.Euler( 0, 0, 0 );
			_rotations[1] = Quaternion.Euler( 0, 0, 0 );
			_rotations[2] = Quaternion.Euler( 0, 0, 90 );
			_rotations[3] = Quaternion.Euler( 0, 0, 90 );
		}

		StartCoroutine( _SpawnLoop() );
	}
	

	IEnumerator _SpawnLoop()
	{
		while ( true )
		{
			if ( GameManager.gameON )
			{
				_SpawnWave();
			}
			yield return new WaitForSeconds( spawnDelay );
		}
	}


	void _SpawnWave()
	{
		int chosenStart = Random.Range( 0, 4 );
		int chosenColor = Random.Range( 0, _GM.colors.Count );

		Wave wave = Instantiate( wavePrefab );
		wave.moveSpeed			= moveSpeed;
		wave.colorIndex			= _GM.colors[chosenColor].colorIndex;
		wave.moveDirection		= _moveDirections[chosenStart];
		wave.transform.position = _startPoints[chosenStart];
		wave.transform.rotation = _rotations[chosenStart];
		wave.transform.SetParent( transform );
	}
}
