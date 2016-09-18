using UnityEngine;
using System.Collections;

public class BoxTarget : MonoBehaviour
{
	private AudioSource audioSource = null;

	// Use this for initialization
	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.spatialize = true;
		audioSource.spatialBlend = 1.0f;
		audioSource.dopplerLevel = 1.0f;
		audioSource.loop = false;
		audioSource.clip = Resources.Load<AudioClip>("blockbreak");

	}

	void OnCollisionEnter(Collision collision)
	{
		audioSource.Play();
		var detonator = GetComponent<Detonator>();
		if(detonator != null)
			detonator.Explode();
		Destroy(collision.gameObject);

	}
}
