using UnityEngine;
using UnityEngine.Rendering;

public class Character : MonoBehaviour
{
    [SerializeField]
    Vector2Int characterStartingPos;

    //NOTE: Must cast vector2 values from float to int to handle board positions before moving
    [SerializeField]
    Vector2[] characterMovingPos;

    GameBoard gameBoard;

    private void Start()
    {
        gameBoard = Object.FindFirstObjectByType<GameBoard>();

        if ( gameBoard != null)
        {
            gameBoard.AddCharacterToBoard(gameObject, characterStartingPos);
        }
    }

    public Vector2[] GetMovingPositions()
    {
        return characterMovingPos;
    }
}
