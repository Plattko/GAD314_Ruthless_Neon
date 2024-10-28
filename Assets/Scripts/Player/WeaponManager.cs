using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    public void Shoot(Vector3 playerPos, Vector3 gunEndPos, Vector3 aimPos)
    {
        Transform bullet = Instantiate(bulletPrefab, gunEndPos, Quaternion.identity);
        Vector3 shootDir = (aimPos - playerPos).normalized;
        bullet.GetComponent<Bullet>().Initialise(shootDir);
    }
}
