using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseGun
{
    public override void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !MagazineIsEmpty)
        {
            for (int i = 0; i < NumberOfBulletsPerShot; i++)
            {
                CreateBullet();
            }

            CanShoot = false;
            CurrentAmmo--;

            StartCoroutine(ToggleCanShoot());
        }
    }
}
