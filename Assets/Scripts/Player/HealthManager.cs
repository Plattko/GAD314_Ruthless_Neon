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
    [SerializeField] private Image healthHUD;

    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMaximum;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurrent <0)
        {
            healthCurrent = 0;
        }

        switch (healthCurrent)
        {
            case 0: healthHUD.sprite = healthEmpty; break;
            case 1: healthHUD.sprite = healthOne; break;
            case 2: healthHUD.sprite = healthTwo; break;
            case 3: healthHUD.sprite = healthThree; break;
            case 4: healthHUD.sprite = healthFour; break;
            case 5: healthHUD.sprite = healthFive; break;
            default: healthHUD.sprite = healthFive; break;
        }
    }
}
