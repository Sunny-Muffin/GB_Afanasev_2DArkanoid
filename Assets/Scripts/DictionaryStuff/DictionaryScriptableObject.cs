using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Dictionary Storage Object")]
public class DictionaryScriptableObject : ScriptableObject
{
    [SerializeField] private List<string> _keys = new List<string>();
    [SerializeField] private List<int> _values = new List<int>();

    public List<string> Keys { get => _keys; set => _keys = value; }
    public List<int> Values { get => _values; set => _values = value; }

}
