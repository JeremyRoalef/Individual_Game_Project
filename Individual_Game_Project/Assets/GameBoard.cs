using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    //SerializedFields
    [Header("Player Color States")]

    [SerializeField]
    Color playerPosColor = Color.yellow;

    [SerializeField]
    Color playerSelectColor = Color.green;


    [Header("Debugging")]

    [SerializeField]
    Vector2Int playerSelectPos = new Vector2Int(0, 0);

    //Cashe References
    Dictionary<Vector2Int, Tile> gameBoard = new Dictionary<Vector2Int, Tile>();
    Dictionary<Vector2Int, GameObject> characterObjPos = new Dictionary<Vector2Int, GameObject>();

    Tile selectedTile;
    Vector2Int selectedCharacterPos = new Vector2Int();
    GameObject selectedObj;

    //Attributes
    bool playerSelectedTile = false;
    bool playerSelectedCharacter = false;

    private void Start()
    {
        //Get all child tile objects
        Tile[] gameBoardTiles = GetComponentsInChildren<Tile>();

        //Add tile objects to the game board. Their x-y position will be stored as a vector2int
        foreach (Tile tile in gameBoardTiles)
        {
            gameBoard.Add(
                new Vector2Int(
                    (int)tile.transform.position.x, 
                    (int)tile.transform.position.y
                    ),
                tile
                );
        }

        //Set the current player position
        if (gameBoard[playerSelectPos].TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
        {
            renderer.color = playerPosColor;
        }
    }

    private void Update()
    {
        //Check if player wants to move
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayerUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayerDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayerLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayerRight();
        }

        //Check if player wants to select current tile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectTileObject();
        }

        //Check if player wants to deselect current tile
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectTileObject();
        }
    }

    //Method to move player to the given position
    void MovePlayerToPos(Vector2Int newPos)
    {
        //Set the current tile the player is on back to its tile color only if it's not the selected tile
        if (gameBoard[playerSelectPos] != selectedTile)
        {
            gameBoard[playerSelectPos].SetTileColor();
        }


        //get square renderer at new position and update the color if the tile is not selected
        if(gameBoard[newPos].TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer) && 
            gameBoard[newPos] != selectedTile)
        {
            //Update the color
            renderer.color = playerPosColor;
        }
        else
        {
            Debug.Log($"No tile at position {newPos}");
        }

        //Set player position to new position
        playerSelectPos = newPos;
    }
    
    //Method to check if player can move upwards
    void MovePlayerUp()
    {
        Vector2Int newPlayerPos = new Vector2Int(playerSelectPos.x, playerSelectPos.y+1);

        //Check if the tile above the player exists
        if (gameBoard.ContainsKey(newPlayerPos))
        {
            MovePlayerToPos(newPlayerPos);
        }
    }

    //Method to check if player can move downwards
    void MovePlayerDown()
    {
        Vector2Int newPlayerPos = new Vector2Int(playerSelectPos.x, playerSelectPos.y - 1);

        //Check if the tile above the player exists
        if (gameBoard.ContainsKey(newPlayerPos))
        {
            MovePlayerToPos(newPlayerPos);
        }
    }

    //Method to check if player can move left
    void MovePlayerLeft()
    {
        Vector2Int newPlayerPos = new Vector2Int(playerSelectPos.x - 1, playerSelectPos.y);

        //Check if the tile above the player exists
        if (gameBoard.ContainsKey(newPlayerPos))
        {
            MovePlayerToPos(newPlayerPos);
        }
    }

    //Method to check if player can move right
    void MovePlayerRight()
    {
        Vector2Int newPlayerPos = new Vector2Int(playerSelectPos.x + 1, playerSelectPos.y);

        //Check if the tile above the player exists
        if (gameBoard.ContainsKey(newPlayerPos))
        {
            MovePlayerToPos(newPlayerPos);
        }
    }

    //Method to select current tile object
    void SelectTileObject()
    {
        HandleCharacterLogic();

        //If the player already selected a tile, Do not continue, & player is no longer selecting a tile
        if (playerSelectedTile)
        {
            DeselectTileObject();
            return;
        }
        //Otherwise, the player is selecting the current tile
        else
        {
            //Player selected a tile
            playerSelectedTile = true;
            selectedTile = gameBoard[playerSelectPos];
            if (selectedObj != null)
            {
                Debug.Log($"Selected object: {selectedObj.name}");
            }
            //Set the tile object color to given player selected color
            if (gameBoard[playerSelectPos].TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
            {
                renderer.color = playerSelectColor;
            }
        }
    }

    private void HandleCharacterLogic()
    {
        //If the player selected an object & the selected position doesn't have a character object, move the character to the position
        if (selectedObj != null && !characterObjPos.ContainsKey(playerSelectPos) && playerSelectedCharacter == true)
        {
            MoveCharacterObject(selectedObj, selectedCharacterPos, playerSelectPos);
        }
        //Otherwise, player selected character object on this tile (if there is one)
        else
        {
            if (characterObjPos.ContainsKey(playerSelectPos))
            {
                selectedObj = characterObjPos[playerSelectPos];
                playerSelectedCharacter = true;
                selectedCharacterPos = playerSelectPos;
            }
        }
    }

    //Method to deselect current tile object
    void DeselectTileObject()
    {
        //Check if the object was selected. No need to run if they haven't
        if (!playerSelectedTile) { return; }

        //Player is not selecting an object
        selectedObj = null;
        playerSelectedCharacter = false;

        //Reset the color of the tile to player color
        if (gameBoard[playerSelectPos] == selectedTile && 
            selectedTile.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
        {
            renderer.color = playerPosColor;
        }
        else
        {
            selectedTile.SetTileColor();
        }

        //Player is no longer selecting a tile
        playerSelectedTile = false;
        selectedTile = null;
    }

    public void AddCharacterToBoard(GameObject characterObj, Vector2Int pos)
    {
        //Set chararcter obj to the given position
        characterObj.transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        //update the character's position in the dictionary
        characterObjPos.Add(pos, characterObj);
    }

    void MoveCharacterObject(GameObject characterObj, Vector2Int currentPos, Vector2Int newPos)
    {
        //Move the charactrer visually
        characterObj.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        //update the character's position in the dictionary
        characterObjPos.Remove(currentPos);
        characterObjPos.Add(newPos, characterObj);
    }
}
