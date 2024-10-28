using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private int minHealth = 3;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float curHealth = 10f;

    private void Start()
    {
        curHealth = Random.Range(minHealth, maxHealth);
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
