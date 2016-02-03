using UnityEngine;
using System.Collections;

public class ExplosionDot : MonoBehaviour
{

	public float timeToLive;
    public AudioClip[] audioClips;

    void Start ()
	{
		Destroy( gameObject, timeToLive );
	}
}
