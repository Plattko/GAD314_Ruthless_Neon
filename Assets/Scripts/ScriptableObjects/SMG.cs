using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/SMG")]
public class SMG : ScriptableObject
{
    public string weaponName;

    public Sprite weaponSprite;
    public Sprite bulletSprite;

    public enum Rarity {Common, Rare, Epic, Legendary}
    public Rarity rarity;
    private int commonWeight = 60;
    private int rareWeight = 25;
    private int epicWeight = 10;

    public float bulletDamage;
    public float critChance;
    public float fireRate;
    public int ammoCount;

    private float comMinBulDmg = 3f, comMaxBulDmg = 5f;
    private float rarMinBulDmg = 6f, rarMaxBulDmg = 8f;
    private float epiMinBulDmg = 9f, epiMaxBulDmg = 11f;
    private float legMinBulDmg = 12f, legMaxBulDmg = 14f;

    private float comMinCritCha = 2f, comMaxCritCha = 5f;
    private float rarMinCritCha = 6f, rarMaxCritCha = 10f;
    private float epiMinCritCha = 11f, epiMaxCritCha = 15f;
    private float legMinCritCha = 16f, legMaxCritCha = 25f;

    private float comMinFireRate = 200f, comMaxFireRate = 299f;
    private float rarMinFireRate = 300f, rarMaxFireRate = 399f;
    private float epiMinFireRate = 400f, epiMaxFireRate = 499f;
    private float legMinFireRate = 500f, legMaxFireRate = 600f;

    private int minAmmoCount = 15, maxAmmoCount = 50;

    public void RarityTest()
    {
        int commonCount = 0;
        int rareCount = 0;
        int epicCount = 0;
        int legendaryCount = 0;

        for (int i = 0; i < 1000; i++)
        {
            int roll = Random.Range(0, 100);
            //Debug.Log("Roll: " + roll);
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
            else
            {
                legendaryCount++;
            }
        }
        Debug.Log(commonCount);
        Debug.Log("Common count: " + (float)commonCount/1000 * 100 + "%");
        Debug.Log("Rare count: " + (float)rareCount / 1000 * 100 + "%");
        Debug.Log("Epic count: " + (float)epicCount / 1000 * 100 + "%");
        Debug.Log("Legendary count: " + (float)legendaryCount / 1000 * 100 + "%");
    }

    public void CreateSMG()
    {
        int roll = Random.Range(0, 100);
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
        else
        {
            rarity = Rarity.Legendary;
        }

        switch (rarity)
        {
            case Rarity.Common:
                weaponName = "Common SMG";
                bulletDamage = Random.Range(comMinBulDmg, comMaxBulDmg);
                critChance = Random.Range(comMinCritCha, comMaxCritCha);
                fireRate = Random.Range(comMinFireRate, comMaxFireRate);
                break;

            case Rarity.Rare:
                weaponName = "Rare SMG";
                bulletDamage = Random.Range(rarMinBulDmg, rarMaxBulDmg);
                critChance = Random.Range(rarMinCritCha, rarMaxCritCha);
                fireRate = Random.Range(rarMinFireRate, rarMaxFireRate);
                break;

            case Rarity.Epic:
                weaponName = "Epic SMG";
                bulletDamage = Random.Range(epiMinBulDmg, epiMaxBulDmg);
                critChance = Random.Range(epiMinCritCha, epiMaxCritCha);
                fireRate = Random.Range(epiMinFireRate, epiMaxFireRate);
                break;

            case Rarity.Legendary:
                weaponName = "Legendary SMG";
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
