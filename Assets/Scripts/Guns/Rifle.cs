using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseGun
{
    public override void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !MagazineIsEmpty)
        {
            CreateBullet();

            CanShoot = false;
            CurrentAmmo--;

            StartCoroutine(ToggleCanShoot());
        }
    }
}
