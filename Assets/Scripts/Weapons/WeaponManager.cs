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
    [SerializeField] private AudioClip pistolFireSFX;
    [SerializeField] private AudioClip pickUpWeaponSFX;
    [SerializeField] private AudioClip dropWeaponSFX;
    [SerializeField] private float pistolDmg = 4f;
    [SerializeField] private float pistolFireRate = 90f;
    
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
    private float pistolShotCooldown = 0f;
    private float pickupWeaponShotCooldown = 0f;

    [Header("Sprite & Animation")]
    [SerializeField] private Image weaponHUD;
    [SerializeField] private Sprite pistolHudIcon;
    private Animator animator;

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
        // Do nothing if the firing is on cooldown
        if (Time.time < pistolShotCooldown) { return; }

        // Play the fire sound effect
        SFXManager.instance.PlayGunshotAudioClip(pistolFireSFX, gunEndPos, 0.25f, false);
        // Spawn the bullet
        Transform bullet = Instantiate(bulletPrefab, gunEndPos.position, Quaternion.identity);
        // Set the shoot direction
        Vector3 shootDir = (aimPos - transform.position).normalized;
        // Initialise the bullet
        bullet.GetComponent<Bullet>().Initialise(shootDir, pistolDmg);
        // Set the shot cooldown based on the weapon's firerate
        pistolShotCooldown = Time.time + (1f / (pistolFireRate / 60f));
        // Stop shooting so the pisol is semi-automatic
        StopShooting();
    }

    //-------------------------------------------------------------
    // PICKUP WEAPON
    //-------------------------------------------------------------
    private void FirePickupWeapon()
    {
        // Do nothing if the firing is on cooldown
        if (Time.time < pickupWeaponShotCooldown) { return; }

        // Fire the weapon
        pickupWeapon.weaponStats.Fire(gunEndPos, aimPos, transform.position);
        // Set the shot cooldown based on the weapon's firerate
        pickupWeaponShotCooldown = Time.time + (1f / (pickupWeapon.weaponStats.fireRate / 60f));
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

        // Play the pick up SFX
        SFXManager.instance.PlayAudioClip(pickUpWeaponSFX, transform, 0.25f);
        // Get a reference to the weapon's script
        pickupWeapon = weapon.GetComponent<PickupWeapon>();
        // Parent it to the player and update its position to be in the player's hands
        pickupWeapon.transform.parent = weaponHolder;
        pickupWeapon.transform.localScale = Vector3.one;
        pickupWeapon.transform.localPosition = new Vector3(0.72f, 0.25f, -0.25f);
        // If the player is holding the pistol, swap to the picked up weapon
        if (equippedGun == EquippedGun.Pistol)
        {
            SwapWeapon();
        }
        // Set the weapon HUD sprite to the new weapon's HUD icon
        weaponHUD.sprite = pickupWeapon.weaponStats.hudIcon;
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

            // Play the drop SFX
            SFXManager.instance.PlayAudioClip(dropWeaponSFX, transform, 0.25f);
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
                weaponHUD.sprite = pickupWeapon.weaponStats.hudIcon;
                break;

            // Swapping from pickup weapon to pistol
            case EquippedGun.PickupWeapon:
                equippedGun = EquippedGun.Pistol;
                pickupWeapon.GetComponent<SpriteRenderer>().enabled = false;
                pistol.GetComponent<SpriteRenderer>().enabled = true;
                animator.SetBool("isHoldingPistol", true);
                animator.SetBool("isHoldingWeapon", false);
                weaponHUD.sprite = pistolHudIcon;
                break;

            default:
                break;
        }
    }
}
