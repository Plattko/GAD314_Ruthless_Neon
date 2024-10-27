using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float curHealth = 10f;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void OnEnable()
    {
        damageable.onDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        damageable.onDamaged -= TakeDamage;
    }

    private void TakeDamage(float amount)
    {
        curHealth -= amount;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
