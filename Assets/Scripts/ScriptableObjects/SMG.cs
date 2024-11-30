using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SMG", menuName = "Weapons/SMG", order = 2)]
public class SMG : PickupWeapon
{
    public override void CreateWeapon()
    {
        base.CreateWeapon();

        switch (rarity)
        {
            case Rarity.Common:
                weaponName = "Common SMG";
                weaponSprite = comWeaponSprite;
                bulletDamage = Random.Range(comMinBulDmg, comMaxBulDmg);
                critChance = Random.Range(comMinCritCha, comMaxCritCha);
                fireRate = Random.Range(comMinFireRate, comMaxFireRate);
                break;

            case Rarity.Rare:
                weaponName = "Rare SMG";
                weaponSprite = rarWeaponSprite;
                bulletDamage = Random.Range(rarMinBulDmg, rarMaxBulDmg);
                critChance = Random.Range(rarMinCritCha, rarMaxCritCha);
                fireRate = Random.Range(rarMinFireRate, rarMaxFireRate);
                break;

            case Rarity.Epic:
                weaponName = "Epic SMG";
                weaponSprite = epiWeaponSprite;
                bulletDamage = Random.Range(epiMinBulDmg, epiMaxBulDmg);
                critChance = Random.Range(epiMinCritCha, epiMaxCritCha);
                fireRate = Random.Range(epiMinFireRate, epiMaxFireRate);
                break;

            case Rarity.Legendary:
                weaponName = "Legendary SMG";
                weaponSprite = legWeaponSprite;
                bulletDamage = Random.Range(legMinBulDmg, legMaxBulDmg);
                critChance = Random.Range(legMinCritCha, legMaxCritCha);
                fireRate = Random.Range(legMinFireRate, legMaxFireRate);
                break;

            default:
                break;
        }

        ammoCount = Random.Range(minAmmoCount, maxAmmoCount);

        Debug.Log("Weapon name: " + weaponName);
        Debug.Log("Rarity: " + rarity);
        Debug.Log("Bullet damage: " + bulletDamage);
        Debug.Log("Crit chance: " + critChance);
        Debug.Log("Fire rate: " + fireRate);
        Debug.Log("Ammo count: " + ammoCount);
    }
}
