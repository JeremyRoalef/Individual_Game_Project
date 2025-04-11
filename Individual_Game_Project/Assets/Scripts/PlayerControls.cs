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



    /*
     * 
     * UNITY EVENTS
     * 
     */

    private void Start()
    {
        SetRigidbodyProperties();
    }

    private void FixedUpdate()
    {
        //Apply player movement
        rb.linearVelocity = playerMoveDir * moveSpd * Time.deltaTime;
    }



    /*
     * 
     * Other Methods
     * 
     */

    //Method to initialize rigidbody
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

    //Method to listen to event. Set up through StartingSceneInputManager
    public void PlayerInput_OnWorldMove(Vector2 moveDir)
    {
        playerMoveDir = moveDir;
    }
}
