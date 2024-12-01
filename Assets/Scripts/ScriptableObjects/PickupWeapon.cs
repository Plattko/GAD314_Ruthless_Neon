using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Weapon", menuName = "Weapons/PickupWeapon", order = 0)]
public class PickupWeapon : ScriptableObject
{
    [Header("Universal Variables")]
    [SerializeField] protected RectTransform infoPanelPrefab;
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

    protected int bulletDamage;
    [HideInInspector] public int fireRate;
    protected int critChance;
    
    protected int curAmmo;

    [Header("Weapon Sprites")]
    [SerializeField] protected Sprite comWeaponSprite;
    [SerializeField] protected Sprite rarWeaponSprite;
    [SerializeField] protected Sprite epiWeaponSprite;
    [SerializeField] protected Sprite legWeaponSprite;

    [Header("Bullet Damage")]
    [SerializeField] protected int comMinBulDmg;
    [SerializeField] protected int comMaxBulDmg;
    [SerializeField] protected int rarMinBulDmg, rarMaxBulDmg;
    [SerializeField] protected int epiMinBulDmg, epiMaxBulDmg;
    [SerializeField] protected int legMinBulDmg, legMaxBulDmg;

    [Header("Crit Chance")]
    [SerializeField] protected int comMinCritCha;
    [SerializeField] protected int comMaxCritCha;
    [SerializeField] protected int rarMinCritCha, rarMaxCritCha;
    [SerializeField] protected int epiMinCritCha, epiMaxCritCha;
    [SerializeField] protected int legMinCritCha, legMaxCritCha;

    [Header("Fire Rate")]
    [SerializeField] protected int comMinFireRate;
    [SerializeField] protected int comMaxFireRate;
    [SerializeField] protected int rarMinFireRate, rarMaxFireRate;
    [SerializeField] protected int epiMinFireRate, epiMaxFireRate;
    [SerializeField] protected int legMinFireRate, legMaxFireRate;

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

    public virtual void InitialiseInfoPanel(RectTransform infoPanel)
    {

    }

    public virtual void Fire(Transform gunEndPos, Vector3 aimPos, Vector3 playerPos)
    {
        
    }
}
