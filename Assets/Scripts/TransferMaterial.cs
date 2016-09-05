using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using HoloToolkit.Unity;

public class TransferMaterial : MonoBehaviour
{
    Renderer _renderer = null;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
        var collidedRenderer = collision.collider.gameObject.GetComponent<Renderer>();
        if (collidedRenderer.material != _renderer.material )
        {
            collidedRenderer.material = _renderer.material;
        }
	}
}
