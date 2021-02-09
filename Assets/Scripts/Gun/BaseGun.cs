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
    public bool MagazineIsEmpty;

    public int CurrentAmmo;

    private void Awake()
    {
        CurrentAmmo = MagazineSize;
    }

    public void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !MagazineIsEmpty)
        {
            BaseBullet baseBullet = Instantiate(BaseBullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            baseBullet.gameObject.SetActive(true);
            CurrentAmmo--;
            CanShoot = false;
            StartCoroutine(ToggleCanShoot());
        }
    }

    public void Reload()
    {
        MagazineIsEmpty = true;
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
        MagazineIsEmpty = false;
    }
}
