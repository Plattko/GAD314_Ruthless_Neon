using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shotgun", menuName = "Weapons/Shotgun", order = 1)]
public class Shotgun : Weapon
{
    private int spreadDegrees;
    private int pelletCount;

    [Header("Shotgun Variables")]
    [SerializeField] private int minPelletCount;
    [SerializeField] private int maxPelletCount;
    [SerializeField] private int minSpreadDegrees;
    [SerializeField] private int maxSpreadDegrees;

    public override void CreateWeapon()
    {
        base.CreateWeapon();

        switch (rarity)
        {
            case Rarity.Common:
                weaponName = "Common Shotgun";
                weaponSprite = comWeaponSprite;
                bulletDamage = Random.Range(comMinBulDmg, comMaxBulDmg);
                critChance = Random.Range(comMinCritCha, comMaxCritCha);
                fireRate = Random.Range(comMinFireRate, comMaxFireRate);
                break;

            case Rarity.Rare:
                weaponName = "Rare Shotgun";
                weaponSprite = rarWeaponSprite;
                bulletDamage = Random.Range(rarMinBulDmg, rarMaxBulDmg);
                critChance = Random.Range(rarMinCritCha, rarMaxCritCha);
                fireRate = Random.Range(rarMinFireRate, rarMaxFireRate);
                break;

            case Rarity.Epic:
                weaponName = "Epic Shotgun";
                weaponSprite = epiWeaponSprite;
                bulletDamage = Random.Range(epiMinBulDmg, epiMaxBulDmg);
                critChance = Random.Range(epiMinCritCha, epiMaxCritCha);
                fireRate = Random.Range(epiMinFireRate, epiMaxFireRate);
                break;

            case Rarity.Legendary:
                weaponName = "Legendary Shotgun";
                weaponSprite = legWeaponSprite;
                bulletDamage = Random.Range(legMinBulDmg, legMaxBulDmg);
                critChance = Random.Range(legMinCritCha, legMaxCritCha);
                fireRate = Random.Range(legMinFireRate, legMaxFireRate);
                break;

            default:
                break;
        }

        pelletCount = Random.Range(minPelletCount, maxPelletCount);
        spreadDegrees = Random.Range(minSpreadDegrees, maxSpreadDegrees);
        curAmmo = Random.Range(minAmmoCount, maxAmmoCount);

        Debug.Log("Weapon name: " + weaponName);
        Debug.Log("Rarity: " + rarity);
        Debug.Log("Bullet damage: " + bulletDamage);
        Debug.Log("Crit chance: " + critChance);
        Debug.Log("Fire rate: " + fireRate);
        Debug.Log("Bullet spread (degrees): " + spreadDegrees);
        Debug.Log("Pellet count: " + pelletCount);
        Debug.Log("Ammo count: " + curAmmo);
    }

    public override void InitialiseInfoPanel(RectTransform infoPanel)
    {
        // Get a reference to the Shotgun info panel script
        ShotgunInfoPanel shotgunInfoPanel = infoPanel.GetComponent<ShotgunInfoPanel>();
        // Initialise it with the Shotgun's stats
        shotgunInfoPanel.Initialise(rarity, weaponName, bulletDamage, pelletCount, spreadDegrees, fireRate, critChance, curAmmo);
    }

    public override void UpdateInfoPanelAmmo(RectTransform infoPanel)
    {
        // Get a reference to the Shotgun info panel script
        ShotgunInfoPanel shotgunInfoPanel = infoPanel.GetComponent<ShotgunInfoPanel>();
        // Initialise it with the Shotgun's stats
        shotgunInfoPanel.UpdateAmmo(curAmmo);
    }

    public override void Fire(Transform gunEndPos, Vector3 aimPos, Vector3 playerPos)
    {
        // If the weapon has no ammo, do nothing
        if (curAmmo <= 0) { return; }

        // Reduce the weapon's ammo by 1
        curAmmo -= 1;

        for (int i = 0; i < pelletCount; i++)
        {
            // Spawn the bullet
            Transform bullet = Instantiate(bulletPrefab, gunEndPos.position, Quaternion.identity);

            // Set the initial shoot direction
            Vector3 shootDir = (aimPos - playerPos).normalized;
            // Randomly rotate the shoot direction by the weapon's spread
            shootDir = Quaternion.AngleAxis(Random.Range(-spreadDegrees / 2, spreadDegrees / 2), Vector3.up) * shootDir;
            // Initialise the bullet
            bullet.GetComponent<Bullet>().Initialise(shootDir, bulletDamage);
        }
    }
}
