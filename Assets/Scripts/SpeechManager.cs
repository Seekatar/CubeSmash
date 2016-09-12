using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    Material _rock;
    Material _water;
    Material _lava;
    private AudioSource audioSource = null;

    GameObject _hammer = null;
    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 1.0f;
        audioSource.loop = false;
        audioSource.clip = Resources.Load<AudioClip>("Explosion");

        var p = FindObjectsOfType<GameObject>().SingleOrDefault(o => o.name == "Plane");
        if ( p != null )
            p.SetActive(false);

        _hammer = FindObjectsOfType<GameObject>().Where(o => o.name == "Hammer").FirstOrDefault();
        _rock = Resources.Load<Material>("wall09");
        _water = Resources.Load<Material>("water");
        _lava = Resources.Load<Material>("lava");

        keywords.Add("gravity on", () =>
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);

        });
        keywords.Add("gravity off", () =>
        {
            if (Physics.gravity.y < -9)
                Physics.gravity = new Vector3(0, +9.81f, 0);
            this.Invoke("gravityOff1", .4f);

        });
        keywords.Add("rock on", () =>
        {
            _hammer.GetComponent<Renderer>().material = _rock;
        });
        keywords.Add("flame on", () =>
        {
            _hammer.GetComponent<Renderer>().material = _lava;
        });
        keywords.Add("water on", () =>
        {
            _hammer.GetComponent<Renderer>().material = _water;
        });
        keywords.Add("nuke everything", () =>
        {
            audioSource.Play();
            foreach ( var go in FindObjectsOfType<GameObject>().Where( o => o.name.EndsWith("(Clone)")))
            {
                Destroy(go);
            }
            
        });
        keywords.Add("drop it", () =>
        {
            TapToPlaceEx.DropIt();
        });
        keywords.Add("bring it on", () =>
        {
            BroadcastMessage("DropBlocks");
        });
        keywords.Add("fetch the ball", () =>
        {
            _hammer.SendMessage("OnSelect");
        });
        keywords.Add("mesh on", () =>
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = true;
        });
        keywords.Add("mesh off", () =>
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = false;
        });
        keywords.Add("plane on", () =>
        {
            if ( p != null ) 
                p.SetActive(true);
        });
        keywords.Add("plane off", () =>
        {
            if (p != null)
                p.SetActive(false);
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    public void gravityOff1()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        this.Invoke("gravityOff2",.3f);
    }

    public void gravityOff2()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        foreach (var go in FindObjectsOfType<GameObject>())
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = Vector3.zero;
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}