using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private int maxHealth = 3;
    private float curHealth;
    private bool isDead = false;

    [Header("Loot")]
    [SerializeField] private WeaponSpawner weaponSpawner;
    private float weaponDropChance = 0.5f;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the damage amount
        curHealth -= amount;
        // Play the hurt SFX with randomised pitch
        SFXManager.instance.PlayAudioClip(hurtSFX, transform, 1.1f, true);

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
