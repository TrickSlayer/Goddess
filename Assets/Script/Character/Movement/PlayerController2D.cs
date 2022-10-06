using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
	[SerializeField] private float JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private bool AirControl = false;                           // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheckPoint;						// A position marking where to check if the player is grounded.
	[SerializeField] private Transform CeilingCheckPoint;                       // A position marking where to check for ceilings
    [SerializeField] private Collider2D CrouchDisableCollider;                // A collider that will be disabled when crouching

    public float GroundedRadius = .2f;                                          // Radius of the overlap circle to determine if grounded
    public float CeilingRadius = 0.6f;                                          // Radius of the overlap circle to determine if the player can stand up
    [HideInInspector] public bool Grounded;                                     // Whether or not the player is grounded.
    [HideInInspector] public bool Ceiling;
                                       
	private Rigidbody2D Rigidbody2D;
	private bool facingRight = true;                                            // For determining which way the player is currently facing.
	private Vector3 Velocity = Vector3.zero;
    private bool wasCrouching = false;

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;

    private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

	private void FixedUpdate()
	{
		bool wasGrounded = Grounded;
		Grounded = false;

		// The player is grounded if a circlecast to the GroundCheckPoint position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

        Ceiling = Physics2D.OverlapCircle(CeilingCheckPoint.position, CeilingRadius, WhatIsGround);
    }


	public void Move(float move, bool crouch, bool jump)
	{

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

		RotationFace(move);

        Crouch(crouch);

		Jump(jump);
    }

    #region RotationFace
    private void RotationFace(float move)
	{
        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    #endregion

    #region Jump
    private void Jump(bool jump)
	{
        // If the player should jump...
        if (Grounded && jump)
        {
            // Add a vertical force to the player.
            Grounded = true;
            Vector2 force = new Vector2(0f, JumpForce);
            Rigidbody2D.AddForce(force);
        }
    }
    #endregion

    #region Crouch
    private void Crouch(bool crouch)
    {

        // If crouching, check to see if the character can stand up
        if (!crouch && wasCrouching)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(CeilingCheckPoint.position, CeilingRadius, WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (Grounded || AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Disable one of the colliders when crouching
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = false;

            }
            else
            {
                // Enable the collider when not crouching
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = true;

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

        }

    }
    #endregion

}
