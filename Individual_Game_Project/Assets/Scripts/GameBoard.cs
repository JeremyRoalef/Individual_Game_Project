using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameBoard : MonoBehaviour
{

    //SerializedFields
    [Header("Player Color States")]

    [SerializeField]
    Color playerPosColor = Color.yellow;

    [SerializeField]
    Color playerSelectColor = Color.green;

    [SerializeField]
    Color characterMoveColor = Color.green;

    [Header("Debugging")]

    [SerializeField]
    Vector2Int playerSelectPos = new Vector2Int(0, 0);

    //Cashe References
    Dictionary<Vector2Int, Tile> gameBoard = new Dictionary<Vector2Int, Tile>();
    Dictionary<Vector2Int, GameObject> characterObjPos = new Dictionary<Vector2Int, GameObject>();
    Tile selectedTile;
    GameObject selectedCharacter;

    //Attributes
    Vector2Int selectedCharacterPos = new Vector2Int();
    bool playerSelectedTile = false;
    bool playerSelectedCharacter = false;



    /*
     * 
     * UNITY EVENTS
     * 
     */

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

    /*
     * 
     * PLAYER INPUT EVENTS
     * 
     */

    //Methods to subscribe to PlayerInputManager through StartingSceneInputManager
    public void PlayerInput_OnBoardMoveUp() { MovePlayerUp(); }
    public void PlayerInput_OnBoardMoveDown() { MovePlayerDown(); }
    public void PlayerInput_OnBoardMoveLeft() { MovePlayerLeft(); }
    public void PlayerInput_OnBoardMoveRight() { MovePlayerRight(); }
    public void PlayerInput_OnBoardSelect() { SelectTileObject(); }
    public void PlayerInput_OnBoardDeselect() { DeselectTileObject(); }



    /*
     * 
     * MOVEMENT LOGIC
     * 
     */

    //Method to move player to the given position
    void MovePlayerToPos(Vector2Int newPos)
    {
        //Set the current tile the player is on back to its tile color only if it's not the selected tile
        if (gameBoard[playerSelectPos] != selectedTile)
        {
            if (gameBoard[playerSelectPos].isDisplayingCharacterMovePosition)
            {
                gameBoard[playerSelectPos].SetTileColor(characterMoveColor);
            }
            else
            {
                gameBoard[playerSelectPos].SetTileColor();
            }
        }


        //get square renderer at new position and update the color if the tile is not selected
        if (gameBoard[newPos].TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer) && 
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



    /*
     * 
     * TILE LOGIC
     * 
     */

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
            if (selectedCharacter != null)
            {
                Debug.Log($"Selected object: {selectedCharacter.name}");
            }
            //Set the tile object color to given player selected color
            if (gameBoard[playerSelectPos].TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
            {
                renderer.color = playerSelectColor;
            }
        }
    }

    //Method to deselect current tile object
    void DeselectTileObject()
    {
        //Check if the object was selected. No need to run if they haven't
        if (!playerSelectedTile) { return; }

        //Player is not selecting an object
        if (selectedCharacter != null)
        {
            HideCharacterMovePositions(selectedCharacter, selectedCharacterPos);
            selectedCharacter = null;
        }
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



    /*
     * 
     * CHARACTER LOGIC
     * 
     */

    //Method for basic character logic
    private void HandleCharacterLogic()
    {
        //If the player selected an object & the selected position doesn't have a character object, move the character to the position
        if (selectedCharacter != null && !characterObjPos.ContainsKey(playerSelectPos) && playerSelectedCharacter == true)
        {
            MoveCharacterObject(selectedCharacter, selectedCharacterPos, playerSelectPos);
            HideCharacterMovePositions(selectedCharacter, selectedCharacterPos);
        }
        //Otherwise, player selected character object on this tile (if there is one)
        else
        {
            if (characterObjPos.ContainsKey(playerSelectPos))
            {
                selectedCharacter = characterObjPos[playerSelectPos];
                playerSelectedCharacter = true;
                selectedCharacterPos = playerSelectPos;

                //Display where the selected character can move to
                DisplayCharacterMovePositions(selectedCharacter, playerSelectPos);
            }
        }
    }

    //Method to add character to board at given position
    public void AddCharacterToBoard(GameObject characterObj, Vector2Int pos)
    {
        //Set chararcter obj to the given position
        characterObj.transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        //update the character's position in the dictionary
        characterObjPos.Add(pos, characterObj);
    }

    //Method to move character on the board
    void MoveCharacterObject(GameObject characterObj, Vector2Int currentPos, Vector2Int newPos)
    {
        //Get the displacement of the new and current position
        Vector2 moveDisplacement = new Vector2(
            newPos.x - currentPos.x,
            newPos.y - currentPos.y
            );

        //Variable to test if character can move to position
        bool canMoveCharacter = false;

        //Check if the character can move to the given position. If they cant, don't move them
        if (characterObj.TryGetComponent<Character>(out Character character))
        {
            //Loop through each position until all movement positions have been explored
            foreach (Vector2 pos in character.GetMovingPositions())
            {
                if (moveDisplacement.x == pos.x && moveDisplacement.y == pos.y)
                {
                    canMoveCharacter = true;
                    break;
                }
            }
        }

        //If player cannot move to position, return
        if (!canMoveCharacter){ return; }


        //Move the charactrer visually
        characterObj.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        //update the character's position in the dictionary
        characterObjPos.Remove(currentPos);
        characterObjPos.Add(newPos, characterObj);
    }

    //Method to show where the selected character can move to
    void DisplayCharacterMovePositions(GameObject givenCharacter, Vector2Int currentPos)
    {
        //Temporary V2Int
        Vector2Int tempV2Int = new Vector2Int();

        //Get the character component
        if (givenCharacter.TryGetComponent<Character>(out Character character)){
            //Loop through each position the character can move to
            foreach (Vector2 pos in character.GetMovingPositions())
            {
                //Get the board position from where the character can move to
                tempV2Int.x = (int)pos.x + currentPos.x;
                tempV2Int.y = (int)pos.y + currentPos.y;

                //Display the tiles with the movement color
                if (gameBoard.ContainsKey(tempV2Int))
                {
                    gameBoard[tempV2Int].SetTileColor(characterMoveColor);
                    gameBoard[tempV2Int].isDisplayingCharacterMovePosition = true;
                }
            }
        }
    }

    //Method to hide where the selected character can move to
    void HideCharacterMovePositions(GameObject givenCharacter, Vector2Int currentPos)
    {
        //Temporary V2Int
        Vector2Int tempV2Int = new Vector2Int();

        //Get the character component
        if (givenCharacter.TryGetComponent<Character>(out Character character))
        {
            //Loop through each position the character can move to
            foreach (Vector2 pos in character.GetMovingPositions())
            {
                //Get the board position from where the character can move to
                tempV2Int.x = (int)pos.x + currentPos.x;
                tempV2Int.y = (int)pos.y + currentPos.y;

                //Display the tiles with the movement color
                if (gameBoard.ContainsKey(tempV2Int))
                {
                    //Set tile color to default
                    gameBoard[tempV2Int].SetTileColor();
                    gameBoard[tempV2Int].isDisplayingCharacterMovePosition = false;
                }
            }
        }
    }
}
