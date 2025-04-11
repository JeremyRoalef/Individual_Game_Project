using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSceneInputManager : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager.InputMode StartingInputMode;

    GameBoard gameBoard;
    PlayerControls playerControls;


    private void Awake()
    {
        //Setup scene if managers are broken
        if (PlayerInputManager.Instance == null)
        {
            SimpleDevTools.InitializeManagers(SceneManager.GetActiveScene());
        }
    }

    private void Start()
    {
        switch (StartingInputMode)
        {
            case PlayerInputManager.InputMode.UI:
                InitializeUIMode();
                break;
            case PlayerInputManager.InputMode.World:
                InitializeWorldMode();
                break;
            case PlayerInputManager.InputMode.Board:
                InitializeBoardMode();
                break;
            default:
                //Initialize UI by defualt
                InitializeUIMode();
                break;
        }
    }

    /*
     * 
     * UI INITIALIZATION
     * 
     */
    
    void InitializeUIMode()
    {
        PlayerInputManager.Instance.SetInputToUI();

        //Get UI canvas in scene & connect controls
    }



    /*
     * 
     * WORLD INITIALIZATION
     * 
     */

    void InitializeWorldMode()
    {
        PlayerInputManager.Instance.SetInputToWorld();

        //Get playerControls in the world & connect controls
        playerControls = GameObject.FindFirstObjectByType<PlayerControls>();

        if (playerControls != null)
        {
            //Don't forget to Like, Comment, & SUBSCRIBE!
            AddPlayerListeners();
            //Unsubscribe. Channel isn't cool anymore
            SceneManager.sceneUnloaded += SceneManage_RemovePlayerListeners;
        }
    }
    private void AddPlayerListeners()
    {
        PlayerInputManager.EOnWorldMove += playerControls.PlayerInput_OnWorldMove;
    }
    private void RemovePlayerListeners()
    {
        PlayerInputManager.EOnWorldMove -= playerControls.PlayerInput_OnWorldMove;
    }
    private void SceneManage_RemovePlayerListeners(Scene arg0)
    {
        RemovePlayerListeners();
    }



    /*
     * 
     * BOARD INITIALIZATION
     * 
     */

    void InitializeBoardMode()
    {
        PlayerInputManager.Instance.SetInputToBoard();

        //Get gameBoard in scene & connect controls
        gameBoard = GameObject.FindFirstObjectByType<GameBoard>();

        if (gameBoard != null)
        {
            //SUBSCRIBE to PewDiePie :)
            AddBoardListeners();
            //Unsubscribe :/
            SceneManager.sceneUnloaded += SceneManager_RemoveBoardListeners;
        }
    }
    private void AddBoardListeners()
    {
        PlayerInputManager.EOnBoardMoveUp += gameBoard.PlayerInput_OnBoardMoveUp;
        PlayerInputManager.EOnBoardMoveDown += gameBoard.PlayerInput_OnBoardMoveDown;
        PlayerInputManager.EOnBoardMoveLeft += gameBoard.PlayerInput_OnBoardMoveLeft;
        PlayerInputManager.EOnBoardMoveRight += gameBoard.PlayerInput_OnBoardMoveRight;
        PlayerInputManager.EOnBoardSelect += gameBoard.PlayerInput_OnBoardSelect;
        PlayerInputManager.EOnBoardDeselect += gameBoard.PlayerInput_OnBoardDeselect;
    }
    private void RemoveBoardListeners()
    {
        Debug.Log("Removing Board listeners");

        PlayerInputManager.EOnBoardMoveUp -= gameBoard.PlayerInput_OnBoardMoveUp;
        PlayerInputManager.EOnBoardMoveDown -= gameBoard.PlayerInput_OnBoardMoveDown;
        PlayerInputManager.EOnBoardMoveLeft -= gameBoard.PlayerInput_OnBoardMoveLeft;
        PlayerInputManager.EOnBoardMoveRight -= gameBoard.PlayerInput_OnBoardMoveRight;
        PlayerInputManager.EOnBoardSelect -= gameBoard.PlayerInput_OnBoardSelect;
        PlayerInputManager.EOnBoardDeselect -= gameBoard.PlayerInput_OnBoardDeselect;
    }
    private void SceneManager_RemoveBoardListeners(Scene arg0)
    {
        RemoveBoardListeners();
    }
}
