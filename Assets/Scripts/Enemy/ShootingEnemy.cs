using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEntity
{
    [Header("Radius for checking ")]
    public float CheckRadius;

    public BaseGun Gun;

    private bool _isEnemyShooting;
    private bool _playerInLineOfSight;

    private void Awake()
    {
        InitializeCharacterController();
    }

    void Update()
    {
        CheckGrounded();

        CheckSurroundings();

        CheckShoot();

        CheckReload();
    }

    private void CheckSurroundings()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, CheckRadius);

        foreach (Collider collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player && CheckIfPlayerIsInLineOfSight(player))
            {
                _playerInLineOfSight = true;
                break;
            }
            else
            {
                _playerInLineOfSight = false;
            }
        }
    }

    private bool CheckIfPlayerIsInLineOfSight(PlayerController player)
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            return hitInfo.collider.gameObject.name == player.gameObject.name;
        }
        return false;
    }

    private void CheckShoot()
    {
        if (_playerInLineOfSight && Gun)
        {
            _isEnemyShooting = !Gun.MagazineIsEmpty;
            Gun.Shoot();
        }
        else
        {
            _isEnemyShooting = false;
        }
    }

    private void CheckReload()
    {
        if (Gun && Gun.CurrentAmmo < 1)
        {
            Gun.Reload();
        }
    }
}
