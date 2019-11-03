using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadScript : MonoBehaviour
{
    private StreamReader scriptReader;
    [SerializeField]
    private TextAsset asset;
    [SerializeField]
    private List<string> scriptLines;

    // Start is called before the first frame update
    void Start()
    {
        scriptLines = asset.text.Split('\n').ToList();
        Debug.LogWarning(scriptLines.Count);
    }

}
