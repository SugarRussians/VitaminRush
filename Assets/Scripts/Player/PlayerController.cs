using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Controller")]
    public CharacterController characterController;

    [Header("Player Variables")]
    public float MovementSpeed;
    public float SprintMovementSpeed;
    public float gravity;
    public float JumpHeight;
    public float CrouchHeight;

    [Header("Camera")]
    public GameObject MainCamera;

    [Header("Button Layout")]
    public KeyCode CrouchKey;
    public KeyCode SprintKey;
    public KeyCode ReloadKey;
    public int ShootKey;

    [Header("Grounded Check")]
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    private BaseGun _gun;

    private Vector3 _velocity;
    private bool _isGrounded;

    private Rigidbody _rigidbody;

    private float _defaultPlayerHeight;
    private float _defaultMovementSpeed;

    private void Awake()
    {
        //_rigidbody = GetComponent<Rigidbody>();

        _defaultPlayerHeight = transform.localScale.y;
        _defaultMovementSpeed = MovementSpeed;

        _gun = GetComponentInChildren<BaseGun>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        CheckMovementInput();

        CheckJump();

        CheckCrouch();

        CheckSprint();

        CheckShoot();

        CheckReload();
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = gravity;
        }
    }

    private void CheckMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * MovementSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;

        characterController.Move(_velocity * Time.deltaTime);
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    // TODO change this, move the camera position when there are meshes
    private void CheckCrouch()
    {
        if (Input.GetKeyDown(CrouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, CrouchHeight, transform.localScale.z);
        }
        else if (Input.GetKeyUp(CrouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, _defaultPlayerHeight, transform.localScale.z);
        }
    }

    private void CheckSprint()
    {
        if (Input.GetKeyDown(SprintKey))
        {
            MovementSpeed = SprintMovementSpeed;
        }
        else if (Input.GetKeyUp(SprintKey))
        {
            MovementSpeed = _defaultMovementSpeed;
        }
    }

    private void CheckShoot()
    {
        if (Input.GetMouseButton(ShootKey) && _gun)
        {
            _gun.Shoot();
        }
    }

    private void CheckReload()
    {
        if (_gun && _gun.CurrentAmmo < 1)
        {
            _gun.Reload();
        }
        else if (Input.GetKeyDown(ReloadKey) && _gun)
        {
            _gun.Reload();
        }
    }
}
