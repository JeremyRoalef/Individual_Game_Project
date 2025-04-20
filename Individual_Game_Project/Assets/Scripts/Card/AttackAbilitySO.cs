using UnityEngine;

[CreateAssetMenu(fileName = "AtackAbilitySO", menuName = "ScriptableObject/AttackAbilitySO")]
public class AttackAbilitySO : AbilitySO
{
    public enum AttackType
    {
        Physical,
        Magic
    }

    [Space(20)]
    [Header("Attacking Attributes")]

    [Tooltip("The positions that this attack will affect relative to the card's position")]
    public Vector2[] attackPositions;
    [Tooltip("The type of damage this attack will do")]
    public AttackType attackType = AttackType.Physical;
    [Tooltip("The amount of damage this attack will do")]
    public int damage = 5;
    [Tooltip("Does this attack ignore the enemy's shield?")]
    public bool ignoresShields = false;
    [Tooltip("Will this attack be blocked by debris?")]
    public bool isBlockedByDebris = false;

    //Add status effects this applies (to self or other)

    public override void OnAbilityUsed()
    {
        //Logic when ability use used
    }
}
