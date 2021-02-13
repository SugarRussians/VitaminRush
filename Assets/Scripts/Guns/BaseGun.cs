using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    public int MagazineSize;
    public int ReloadTime;

    [Header("RPM (60 sec / FireRate = Delay between shots)")]
    public float FireRate;

    [Header("0 is no spread, 4 is already inaccurate")]
    public float Accuracy;

    [Header("Full auto = hold mouse button down")]
    public bool FullAuto;

    public Transform BulletSpawnPoint;

    public BaseBullet BaseBullet;

    [HideInInspector]
    public bool CanShoot = true;
    [HideInInspector]
    public bool MagazineIsEmpty;
    [HideInInspector]
    public int CurrentAmmo;

    [Header("Bullets per shot")]
    public int NumberOfBulletsPerShot = 1;

    [Header("Burst Fire")]
    public bool IsBurstFire;
    public float BurstFireDelayBetweenShots;

    private void Awake()
    {
        CurrentAmmo = MagazineSize;
    }

    public void Reload()
    {
        MagazineIsEmpty = true;
        StartCoroutine(ToggleMagazineIsEmpty());
    }

    public abstract void Shoot();

    public void CreateBullet()
    {
        var randomNumberX = Random.Range(-Accuracy, Accuracy);
        var randomNumberY = Random.Range(-Accuracy, Accuracy);
        var randomNumberZ = Random.Range(-Accuracy, Accuracy);

        Vector3 spread = new Vector3(randomNumberX, randomNumberY, randomNumberZ);
        Quaternion rotation = Quaternion.Euler(spread) * BulletSpawnPoint.rotation;

        BaseBullet baseBullet = Instantiate(BaseBullet, BulletSpawnPoint.position, rotation);
        baseBullet.gameObject.SetActive(true);
    }

    public IEnumerator ToggleCanShoot()
    {
        yield return new WaitForSeconds(60 / FireRate);
        CanShoot = true;
    }

    private IEnumerator ToggleMagazineIsEmpty()
    {
        yield return new WaitForSeconds(ReloadTime);
        CurrentAmmo = MagazineSize;
        MagazineIsEmpty = false;
    }
}
