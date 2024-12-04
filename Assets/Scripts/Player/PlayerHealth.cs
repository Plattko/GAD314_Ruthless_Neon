using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("UI Reference")]
    [SerializeField] private Image healthUI;

    [Header("Health Variables")]
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private int maxHealth = 5;
    private int curHealth;
    private bool isDead = false;

    [Header("Sprites")]
    [SerializeField] private Sprite[] healthSprites;

    void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the damage amount
        curHealth -= Mathf.RoundToInt(amount);
        // Clamp the health between 0 and the max health
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        // Play the hurt SFX with randomised pitch
        SFXManager.instance.PlayAudioClip(hurtSFX, transform, 1.1f, true);
        // Update the health UI
        UpdateHealthUI();

        // What to do if the player dies
        if (curHealth <= 0 && !isDead)
        {
            // Set the player to dead so the following code can't be run multiple times
            isDead = true;
            // Show the death menu
            GameManager.instance.EnterDeathMenu();
        }
    }

    public void UpdateHealthUI()
    {
        // Set the health UI to match the current health
        healthUI.sprite = healthSprites[curHealth];
    }
}
