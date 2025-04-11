using System;
using UnityEngine;

public class StartingSceneInputManager : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager.InputMode StartingInputMode;

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

    //Method to initialize input for UI mode
    void InitializeUIMode()
    {
        PlayerInputManager.Instance.SetInputToUI();

        //Get UI canvas in scene & connect controls
    }

    //Method to initialize input for world mode
    void InitializeWorldMode()
    {
        PlayerInputManager.Instance.SetInputToWorld();

        //Get player in the world & connect controls
        PlayerControls player = GameObject.FindFirstObjectByType<PlayerControls>();

        if (player != null)
        {
            //Don't forget to Like, Comment, & SUBSCRIBE!
            PlayerInputManager.EOnWorldMove += player.PlayerInput_OnWorldMove;
        }
    }

    //Method to initialize input for board mode
    void InitializeBoardMode()
    {
        PlayerInputManager.Instance.SetInputToBoard();

        //Get board in scene & connect controls
        GameBoard board = GameObject.FindFirstObjectByType<GameBoard>();

        if (board != null)
        {
            //SUBSCRIBE to PewDiePie :)
            PlayerInputManager.EOnBoardMoveUp += board.PlayerInput_OnBoardMoveUp;
            PlayerInputManager.EOnBoardMoveDown += board.PlayerInput_OnBoardMoveDown;
            PlayerInputManager.EOnBoardMoveLeft += board.PlayerInput_OnBoardMoveLeft;
            PlayerInputManager.EOnBoardMoveRight += board.PlayerInput_OnBoardMoveRight;
            PlayerInputManager.EOnBoardSelect += board.PlayerInput_OnBoardSelect;
            PlayerInputManager.EOnBoardDeselect += board.PlayerInput_OnBoardDeselect;
        }
    }
}
