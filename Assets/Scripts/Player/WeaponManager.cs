using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private enum EquippedGun { Pistol, Weapon };
    private EquippedGun equippedGun = EquippedGun.Pistol;

    [Header("Shooting")]
    [SerializeField] private Transform aimPivot;
    [SerializeField] private Transform gunEndPos;
    private Vector3 aimPos;

    [Header("Sprite & Animation")]
    private Animator animator;

    private void FixedUpdate()
    {
        GetAimPosition();
    }

    //-------------------------------------------------------------
    // INITIALISATION
    //-------------------------------------------------------------
    public void Initialise(Animator _animator)
    {
        animator = _animator;
    }

    //-------------------------------------------------------------
    // SHOOTING
    //-------------------------------------------------------------
    private void GetAimPosition()
    {
        Vector3 mousePos = Mouse3D.GetMouseWorldPosition();
        aimPos = new Vector3(mousePos.x, aimPivot.position.y, mousePos.z);
        aimPivot.LookAt(aimPos, Vector3.up);
    }

    public void Shoot()
    {
        switch (equippedGun)
        {
            case EquippedGun.Pistol:
                Transform bullet = Instantiate(bulletPrefab, gunEndPos.position, Quaternion.identity);
                Vector3 shootDir = (aimPos - transform.position).normalized;
                bullet.GetComponent<Bullet>().Initialise(shootDir);
                break;

            case EquippedGun.Weapon:
                break;

            default:
                break;
        }
    }

    //-------------------------------------------------------------
    // SWAPPING
    //-------------------------------------------------------------
    public void SwapWeapon()
    {
        switch (equippedGun)
        {
            case EquippedGun.Pistol:
                equippedGun = EquippedGun.Weapon;
                animator.SetBool("isHoldingPistol", false);
                animator.SetBool("isHoldingWeapon", true);
                break;

            case EquippedGun.Weapon:
                equippedGun = EquippedGun.Pistol;
                animator.SetBool("isHoldingPistol", true);
                animator.SetBool("isHoldingWeapon", false);
                break;

            default:
                break;
        }
    }
}
