using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    //Commonly-shared ability attributes
    [Header("General")]
    
    [Tooltip("The name that will be displayed")]
    public string abilityName = "Ability";
    [Tooltip("The image sprite that will be displayed")]
    public Sprite icon;
    [TextArea(5,15)] [Tooltip("The description of this ability (if applicable)")]
    public string abilityDescription = "This does something";

    public abstract void OnAbilityUsed();
}
