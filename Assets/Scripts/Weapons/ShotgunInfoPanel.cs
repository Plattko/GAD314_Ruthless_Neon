using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotgunInfoPanel : MonoBehaviour
{
    [SerializeField] private Color commonLightColour;
    [SerializeField] private Color commonDarkColour;
    [SerializeField] private Color rareLightColour;
    [SerializeField] private Color rareDarkColour;
    [SerializeField] private Color epicLightColour;
    [SerializeField] private Color epicDarkColour;
    [SerializeField] private Color legendaryLightColour;
    [SerializeField] private Color legendaryDarkColour;

    [SerializeField] private Image[] lightColourImages;
    [SerializeField] private Image darkColourImage;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI dmgNum;
    [SerializeField] private TextMeshProUGUI pelletsNum;
    [SerializeField] private TextMeshProUGUI spreadNum;
    [SerializeField] private TextMeshProUGUI fireRateNum;
    [SerializeField] private TextMeshProUGUI critChanceNum;
    [SerializeField] private TextMeshProUGUI ammoNum;

    [SerializeField] private Slider dmgBar;
    //[SerializeField] private Slider pelletsBar;
    [SerializeField] private Slider fireRateBar;
    [SerializeField] private Slider critChanceBar;

    public void Initialise(Weapon.Rarity rarity, string weaponName, float dmg, int pellets, int spread, int fireRate, int critChance, int ammo)
    {
        switch (rarity)
        {
            case Weapon.Rarity.Common:
                foreach (Image image in lightColourImages)
                {
                    image.color = commonLightColour;
                }
                darkColourImage.color = commonDarkColour;
                rarityText.text = "Common";
                break;

            case Weapon.Rarity.Rare:
                foreach (Image image in lightColourImages)
                {
                    image.color = rareLightColour;
                }
                darkColourImage.color = rareDarkColour;
                rarityText.text = "Rare";
                break;

            case Weapon.Rarity.Epic:
                foreach (Image image in lightColourImages)
                {
                    image.color = epicLightColour;
                }
                darkColourImage.color = epicDarkColour;
                rarityText.text = "Epic";
                break;

            case Weapon.Rarity.Legendary:
                foreach (Image image in lightColourImages)
                {
                    image.color = legendaryLightColour;
                }
                darkColourImage.color = legendaryDarkColour;
                rarityText.text = "Legendary";
                break;

            default:
                break;
        }

        nameText.text = weaponName;
        dmgNum.text = dmg.ToString("#.#");
        dmgBar.value = dmg;
        pelletsNum.text = pellets.ToString();
        //pelletsBar.value = pellets;
        fireRateNum.text = fireRate.ToString();
        fireRateBar.value = fireRate;
        critChanceNum.text = critChance.ToString() + "%";
        critChanceBar.value = critChance;
        spreadNum.text = spread.ToString() + "°";
        ammoNum.text = ammo.ToString();
    }

    public void UpdateAmmo(int ammo)
    {
        ammoNum.text = ammo.ToString();
    }
}
