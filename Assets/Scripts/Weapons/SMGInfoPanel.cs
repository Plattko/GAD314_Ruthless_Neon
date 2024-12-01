using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SMGInfoPanel : MonoBehaviour
{
    [SerializeField] private Color32 commonLightColour;
    [SerializeField] private Color32 commonDarkColour;
    [SerializeField] private Color32 rareLightColour;
    [SerializeField] private Color32 rareDarkColour;
    [SerializeField] private Color32 epicLightColour;
    [SerializeField] private Color32 epicDarkColour;
    [SerializeField] private Color32 legendaryLightColour;
    [SerializeField] private Color32 legendaryDarkColour;

    [SerializeField] private Image[] lightColourImages;
    [SerializeField] private Image darkColourImage;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI dmgNum;
    [SerializeField] private TextMeshProUGUI fireRateNum;
    [SerializeField] private TextMeshProUGUI critChanceNum;
    [SerializeField] private TextMeshProUGUI ammoNum;

    [SerializeField] private Slider dmgBar;
    [SerializeField] private Slider fireRateBar;
    [SerializeField] private Slider critChanceBar;

    public void Initialise(Weapon.Rarity rarity, string weaponName, int dmg, int fireRate, int critChance, int ammo)
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
        dmgNum.text = dmg.ToString();
        dmgBar.value = dmg;
        fireRateNum.text = fireRate.ToString();
        fireRateBar.value = fireRate;
        critChanceNum.text = (critChance).ToString() + "%";
        critChanceBar.value = critChance;
        ammoNum.text = ammo.ToString();
    }
}
