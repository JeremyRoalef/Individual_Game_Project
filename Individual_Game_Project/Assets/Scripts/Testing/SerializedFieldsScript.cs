/*
 * This script is testing the visibility of attributes in the inspector. The attributes are declared here
 * and are shown/hidden based on the corresponding script editor (SDScriptEditor.cs controls this script)
 */

using UnityEngine;

public class SerializedFieldsScript : MonoBehaviour
{
    //Experimenting with showing different attributes based on the state of an enum

    public enum ScriptMode
    {
        Mode1,
        Mode2,
        Mode3
    }

    [Tooltip("Select different mode types to experiemnt with hiding and showing attributes")]
    public ScriptMode mode;

    [SerializeField]
    public int attributeInMode1 = 1;

    [SerializeField]
    public int attributeInMode2 = 2;

    [SerializeField]
    public int attributeInMode3 = 3;
}
