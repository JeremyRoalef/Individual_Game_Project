using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    //Serialized Fields
    [Header("Movement Attributes")]
    
    [SerializeField] [Min(0)]
    float moveSpd = 10f;


    //Cashe References
    Rigidbody2D rb;


    //Attributes
    Vector2 playerMoveDir;


    private void Start()
    {
        SetRigidbodyProperties();
    }

    private void FixedUpdate()
    {
        //Apply player movement
        rb.linearVelocity = playerMoveDir * moveSpd * Time.deltaTime;
    }

    private void SetRigidbodyProperties()
    {
        //Check if there's a rb2d component
        rb = GetComponent<Rigidbody2D>();

        //If no rb, add component
        if (rb == null)
        {
            rb = transform.AddComponent<Rigidbody2D>();
        }

        //Rb2d necessary values
        rb.gravityScale = 0f;
    }


    /*
     General setup for connecting PlayerInputManager events to code:

    1) Add PlayerInputManager component to game object
    2) Connect it to the right action map
    3) in code: out void [EnterActionEvent](InputValue value){}
       the action event name is under the PlayerInputManager component, where you see event names like 
       OnDeviceLost, OnDeviceRegained, etc.
     */

    //This method is linked to the PlayerInputManager component attached to the Player game object, though
    //my Intellisense is not bright enough to autofill the information for me :/
    public void OnMove(InputValue value)
    {
        playerMoveDir = value.Get<Vector2>();
    }
}
