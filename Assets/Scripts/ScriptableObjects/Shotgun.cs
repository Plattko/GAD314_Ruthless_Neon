using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shotgun", menuName = "Weapons/Shotgun", order = 1)]
public class Shotgun : PickupWeapon
{
    [Header("Shotgun-specific Stats")]
    private float spreadDegrees;
    private int pelletCount;

    [SerializeField] private float minSpreadDegrees;
    [SerializeField] private float maxSpreadDegrees;
    [SerializeField] private int minPelletCount;
    [SerializeField] private int maxPelletCount;

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

        spreadDegrees = Random.Range(minSpreadDegrees, maxSpreadDegrees);
        pelletCount = Random.Range(minPelletCount, maxPelletCount);
        ammoCount = Random.Range(minAmmoCount, maxAmmoCount);

        Debug.Log("Weapon name: " + weaponName);
        Debug.Log("Rarity: " + rarity);
        Debug.Log("Bullet damage: " + bulletDamage);
        Debug.Log("Crit chance: " + critChance);
        Debug.Log("Fire rate: " + fireRate);
        Debug.Log("Bullet spread (degrees): " + spreadDegrees);
        Debug.Log("Pellet count: " + pelletCount);
        Debug.Log("Ammo count: " + ammoCount);
    }
}
