using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class DestroyObjects : MonoBehaviour
{
    [Tooltip("Object name regex to find and destroy")]
	public string ObjectNameRegex;

    void OnSelect()
    {
        Regex r = new Regex(ObjectNameRegex);

        foreach (var o in FindObjectsOfType<GameObject>().Where(o => r.IsMatch( o.name )))
            Destroy(o);
    }

}