using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*
 This script will be responsible for all of the player's input for world and UI navigation
 */

public class PlayerInputManager : MonoBehaviour
{
    //Enumerator for different input mode states
    public enum InputMode
    {
        World,
        Board,
        UI
    }

    //SerializedFields
    [SerializeField]
    InputActionAsset inputActionAsset;


    [Header("Debugging || Dev Tools")]

    [SerializeField] [Tooltip("Change action map to use UI input")]
    bool useUIActionMap = false;

    [SerializeField] [Tooltip("Change action map to use board input")]
    bool useBoardActionMap = false;
    
    [SerializeField] [Tooltip("Change action map to use world input")]
    bool useWorldActionMap = false;

    [SerializeField] [Tooltip("Enter the name of the scene you want to enter")]
    string loadSceneName;

    [SerializeField] [Tooltip("Enter the scene name above before switching the scene")]
    bool switchScene = false;
    
    //Delegates (Each delegate will control one of the PlayerInput events for easy game object hookup)

    //World delegates
    public static event Action<Vector2> EOnWorldMove;


    //Board delegates
    public static event Action EOnBoardMoveUp;
    public static event Action EOnBoardMoveDown;
    public static event Action EOnBoardMoveLeft;
    public static event Action EOnBoardMoveRight;
    public static event Action EOnBoardSelect;
    public static event Action EOnBoardDeselect;

    //UI delegates
    public static event Action EOnUIUp;
    public static event Action EOnUIDown;
    public static event Action EOnUILeft;
    public static event Action EOnUIRight;
    public static event Action EOnUISelect;
    public static event Action EOnUIDeselect;

    //Cashe References
    PlayerInput playerInput;

    //Attributes
    InputMode currentInputMode;

    const string UI_ACTION_MAP = "UI";
    const string BOARD_ACTION_MAP = "PlayerBoard";
    const string WORLD_ACTION_MAP = "PlayerWorld";

    //Singleton instance
    public static PlayerInputManager Instance { get; private set; }

    //Temporary attributes (will change later as things are automated)


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
        {
            playerInput = transform.AddComponent<PlayerInput>();
            playerInput.actions = inputActionAsset;
            playerInput.defaultActionMap = UI_ACTION_MAP;
        }

        //Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

        if (switchScene)
        {
            switchScene = false;
            SceneManager.LoadScene(loadSceneName);
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
        currentInputMode = InputMode.UI;
    }

    //Method ot set input action map to board action map
    public void SetInputToBoard()
    {
        playerInput.SwitchCurrentActionMap(BOARD_ACTION_MAP);
        currentInputMode = InputMode.Board;
    }

    //Method to set input action map to world action map
    public void SetInputToWorld()
    {
        playerInput.SwitchCurrentActionMap(WORLD_ACTION_MAP);
        currentInputMode = InputMode.World;
    }

    /*
     * General setup for connecting PlayerInputManager events to code:
     *
     * 1) Add PlayerInputManager component to game object
     * 2) Connect it to the right action map
     * 3) in code: void [EnterActionEvent](InputValue value){}
     *    the action event name is under the PlayerInputManager component, where you see event names like 
     *    OnDeviceLost, OnDeviceRegained, etc.
     */

    /*
     * 
     * EVENTS FOR WORLD INPUT
     * 
     */

    public void OnWorldMove(InputValue value)
    {
        EOnWorldMove?.Invoke(value.Get<Vector2>());
    }



    /*
     * 
     * EVENTS FOR BOARD INPUT
     * 
     */

    public void OnBoardMoveUp(InputValue value)
    {
        EOnBoardMoveUp.Invoke();
    }
    public void OnBoardMoveDown(InputValue value) 
    {
        EOnBoardMoveDown?.Invoke();
    }
    public void OnBoardMoveLeft(InputValue value) 
    {
        EOnBoardMoveLeft?.Invoke();
    }
    public void OnBoardMoveRight(InputValue value) 
    {
        EOnBoardMoveRight?.Invoke();
    }
    public void OnBoardSelect(InputValue value)
    {
        EOnBoardSelect?.Invoke();
    }
    public void OnBoardDeselect(InputValue value)
    {
        EOnBoardDeselect?.Invoke();
    }

    /*
     * 
     * EVENTS FOR UI INPUT
     * 
     */

    public void OnUIUp(InputValue value)
    {
        EOnUIUp?.Invoke();
    }
    public void OnUIDown(InputValue value)
    {
        EOnUIDown?.Invoke();
    }
    public void OnUILeft(InputValue value)
    {
        EOnUILeft?.Invoke();
    }
    public void OnUIRight(InputValue value)
    {
        EOnUIRight?.Invoke();
    }
    public void OnUISelect(InputValue value)
    {
        EOnUISelect?.Invoke();
    }
    public void OnUIDeselect(InputValue value)
    {
        EOnUIDeselect?.Invoke();
    }
}
