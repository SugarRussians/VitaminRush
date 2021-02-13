using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRifle : BaseGun
{
    public override void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !MagazineIsEmpty)
        {
            if (IsBurstFire)
            {
                StartCoroutine(FireBurst());
            }

            CanShoot = false;

            StartCoroutine(ToggleCanShoot());
        }
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < NumberOfBulletsPerShot; i++)
        {
            CreateBullet();
            CurrentAmmo--;
            yield return new WaitForSeconds(BurstFireDelayBetweenShots);
        }
    }
}
