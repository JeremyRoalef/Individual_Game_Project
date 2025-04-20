/*
 * This script is an editor script that changes the shown attributes of another script in Unity's inspector
 * Use this layout as a template for showing different attributes for a script in the inspector
 */

//Credit for template: ChatGPT

using UnityEditor;
using UnityEngine;

//Custom editor for my script I am testing
[CustomEditor(typeof(SerializedFieldsScript))]
public class SFScriptEditor : Editor
{
    const string SF_SCRIPT_MODE_1 = "Mode1";
    const string SF_SCRIPT_MODE_2 = "Mode2";
    const string SF_SCRIPT_MODE_3 = "Mode3";

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        //Get the script
        SerializedFieldsScript sfScript = (SerializedFieldsScript)target;
        
        //Set Default Mode
        sfScript.mode = (SerializedFieldsScript.ScriptMode) EditorGUILayout.EnumPopup(
            SF_SCRIPT_MODE_1, sfScript.mode);

        //Switch between attributes shown based on the enum value
        switch (sfScript.mode)
        {
            case SerializedFieldsScript.ScriptMode.Mode1:
                DisplayAttributesInMode1(sfScript);
                break;
            case SerializedFieldsScript.ScriptMode.Mode2:
                DisplayAttributesInMode2(sfScript);
                break;
            case SerializedFieldsScript.ScriptMode.Mode3:
                DisplayAttributesInMode3(sfScript);
                break;
        }
        
        //This changes the visible attributes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(sfScript);
        }
    }

    private static void DisplayAttributesInMode1(SerializedFieldsScript sfScript)
    {
        sfScript.attributeInMode1 = EditorGUILayout.IntField("attributeInMode1", sfScript.attributeInMode1);
    }
    private static void DisplayAttributesInMode2(SerializedFieldsScript sfScript)
    {
        sfScript.attributeInMode2 = EditorGUILayout.IntField("attributeInMode2", sfScript.attributeInMode2);
    }
    private static void DisplayAttributesInMode3(SerializedFieldsScript sfScript)
    {
        sfScript.attributeInMode3 = EditorGUILayout.IntField("attributeInMode3", sfScript.attributeInMode3);
    }
}
