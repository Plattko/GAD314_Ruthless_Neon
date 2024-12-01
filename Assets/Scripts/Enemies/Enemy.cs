using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    private float curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
