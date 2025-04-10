using Unity.VisualScripting;
using UnityEngine;

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

    void Update()
    {
        //Get player movement input
        playerMoveDir.x = Input.GetAxisRaw("Horizontal");
        playerMoveDir.y = Input.GetAxisRaw("Vertical");
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
}
