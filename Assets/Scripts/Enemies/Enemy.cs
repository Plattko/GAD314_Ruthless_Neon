using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    private float curHealth;
    private bool isDead = false;

    [Header("Loot")]
    [SerializeField] private WeaponSpawner weaponSpawner;
    [SerializeField] private float weaponDropChance = 0.33f;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the damage amount
        curHealth -= amount;

        // What to do if the enemy dies
        if (curHealth <= 0 && !isDead)
        {
            // Set the enemy to dead so the following code can't be run multiple times
            isDead = true;
            // Drop a weapon if the roll is lower than the weapon drop chance
            float roll = Random.Range(0f, 1f);
            if (roll < weaponDropChance)
            {
                weaponSpawner.SpawnWeapon();
            }
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}
