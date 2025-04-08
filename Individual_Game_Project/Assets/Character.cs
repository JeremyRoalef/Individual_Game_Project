using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    Vector2Int characterStartingPos;

    GameBoard gameBoard;

    private void Start()
    {
        gameBoard = FindObjectOfType<GameBoard>();

        if ( gameBoard != null)
        {
            gameBoard.AddCharacterToBoard(gameObject, characterStartingPos);
        }
    }
}
