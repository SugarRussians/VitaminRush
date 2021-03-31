using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEnemy
{
    public BaseGun Gun;

    public PatrolAgent PatrolAgent;

    private bool _isEnemyShooting;

    private void Awake()
    {
        InitializeCharacterController();
        PatrolAgent = GetComponent<PatrolAgent>();
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

            PatrolAgent.Pause(true);

            Gun.Shoot();
        }
        else
        {
            _isEnemyShooting = false;

            PatrolAgent.Pause(false);
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
