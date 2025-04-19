using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    //Canvas manager singleton
    public static CanvasManager Instance { get; private set; }

    List<GameObject> canvasesInScene = new List<GameObject>();

    [SerializeField]
    GameObject activeCanvas = null;


    InitializationCheck initializationCheck;

    private void Awake()
    {
        initializationCheck = GetComponent<InitializationCheck>();

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

        //Experimental code. IDK if this will work as wanted
        SceneManager.sceneUnloaded += ClearCanvasesInScene;
    }

    private void Start()
    {
        
        //Canvas Manager is initialized
        initializationCheck.SetObjectToInitialized();
        //Reference is no longer needed
        initializationCheck = null;
    }

    /*
     * 
     * CANVAS LOGIC
     * 
     */

    private void ClearCanvasesInScene(Scene arg0)
    {
        canvasesInScene.Clear();
    }
    public void AddCanvasToScene(GameObject canvas)
    {
        canvasesInScene.Add(canvas);
        canvas.SetActive(false);
    }
    public void SetActiveCanvas(GameObject canvas)
    {
        if (canvasesInScene.Contains(canvas))
        {
            RemoveActiveCanvas();

            //Set new active canvas
            activeCanvas = canvas;
            activeCanvas.SetActive(true);
            InitializeActiveCanvasListeners();
        }
    }
    private void InitializeActiveCanvasListeners()
    {
        CanvasNavigator canvasNavigator =  activeCanvas.GetComponent<CanvasNavigator>();
        if (canvasNavigator != null)
        {
            PlayerInputManager.EOnUIUp += canvasNavigator.PlayerInput_OnUIUp;
            PlayerInputManager.EOnUIDown += canvasNavigator.PlayerInput_OnUIDown;
            PlayerInputManager.EOnUILeft += canvasNavigator.PlayerInput_OnUILeft;
            PlayerInputManager.EOnUIRight += canvasNavigator.PlayerInput_OnUIRight;
            PlayerInputManager.EOnUISelect += canvasNavigator.PlayerInput_OnUISelect;
            PlayerInputManager.EOnUIDeselect += canvasNavigator.PlayerInput_OnUIDeselect;
        }
    }
    public void RemoveActiveCanvas()
    {
        if (activeCanvas == null) { return; }
        CanvasNavigator canvasNavigator = activeCanvas.GetComponent<CanvasNavigator>();

        if (canvasNavigator != null)
        {
            PlayerInputManager.EOnUIUp -= canvasNavigator.PlayerInput_OnUIUp;
            PlayerInputManager.EOnUIDown -= canvasNavigator.PlayerInput_OnUIDown;
            PlayerInputManager.EOnUILeft -= canvasNavigator.PlayerInput_OnUILeft;
            PlayerInputManager.EOnUIRight -= canvasNavigator.PlayerInput_OnUIRight;
            PlayerInputManager.EOnUISelect -= canvasNavigator.PlayerInput_OnUISelect;
            PlayerInputManager.EOnUIDeselect -= canvasNavigator.PlayerInput_OnUIDeselect;
        }

        activeCanvas.SetActive(false);
    }
}
