using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    UnityEvent OnHoverEnter;
    [SerializeField]
    UnityEvent OnHoverExit;

    //Interface event handlers
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetButtonToHovered();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetButtonToUnhovered();
    }


    //Button hover states
    private void SetButtonToHovered()
    {
        Debug.Log("Player entered button");
        OnHoverEnter?.Invoke();
    }
    private void SetButtonToUnhovered()
    {
        Debug.Log("Player exited button");
        OnHoverExit?.Invoke();
    }

    //Methods to listen to when button is hovered
    public void UnityEvent_OnHoverEnter()
    {
        SetButtonToHovered();
    }
    public void UnityEvent_OnHoverExit()
    {
        SetButtonToUnhovered();
    }

    public void Select()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick?.Invoke();
        }
    }
}
