using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntity
{
    [Header("Player Variables")]
    public float MovementSpeed;
    public float SprintMovementSpeed;
    public float CrouchMovementSpeed;
    public float ADSMovementSpeed;
    public float JumpHeight;
    public float CrouchHeight;

    [Header("Camera")]
    public GameObject MainCamera;

    [Header("Button Layout, Left click = 0, Right click = 1, Middle Mouse click = 2")]
    public KeyCode CrouchKey;
    public KeyCode SprintKey;
    public KeyCode ReloadKey;
    public int ShootKey;
    public int ADSKey;

    [Header("All Guns & Keycodes for weapon slots")]
    public List<BaseGun> Guns;
    public List<KeyCode> WeaponSlotKeycodes;

    private BaseGun _currentGun;

    private Rigidbody _rigidbody;

    private bool _isCrouching;
    private bool _isShooting;
    private bool _isADS;

    private float _defaultPlayerHeight;
    private float _defaultMovementSpeed;

    private void Awake()
    {
        InitializeCharacterController();
        //_rigidbody = GetComponent<Rigidbody>();

        if (Guns.Count > 0)
        {
            _currentGun = Guns[0];
            _currentGun.gameObject.SetActive(true);
        }

        _defaultPlayerHeight = transform.localScale.y;
        _defaultMovementSpeed = MovementSpeed;
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

        CheckADS();

        CheckReload();

        CheckChangeWeapon();
    }

    private void CheckMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * MovementSpeed * Time.deltaTime);
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            EntityVelocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    // TODO change this, move the camera position when there are meshes
    private void CheckCrouch()
    {
        if (Input.GetKeyDown(CrouchKey))
        {
            _isCrouching = true;
            MovementSpeed = CrouchMovementSpeed;
            transform.localScale = new Vector3(transform.localScale.x, CrouchHeight, transform.localScale.z);
        }
        else if (Input.GetKeyUp(CrouchKey))
        {
            _isCrouching = false;
            MovementSpeed = _defaultMovementSpeed;
            transform.localScale = new Vector3(transform.localScale.x, _defaultPlayerHeight, transform.localScale.z);
        }
    }

    private void CheckSprint()
    {
        if (Input.GetKey(SprintKey) && !_isShooting && MovementSpeed != SprintMovementSpeed && !_isCrouching && !_isADS)
        {
            MovementSpeed = SprintMovementSpeed;
        }
        else if ((Input.GetKeyUp(SprintKey) || _isShooting) && !_isCrouching && !_isADS)
        {
            MovementSpeed = _defaultMovementSpeed;
        }
    }

    private void CheckShoot()
    {
        bool shootInput = _currentGun.FullAuto ? Input.GetMouseButton(ShootKey) : Input.GetMouseButtonDown(ShootKey);
        if (shootInput && _currentGun)
        {
            _isShooting = !_currentGun.MagazineIsEmpty;
            _currentGun.Shoot();
        }
        else if (Input.GetMouseButtonUp(ShootKey))
        {
            _isShooting = false;
        }
    }

    private void CheckADS()
    {
        if (Input.GetMouseButton(ADSKey) && _currentGun)
        {
            _isADS = true;
            if (!_isCrouching)
            {
                MovementSpeed = ADSMovementSpeed;
            }
        }
        else if (Input.GetMouseButtonUp(ADSKey))
        {
            _isADS = false;
            if (!_isCrouching)
            {
                MovementSpeed = _defaultMovementSpeed;
            }
        }
    }

    private void CheckReload()
    {
        if (_currentGun && _currentGun.CurrentAmmo < 1)
        {
            _currentGun.Reload();
        }
        else if (Input.GetKeyDown(ReloadKey) && _currentGun)
        {
            _currentGun.Reload();
        }
    }

    // This is for testing purpose, change it with a list of current weapons
    private void CheckChangeWeapon()
    {
        if (Guns.Count > 0 && Guns.Count == WeaponSlotKeycodes.Count)
        {
            for (int i = 0; i < WeaponSlotKeycodes.Count; i++)
            {
                if (Input.GetKeyDown(WeaponSlotKeycodes[i]))
                {
                    DisableAllGuns();
                    Guns[i].gameObject.SetActive(true);
                    _currentGun = Guns[i];
                }
            }
        }
    }

    private void DisableAllGuns()
    {
        foreach (BaseGun gun in Guns)
        {
            gun.gameObject.SetActive(false);
        }
    }
}
