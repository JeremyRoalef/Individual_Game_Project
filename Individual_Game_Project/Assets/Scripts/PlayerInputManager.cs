using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 This script will be responsible for all of the player's input for world and UI navigation
 */

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActionAsset;

    PlayerInput playerInput;

    const string UI_ACTION_MAP = "UI";
    const string BOARD_ACTION_MAP = "PlayerBoard";
    const string WORLD_ACTION_MAP = "PlayerWorld";


    //Temporary attributes (will change later as things are automated)
    Vector2 playerMoveDir;

    [SerializeField]
    bool useUIActionMap = false;
    [SerializeField]
    bool useBoardActionMap = false;
    [SerializeField]
    bool useWorldActionMap = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
        {
            playerInput = transform.AddComponent<PlayerInput>();
            playerInput.actions = inputActionAsset;
            playerInput.defaultActionMap = UI_ACTION_MAP;
        }
    }

    private void Update()
    {
        if (useUIActionMap)
        {
            useUIActionMap = false;
            SetInputToUI();
        }
        if (useBoardActionMap)
        {
            useBoardActionMap= false;
            SetInputToBoard();
        }
        if (useWorldActionMap)
        {
            useWorldActionMap= false;
            SetInputToWorld();
        }
    }

    /*
     * 
     * METHODS TO SWITCH BETWEEN INPUT ACTION MAPS
     * 
     */

    //Method to set input action map to given input action map. Should not need this method, but just in case
    public void SetInputActionMap(string actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap);
    }

    //Method to set input action map to UI action map
    public void SetInputToUI()
    {
        playerInput.SwitchCurrentActionMap(UI_ACTION_MAP);
    }

    //Method ot set input action map to board action map
    public void SetInputToBoard()
    {
        playerInput.SwitchCurrentActionMap(BOARD_ACTION_MAP);
    }

    //Method to set input action map to world action map
    public void SetInputToWorld()
    {
        playerInput.SwitchCurrentActionMap(WORLD_ACTION_MAP);
    }

    /*
     * 
     * EVENTS FOR WORLD INPUT
     * 
     */

    public void OnWorldMove(InputValue value)
    {
        playerMoveDir = value.Get<Vector2>();

        Debug.Log("World move");
    }



    /*
     * 
     * EVENTS FOR BOARD INPUT
     * 
     */

    public void OnBoardMoveUp(InputValue value)
    {
        Debug.Log("move up");
        //MovePlayerUp(); 
    }
    public void OnBoardMoveDown(InputValue value) 
    {
        Debug.Log("move down");
        //MovePlayerDown(); 
    }
    public void OnBoardMoveLeft(InputValue value) 
    {
        Debug.Log("move left");
        //MovePlayerLeft(); 
    }
    public void OnBoardMoveRight(InputValue value) 
    {
        Debug.Log("move right");
        //MovePlayerRight();
    }
    public void OnBoardSelect(InputValue value)
    {
        Debug.Log("select");
        //SelectTileObject();
    }
    public void OnBoardDeselect(InputValue value)
    {
        Debug.Log("deselect");
        //DeselectTileObject();
    }

    /*
     * 
     * EVENTS FOR UI INPUT
     * 
     */

    public void OnUIUp(InputValue value)
    {
        Debug.Log("UI up");
        //MovePlayerUp();
    }
    public void OnUIDown(InputValue value)
    {
        Debug.Log("UI down");
        //MovePlayerDown();
    }
    public void OnUILeft(InputValue value)
    {
        Debug.Log("UI left");
        //MovePlayerLeft();
    }
    public void OnUIRight(InputValue value)
    {
        Debug.Log("UI right");
        //MovePlayerRight();
    }
    public void OnUISelect(InputValue value)
    {
        Debug.Log("UI select");
    }
    public void OnUIDeselect(InputValue value)
    {
        Debug.Log("UI deselect");
    }
}
