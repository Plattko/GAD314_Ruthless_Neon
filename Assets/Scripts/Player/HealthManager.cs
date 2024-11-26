using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private int healthMaximum = 5;
    public int healthCurrent;

    [Header("Sprites")]
    [SerializeField] private Sprite healthEmpty;
    [SerializeField] private Sprite healthOne;
    [SerializeField] private Sprite healthTwo;
    [SerializeField] private Sprite healthThree;
    [SerializeField] private Sprite healthFour;
    [SerializeField] private Sprite healthFive;

    [Header("Image Reference")]
    [SerializeField] private Image healthRenderer;

    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMaximum;
    }

    // Update is called once per frame
    void Update()
    {
        switch (healthCurrent)
        {
            case 0: healthRenderer.sprite = healthEmpty; break;
            case 1: healthRenderer.sprite = healthOne; break;
            case 2: healthRenderer.sprite = healthTwo; break;
            case 3: healthRenderer.sprite = healthThree; break;
            case 4: healthRenderer.sprite = healthFour; break;
            case 5: healthRenderer.sprite = healthFive; break;
        }
    }
}
