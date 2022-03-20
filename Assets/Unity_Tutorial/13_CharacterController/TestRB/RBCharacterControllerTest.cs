using UnityEngine;

public class RBCharacterControllerTest : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpHeight = 3f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Transform _transform;
    private Vector3 _moveDirection;
    private RaycastHit _slopeHit;

    private void Awake()
    {
        _transform = transform;

        _collider = GetComponent<CapsuleCollider>();

        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore);

        _Inputs();
        _ControlDrag();
        _ControlSpeed();
        _Jump();
    }

	private void FixedUpdate()
	{
		// Move player only in FixedUpdate to deal with physics.
		Vector3 moveDir = _moveDirection;
		if (isGrounded)
		{
			if (_OnSlope())
			{
				moveDir = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);
			}
			_rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier + Physics.gravity * movementMultiplier, ForceMode.Acceleration);
		}
		else
		{
			_rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier * airMultiplier + Physics.gravity * movementMultiplier, ForceMode.Acceleration);
		}
	}

	private bool _OnSlope()
    {
        if (Physics.Raycast(_transform.position, Vector3.down, out _slopeHit, _collider.height * 0.5f + 0.5f))
        {
            return _slopeHit.normal != Vector3.up;
        }
        return false;
    }

    private void _Inputs()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        _moveDirection = _transform.right * xMove + _transform.forward * zMove;
    }

    private void _Jump()
    {
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            // reset vertical velocity (keep ground velocity only).
            // _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); 
            // Kinematic equation : Vf² = Vi² + 2*a*h
            // When max height (h) is reached : Vf is 0
            // => 0 = Vi² + 2*a*h
            // => Vi² = -2*a*h
            // => Vi = sqrt(-2*a*h)
            float verticalSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * movementMultiplier * jumpHeight);
            // Apply jump force without taking of mass, because we want the character to be able to reach jumpHeight.
            //_rb.AddForce(transform.up * verticalSpeed, ForceMode.VelocityChange);

            _rb.velocity = new Vector3(_rb.velocity.x, verticalSpeed, _rb.velocity.z);
        }
    }

    private void _ControlSpeed()
    {
        if (isGrounded && Input.GetKey(sprintKey))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void _ControlDrag()
    {
        if (isGrounded)
        {
            _rb.drag = groundDrag;
        }
        else
        {
            _rb.drag = airDrag;
        }
    }
}
