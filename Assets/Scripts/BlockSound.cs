using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using HoloToolkit.Unity;

public class BlockSound : MonoBehaviour
{
	private AudioSource audioSource = null;
	private readonly System.Random rnd = new System.Random();
    float _bumpVolume = .3f;
    float _bangVolume = 1f;

	// Use this for initialization
	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.spatialize = true;
		audioSource.spatialBlend = 1.0f;
		audioSource.dopplerLevel = 1.0f;
		audioSource.loop = false;
	}

	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if (gameObject.GetComponent<Rigidbody>() != null && collision.gameObject.GetComponent<Rigidbody>() != null )
		{
            if (collision.relativeVelocity.magnitude > .5)
            {
                var source = rnd.Next(1, 4);
                audioSource.clip = Resources.Load<AudioClip>("block" + source.ToString());
                audioSource.volume = _bangVolume;
                audioSource.Play();
            }
            else if ( collision.relativeVelocity.magnitude > .1)
            {
                audioSource.clip = Resources.Load<AudioClip>("Bump");
                audioSource.volume = _bumpVolume;
                audioSource.Play();
            }
        }
    }
}
