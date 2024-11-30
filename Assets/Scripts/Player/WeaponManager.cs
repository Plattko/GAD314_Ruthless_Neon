using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private enum EquippedGun { Pistol, PickupWeapon };
    private EquippedGun equippedGun = EquippedGun.Pistol;

    [Header("Shooting")]
    [SerializeField] private Transform aimPivot;
    [SerializeField] private Transform gunEndPos;
    private Vector3 aimPos;
    private PickupWeapon pickupWeapon;

    [SerializeField] private float pistolDmg = 1f;

    [HideInInspector] public bool isShooting = false;
    private float shotCooldown = 0f;

    [Header("Sprite & Animation")]
    private Animator animator;
    private Sprite pistolSprite;
    private Sprite weaponSprite;

    [Header("Image Reference")]
    [SerializeField] private Image weaponHUD;

    private void FixedUpdate()
    {
        GetAimPosition();

        if (isShooting)
        {
            Shoot();
        }
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
                FirePistol();
                break;

            case EquippedGun.PickupWeapon:
                FirePickupWeapon();
                break;

            default:
                break;
        }
    }

    private void FirePistol()
    {
        // Spawn the bullet
        Transform bullet = Instantiate(bulletPrefab, gunEndPos.position, Quaternion.identity);
        // Set the shoot direction
        Vector3 shootDir = (aimPos - transform.position).normalized;
        // Initialise the bullet
        bullet.GetComponent<Bullet>().Initialise(shootDir, pistolDmg);
        // Stop shooting so the pisol is semi-automatic
        StopShooting();
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    private void FirePickupWeapon()
    {
        // Do nothing if the firing is on cooldown
        if (Time.time < shotCooldown) { return; }
        
        // Fire the weapon
        pickupWeapon.Fire(gunEndPos, aimPos, transform.position);
        // Set the shot cooldown based on the weapon's firerate
        shotCooldown = Time.time + (1f / (pickupWeapon.fireRate / 60f));
        // Stop shooting if the weapon is not automatic
        if (!pickupWeapon.isAutomatic)
        {
            StopShooting();
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
                equippedGun = EquippedGun.PickupWeapon;
                animator.SetBool("isHoldingPistol", false);
                animator.SetBool("isHoldingWeapon", true);
                weaponHUD.sprite = pistolSprite;
                break;

            case EquippedGun.PickupWeapon:
                equippedGun = EquippedGun.Pistol;
                animator.SetBool("isHoldingPistol", true);
                animator.SetBool("isHoldingWeapon", false);
                weaponHUD.sprite = weaponSprite;
                break;

            default:
                break;
        }
    }
}
