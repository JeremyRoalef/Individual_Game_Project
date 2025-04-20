using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObject/CardSO")]
public class CardSO : ScriptableObject
{
    [Tooltip("The positions on the board that the player can move to relative to its position (i.e., (2,3) would mean the player can move to the position 2 unity right and 3 units upward). Keep numbers as an integer!!!")]
    public Vector2[] movePositions;

    [Tooltip("The abilities that this card can perform. Example abilities include attacking, healing, blocking, etc.")]
    public AbilitySO[] abilities;

    [Tooltip("The starting HP that this card will have on pickup")]
    public int baseHp = 10;

    [Tooltip("The shielding value this card will have on pickup")]
    public int baseShielding = 0;
}
