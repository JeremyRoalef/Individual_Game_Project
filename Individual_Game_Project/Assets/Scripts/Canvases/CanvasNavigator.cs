using System;
using UnityEngine;
using UnityEngine.Events;

public class CanvasNavigator : MonoBehaviour
{
    [Tooltip("The layout of the buttons in the canvas")]
    public enum ButtonLayout
    {
        Vertical,
        Horizontal,
        Grid
    }

    //Serialized Fields
    [Header("Canvas Buttons")]

    [SerializeField]
    [Tooltip("Enter the buttons in the canvas in order of top button to bottom button," +
        "or, if canvas is horizontal, enter from left to right." +
        "If the canvas contains buttons in a grid, enter buttons from left to right, top to bottom")]
    CustomUIButton[] buttons;

    [SerializeField]
    [Tooltip("How are the buttons on the canvas placed?")]
    ButtonLayout buttonLayout;

    [SerializeField]
    [Tooltip("DO NOT TOUCH THIS!!! DEBUGGING PURPOSES ONLY")]
    GameObject currentActiveButton;

    [Header("Canvas Navigation Events")]

    //Serialized Fields
    [SerializeField]
    [Tooltip("General Movevement logic (i.e,. Play audio clip sound)")]
    UnityEvent OnMove;

    [SerializeField]
    [Tooltip("General Selection logic (i.e,. Player audio clip sound)")]
    UnityEvent OnSelect;

    [SerializeField]
    [Tooltip("General Deselection logic (i.e,. Player audio clip sound)")]
    UnityEvent OnDeselect;


    //Attributes
    int currentButtonIndex;



    /*
     * 
     * UNITY EVENTS
     * 
     */

    private void Start()
    {
        OnMove.AddListener(DebugActiveButton);


        if (buttons != null)
        {
            currentButtonIndex = 0;
            currentActiveButton = buttons[0].gameObject;
        }
        else
        {
            Debug.Log("No buttons on canvas. Cannot navigate. Disabling canvas");
            gameObject.SetActive(false);
        }
    }



    /*
     * 
     * PLAYER INPUT
     * 
     */

    public void PlayerInput_OnUIUp()
    {
        Debug.Log("up");

        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                MoveUp();
                break;
            case ButtonLayout.Horizontal:
                break;
            case ButtonLayout.Grid:
                MoveUp();
                break;
        }

        OnMove?.Invoke();
    }
    public void PlayerInput_OnUIDown()
    {
        Debug.Log("Down");


        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                MoveDown();
                break;
            case ButtonLayout.Horizontal:
                break;
            case ButtonLayout.Grid:
                MoveDown();
                break;
        }

        OnMove?.Invoke();
    }
    public void PlayerInput_OnUILeft()
    {
        Debug.Log("Left");
        OnMove?.Invoke();

        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                break;
            case ButtonLayout.Horizontal:
                MoveLeft();
                break;
            case ButtonLayout.Grid:
                MoveLeft();
                break;
        }

        OnMove?.Invoke();
    }
    public void PlayerInput_OnUIRight()
    {
        Debug.Log("Right");
        OnMove?.Invoke();

        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                break;
            case ButtonLayout.Horizontal:
                MoveRight();
                break;
            case ButtonLayout.Grid:
                MoveRight();
                break;
        }

        OnMove?.Invoke();
    }
    public void PlayerInput_OnUISelect()
    {
        OnSelect?.Invoke();
        SelectButton();
    }
    public void PlayerInput_OnUIDeselect()
    {
        OnDeselect?.Invoke();
        DeselectButton();
    }



    /*
     * 
     * CANVAS NAVIGATION LOGIC
     * 
     */
    private void DebugActiveButton()
    {
        currentActiveButton = buttons[currentButtonIndex].gameObject;
    }
    bool MoveUp()
    {
        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                if (currentButtonIndex - 1 < 0)
                {
                    return false;
                }
                currentButtonIndex -= 1;
                break;
            case ButtonLayout.Grid:
                break;
            default:
                return false;
        }

        return true;
    }
    bool MoveDown()
    {
        switch (buttonLayout)
        {
            case ButtonLayout.Vertical:
                if (currentButtonIndex >= buttons.Length - 1)
                {
                    return false;
                }
                currentButtonIndex += 1;
                break;
            case ButtonLayout.Grid:
                break;
            default:
                return false;
        }

        return true;
    }
    bool MoveLeft()
    {
        switch (buttonLayout)
        {
            case ButtonLayout.Horizontal:
                if (currentButtonIndex <= 0)
                {
                    return false;
                }
                currentButtonIndex -= 1;
                break;
            case ButtonLayout.Grid:
                break;
            default:
                return false;
        }

        return true;
    }
    bool MoveRight()
    {
        switch (buttonLayout)
        {
            case ButtonLayout.Horizontal:
                if (currentButtonIndex >= buttons.Length - 1)
                {
                    return false;
                }
                currentButtonIndex += 1;
                break;
            case ButtonLayout.Grid:
                break;
            default:
                return false;
        }

        return true;
    }
    private void SelectButton()
    {
        Debug.Log($"Select {buttons[currentButtonIndex].name}");

        buttons[currentButtonIndex].Select();
    }
    private void DeselectButton()
    {
        Debug.Log($"Deselecting {buttons[currentButtonIndex].name}");
    }
}
