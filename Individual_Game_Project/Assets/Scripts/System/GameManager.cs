using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private event Action OnObjectInitialized;
    private event Action OnGameInitialized;

    [Header("Management Properties")]

    [SerializeField] [Tooltip("The game objects in the initialization scene that must be initialized before the game can start")]
    InitializationCheck[] initializationChecks;

    [Header("Debugging")]

    [SerializeField] [Tooltip("Checks if the game is ready to load into main menu")]
    bool gameIsReady = false;


    const string MAIN_MENU_SCENE_STRING = "MainMenu";

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        //Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        initializationChecks = FindObjectsByType<InitializationCheck>(FindObjectsSortMode.None);

        //Subscription Events
        OnObjectInitialized += CheckIfGameIsReady;
        OnGameInitialized += RemoveInitializationMemory;
        OnGameInitialized += StartGame;
    }

    private void RemoveInitializationMemory()
    {
        //Clean out unnecessary memory
        foreach (InitializationCheck check in initializationChecks)
        {
            Destroy(check);
        }

        initializationChecks = null;
    }

    void CheckIfGameIsReady()
    {
        int numOfIncompleteInitializations = initializationChecks.Length;

        foreach (InitializationCheck initCheck in initializationChecks)
        {
            if (initCheck.isInitialized)
            {
                numOfIncompleteInitializations--;
            }
        }

        if (numOfIncompleteInitializations <= 0)
        {
            gameIsReady = true;
            OnGameInitialized?.Invoke();
        }
    }

    public void UpdateInitizalizedObjects()
    {
        OnObjectInitialized?.Invoke();
    }

    void StartGame()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_STRING);
    }
}
