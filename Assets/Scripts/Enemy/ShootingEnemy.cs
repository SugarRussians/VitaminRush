using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEnemy
{
    public BaseGun Gun;

    private bool _isEnemyShooting;

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

        CheckSelfDestroyWhenDead();
    }

    private void CheckShoot()
    {
        if (PlayerInLineOfSight && Gun)
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
