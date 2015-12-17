using UnityEngine;
using System.Collections;

public class ExplosionDot : MonoBehaviour
{
	public float timeToLive;


	void Start ()
	{
		Destroy( gameObject, timeToLive );
	}
}
