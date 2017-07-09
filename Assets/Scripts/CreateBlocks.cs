using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBlocks : MonoBehaviour
{
    IList<Material> _materials = new List<Material>();
    IList<GameObject> _objects = new List<GameObject>();
    System.Random _ran = new System.Random();
    
    public float Height = .5f;
    public float Spacing = .5f;
    public int XCount = 10;
    public int YCount = 1;
    public int ZCount = 10;
    public float CameraDistance = 1.0f;

    // Use this for initialization
    void Start()
    {
        // for (int i = 1; i <= 10; i++)
        //     _materials.Add(Resources.Load<Material>(string.Format("wall{0:00}", i)));
        _materials.Add( Resources.Load<Material>("wall09") );
        _materials.Add( Resources.Load<Material>("water") );
        _materials.Add( Resources.Load<Material>("lava") );

        for ( int i = 1; i < 2; i++ )
            _objects.Add(Resources.Load<GameObject>(string.Format("Shape{0}", i)));
    }

    void DropBlocks()
    {
        OnSelect();
    }

    // Update is called once per frame
    void OnSelect() 
    {
        // var location = Camera.main.transform.position + new Vector3(0, 0, 1.5f); // this.transform.position + new Vector3(0, .5f, 0);
        var direction = Camera.main.transform.forward;
        var origin = Camera.main.transform.position;
        var location = origin + direction * CameraDistance;
        location.x = location.x - (Spacing * XCount / 2.0f);

        for (int x = 0; x < XCount; x++)
            for (int y = 0; y < YCount; y++)
                for (int z = 0; z < ZCount; z++)
                {
                    var material = _materials[_ran.Next(0, _materials.Count)];

                    var go = Instantiate<GameObject>(_objects[_ran.Next(0, _objects.Count)]); //   GameObject.CreatePrimitive();
                    go.GetComponent<Renderer>().material = material;
                    //  go.transform.localScale = new Vector3(Scale * variance, Scale * variance, Scale * variance);
                    go.transform.position = new Vector3(x * Spacing, Height, z * Spacing) + location;
                    go.transform.rotation = new Quaternion((float)_ran.NextDouble(), (float)_ran.NextDouble(), (float)_ran.NextDouble(), 1);
                    go.AddComponent<Rigidbody>();
                }
    }


}
