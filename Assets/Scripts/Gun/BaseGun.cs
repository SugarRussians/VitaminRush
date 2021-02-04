using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public int MagazineSize;
    public int ReloadTime;

    [Header("RPM (60 sec / FireRate = Delay between shots)")]
    public float FireRate;

    public Transform BulletSpawnPoint;

    public BaseBullet BaseBullet;

    public bool CanShoot = true;

    public int CurrentAmmo;

    private bool _magazineIsEmpty;

    private void Awake()
    {
        CurrentAmmo = MagazineSize;
    }

    public void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !_magazineIsEmpty)
        {
            Instantiate(BaseBullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            CurrentAmmo--;
            CanShoot = false;
            StartCoroutine(ToggleCanShoot());
        }
    }

    public void Reload()
    {
        _magazineIsEmpty = true;
        CurrentAmmo = MagazineSize;
        StartCoroutine(ToggleMagazineIsEmpty());
    }

    private IEnumerator ToggleCanShoot()
    {
        yield return new WaitForSeconds(60 / FireRate);
        CanShoot = true;
    }

    private IEnumerator ToggleMagazineIsEmpty()
    {
        yield return new WaitForSeconds(ReloadTime);
        _magazineIsEmpty = false;
    }
}
