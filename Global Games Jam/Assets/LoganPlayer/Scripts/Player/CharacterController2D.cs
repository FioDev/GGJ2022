using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [Range(1, 20)] [SerializeField] private float jumpVelocity;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private BoxCollider2D groundCheck; //Ground check collider

    Transform m_GroundCheck;    // A position marking where to check if the player is grounded.

    public bool m_Grounded;            // Whether or not the player is grounded.

    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 3f;
    private bool jumpHeld = false;

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    public int ID = 0;

    public UnityEvent OnLandEvent;

    public class BoolEvent : UnityEvent<bool> { }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GroundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        //Takes a box collider and checks if it collides with the ground
        RaycastHit2D isGround = Physics2D.BoxCast(groundCheck.bounds.center, groundCheck.bounds.size - new Vector3(0.2f, 0f, 0f), 0f, Vector2.down, 0.1f, m_WhatIsGround);
        if (isGround)
        {
            m_Grounded = true;
        }
    }

    public void Move(float move, bool jump, bool dive, float runSpeed)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            m_Rigidbody2D.gravityScale = 2;

            if (m_Grounded)
            {
                if (!jump)
                {
                    jumpHeld = false;
                }

                // If the player should jump...
                if (jump && !jumpHeld)
                {
                    jumpHeld = true;

                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Rigidbody2D.velocity += Vector2.up * jumpVelocity;

                }


            }

            if (!m_Grounded)
            {


                //Make falling smoother
                if (m_Rigidbody2D.velocity.y < 0)
                {
                    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                else if (m_Rigidbody2D.velocity.y > 0 && !jump)
                {
                    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

                if (dive)
                {
                    m_Rigidbody2D.gravityScale *= 10; //Increase gravity until player lands 
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}