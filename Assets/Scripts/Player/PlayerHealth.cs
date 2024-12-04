using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Image healthUI;

    [Header("Health Variables")]
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private int maxHealth = 5;
    private int curHealth;

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
        // Play the hurt SFX with randomised pitch
        SFXManager.instance.PlayAudioClip(hurtSFX, transform, 1.1f, true);
        // Update the health UI
        UpdateHealthUI();

    }

    public void UpdateHealthUI()
    {
        // Set the health UI to match the current health
        healthUI.sprite = healthSprites[curHealth];
    }
}
