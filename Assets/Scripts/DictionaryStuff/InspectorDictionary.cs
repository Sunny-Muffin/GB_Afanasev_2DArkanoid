using System;
using System.Collections.Generic;
using UnityEngine;

public class InspectorDictionary : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] DictionaryScriptableObject _dictionaryData;

    [SerializeField] List<string> _keys = new List<string> ();
    [SerializeField] List<int> _values = new List<int> ();

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<string, int> _myDictionary = new Dictionary<string, int>();

    public bool _modifyValues;

    private void Awake()
    {
        for (int i = 0; i < Mathf.Min(_dictionaryData.Keys.Count, _dictionaryData.Values.Count); ++i)
        {
            _myDictionary.Add(_dictionaryData.Keys[i], _dictionaryData.Values[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        if (!_modifyValues)
        {
            _keys.Clear();
            _values.Clear();

            for (int i = 0; i < Math.Min(_dictionaryData.Keys.Count, _dictionaryData.Values.Count); ++i)
            {
                _keys.Add(_dictionaryData.Keys[i]);
                _values.Add(_dictionaryData.Values[i]);
            }
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        //Debug.Log("Deserialization");
        _myDictionary = new Dictionary<string, int>();
        _dictionaryData.Keys.Clear();
        _dictionaryData.Values.Clear();
        for (int i = 0; i < Mathf.Min(_keys.Count, _values.Count); ++i)
        {
            _dictionaryData.Keys.Add(_keys[i]);
            _dictionaryData.Values.Add(_values[i]);
            _myDictionary.Add(_keys[i], _values[i]);
        }
        _modifyValues = false;
    }

    public void PrintDictionary()
    {
        foreach (var pair in _myDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
}

