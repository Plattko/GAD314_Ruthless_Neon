using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Weapon", menuName = "Weapons/PickupWeapon", order = 0)]
public class PickupWeapon : ScriptableObject
{
    [Header("Universal Variables")]
    [SerializeField] protected Transform bulletPrefab;
    public bool isAutomatic = false;
    
    public enum Rarity { Common, Rare, Epic, Legendary }
    protected Rarity rarity;

    [HideInInspector] public string weaponName;
    [HideInInspector] public Sprite weaponSprite;

    protected int commonWeight = 60;
    protected int rareWeight = 25;
    protected int epicWeight = 10;
    protected int legendaryWeight = 5;

    protected float bulletDamage;
    protected float critChance;
    public float fireRate;
    
    protected int curAmmo;

    [Header("Weapon Sprites")]
    [SerializeField] protected Sprite comWeaponSprite;
    [SerializeField] protected Sprite rarWeaponSprite;
    [SerializeField] protected Sprite epiWeaponSprite;
    [SerializeField] protected Sprite legWeaponSprite;

    [Header("Bullet Damage")]
    [SerializeField] protected float comMinBulDmg;
    [SerializeField] protected float comMaxBulDmg;
    [SerializeField] protected float rarMinBulDmg, rarMaxBulDmg;
    [SerializeField] protected float epiMinBulDmg, epiMaxBulDmg;
    [SerializeField] protected float legMinBulDmg, legMaxBulDmg;

    [Header("Crit Chance")]
    [SerializeField] protected float comMinCritCha;
    [SerializeField] protected float comMaxCritCha;
    [SerializeField] protected float rarMinCritCha, rarMaxCritCha;
    [SerializeField] protected float epiMinCritCha, epiMaxCritCha;
    [SerializeField] protected float legMinCritCha, legMaxCritCha;

    [Header("Fire Rate")]
    [SerializeField] protected float comMinFireRate;
    [SerializeField] protected float comMaxFireRate;
    [SerializeField] protected float rarMinFireRate, rarMaxFireRate;
    [SerializeField] protected float epiMinFireRate, epiMaxFireRate;
    [SerializeField] protected float legMinFireRate, legMaxFireRate;

    [Header("Ammo Count")]
    [SerializeField] protected int minAmmoCount;
    [SerializeField] protected int maxAmmoCount;

    public void RarityTest()
    {
        int commonCount = 0;
        int rareCount = 0;
        int epicCount = 0;
        int legendaryCount = 0;

        for (int i = 0; i < 1000; i++)
        {
            int totalWeight = commonWeight + rareWeight + epicWeight + legendaryWeight;
            int roll = Random.Range(0, totalWeight);
            if (roll < commonWeight)
            {
                commonCount++;
            }
            else if (roll < commonWeight + rareWeight)
            {
                rareCount++;
            }
            else if (roll < commonWeight + rareWeight + epicWeight)
            {
                epicCount++;
            }
            // If statement is only necessary for readability of legendary drop chance
            else if (roll < commonWeight + rareWeight + epicWeight + legendaryWeight)
            {
                legendaryCount++;
            }
        }
        Debug.Log("Common count: " + (float)commonCount / 1000 * 100 + "%");
        Debug.Log("Rare count: " + (float)rareCount / 1000 * 100 + "%");
        Debug.Log("Epic count: " + (float)epicCount / 1000 * 100 + "%");
        Debug.Log("Legendary count: " + (float)legendaryCount / 1000 * 100 + "%");
    }

    public virtual void CreateWeapon()
    {
        // Set the weapon's rarity
        int totalWeight = commonWeight + rareWeight + epicWeight + legendaryWeight;
        int roll = Random.Range(0, totalWeight);
        Debug.Log("Roll: " + roll);
        if (roll < commonWeight)
        {
            rarity = Rarity.Common;
        }
        else if (roll < commonWeight + rareWeight)
        {
            rarity = Rarity.Rare;
        }
        else if (roll < commonWeight + rareWeight + epicWeight)
        {
            rarity = Rarity.Epic;
        }
        // If statement is only necessary for readability of legendary drop chance
        else if (roll < commonWeight + rareWeight + epicWeight + legendaryWeight)
        {
            rarity = Rarity.Legendary;
        }

        // Set the weapon's current ammo
        curAmmo = Random.Range(minAmmoCount, maxAmmoCount);
    }

    public virtual void Fire(Transform gunEndPos, Vector3 aimPos, Vector3 playerPos)
    {
        
    }
}
