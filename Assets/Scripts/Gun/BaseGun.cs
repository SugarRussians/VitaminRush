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

    [Header("0 is no spread, 4 is already inaccurate")]
    public float Accuracy;

    private void Awake()
    {
        CurrentAmmo = MagazineSize;
    }

    public void Shoot()
    {
        if (CanShoot && CurrentAmmo > 0 && !MagazineIsEmpty)
        {
            CreateBullet();

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

    private void CreateBullet()
    {
        var randomNumberX = Random.Range(-Accuracy, Accuracy);
        var randomNumberY = Random.Range(-Accuracy, Accuracy);
        var randomNumberZ = Random.Range(-Accuracy, Accuracy);

        Vector3 spread = new Vector3(randomNumberX, randomNumberY, randomNumberZ);
        Quaternion rotation = Quaternion.Euler(spread) * BulletSpawnPoint.rotation;

        BaseBullet baseBullet = Instantiate(BaseBullet, BulletSpawnPoint.position, rotation);
        baseBullet.gameObject.SetActive(true);
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
