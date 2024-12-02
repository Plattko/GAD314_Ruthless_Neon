using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private enum EquippedGun { Pistol, PickupWeapon };
    private EquippedGun equippedGun = EquippedGun.Pistol;

    [Header("Pistol")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float pistolDmg = 4f;
    
    [Header("Pickup Weapon")]
    private float minDropForceX = 2f;
    private float maxDropForceX = 3f;
    private float dropForceY = 2f;

    [Header("Shooting")]
    [SerializeField] private Transform aimPivot;
    [SerializeField] private Transform gunEndPos;
    [SerializeField] private Transform weaponHolder;
    private Vector3 aimPos;

    [SerializeField] private Transform pistol;
    private PickupWeapon pickupWeapon;

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

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    //-------------------------------------------------------------
    // PISTOL
    //-------------------------------------------------------------
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

    //-------------------------------------------------------------
    // PICKUP WEAPON
    //-------------------------------------------------------------
    private void FirePickupWeapon()
    {
        // Do nothing if the firing is on cooldown
        if (Time.time < shotCooldown) { return; }

        // Fire the weapon
        pickupWeapon.weaponStats.Fire(gunEndPos, aimPos, transform.position);
        // Set the shot cooldown based on the weapon's firerate
        shotCooldown = Time.time + (1f / (pickupWeapon.weaponStats.fireRate / 60f));
        // Stop shooting if the weapon is not automatic
        if (!pickupWeapon.weaponStats.isAutomatic)
        {
            StopShooting();
        }
    }

    public void PickUpWeapon(Transform weapon)
    {
        // Do nothing if there is no gun to pick up
        if (weapon == null) { return; }
        
        // Drop the currently held pickup weapon
        if (weaponHolder.childCount > 1)
        {
            DropWeapon(true);
        }

        pickupWeapon = weapon.GetComponent<PickupWeapon>();
        pickupWeapon.transform.parent = weaponHolder;
        pickupWeapon.transform.localScale = Vector3.one;
        pickupWeapon.transform.localPosition = new Vector3(0.72f, 0.25f, -0.25f);
        if (equippedGun == EquippedGun.Pistol)
        {
            SwapWeapon();
        }
    }

    public void DropWeapon(bool isReplacingWeapon)
    {
        Debug.Log("Dropped weapon.");

        // Do nothing if the player has no pickup weapon
        if (pickupWeapon == null) { return; }

        // What to do if the player is not picking up another weapon (e.g. pressed the drop weapon input)
        if (!isReplacingWeapon)
        {
            // Do nothing if the player is not holding the pickup weapon
            if (equippedGun == EquippedGun.Pistol) { return; }

            // Swap to the pistol
            SwapWeapon();
        }

        // Remove the weapon's parent
        pickupWeapon.transform.parent = null;

        // Enable the weapon's sprite in case it's disabled
        pickupWeapon.GetComponent<SpriteRenderer>().enabled = true;
        // Set the weapon's scale to make sure it isn't negative
        pickupWeapon.transform.localScale = Vector3.one;
        // Set the weapon's position
        pickupWeapon.transform.position = weaponHolder.position;
        // Set the weapon's rigidbody to non-kinematic and enable collision
        pickupWeapon.GetComponent<Rigidbody>().isKinematic = false;
        pickupWeapon.GetComponent<Collider>().enabled = true;

        // Update the ammo count on the info panel
        pickupWeapon.weaponStats.UpdateInfoPanelAmmo(pickupWeapon.transform.GetChild(0).GetComponent<RectTransform>());

        // Give the weapon a slight force so it's thrown in a random direction
        Vector2 dropForceX = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(minDropForceX, maxDropForceX);
        Vector3 dropForce = new Vector3(dropForceX.x, dropForceY, dropForceX.y);
        pickupWeapon.GetComponent<Rigidbody>().AddForce(dropForce, ForceMode.Impulse);

        // Set the pickup weapon variable to null
        pickupWeapon = null;
    }

    //-------------------------------------------------------------
    // SWAPPING EQUIPPED WEAPON
    //-------------------------------------------------------------
    public void SwapWeapon()
    {
        switch (equippedGun)
        {
            // Swapping from pistol to pickup weapon
            case EquippedGun.Pistol:
                // Don't swap if the player is only holding the pistol
                if (weaponHolder.childCount <= 1) { return; }

                equippedGun = EquippedGun.PickupWeapon;
                pistol.GetComponent<SpriteRenderer>().enabled = false;
                pickupWeapon.GetComponent<SpriteRenderer>().enabled = true;
                animator.SetBool("isHoldingPistol", false);
                animator.SetBool("isHoldingWeapon", true);
                //weaponHUD.sprite = pistolSprite;                           // Turn back on after merge
                break;

            // Swapping from pickup weapon to pistol
            case EquippedGun.PickupWeapon:
                equippedGun = EquippedGun.Pistol;
                pickupWeapon.GetComponent<SpriteRenderer>().enabled = false;
                pistol.GetComponent<SpriteRenderer>().enabled = true;
                animator.SetBool("isHoldingPistol", true);
                animator.SetBool("isHoldingWeapon", false);
                //weaponHUD.sprite = weaponSprite;                           // Turn back on after merge
                break;

            default:
                break;
        }
    }
}
