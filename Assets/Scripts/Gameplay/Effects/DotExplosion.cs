using UnityEngine;
using System.Collections;

public class DotExplosion : MonoBehaviour
{
	public float timeToLive;


	void Start ()
	{
		Destroy( gameObject, timeToLive );
	}
}
