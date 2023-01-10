using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorDictionary))]
public class DictionaryScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (((InspectorDictionary)target)._modifyValues)
        {
            if (GUILayout.Button("Save changes"))
            {
                ((InspectorDictionary)target).DeserializeDictionary();
            }

        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Print Dictionary"))
        {
            ((InspectorDictionary)target).PrintDictionary();
        }
    }
}
