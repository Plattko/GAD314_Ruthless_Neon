using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnTest : MonoBehaviour
{
    [SerializeField] private Transform pickupWeaponPrefab;
    [SerializeField] private Weapon[] origWeaponSO;
    [SerializeField] private RectTransform[] infoPanelPrefabs;
    private Transform pickupWeapon;
    private Weapon weaponSO;

    private float minDropForceX = 2f;
    private float maxDropForceX = 3f;
    private float dropForceY = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnWeapon();
        }
    }

    private void SpawnWeapon()
    {
        // Instantiate the pickup weapon
        pickupWeapon = Instantiate(pickupWeaponPrefab, transform.position, Quaternion.identity);
        // Randomly pick from the array of weapon types
        int weaponRoll = Random.Range(0, origWeaponSO.Length);
        // Instantiate the weapon's scriptable object
        weaponSO = Instantiate(origWeaponSO[weaponRoll]);
        // Give the pickup weapon a reference to its scriptable object
        pickupWeapon.GetComponent<PickupWeapon>().weaponStats = weaponSO;
        // Create the weapon's stats
        weaponSO.CreateWeapon();
        // Set the weapon's sprite to the sprite chosen when the weapon's stats were created
        pickupWeapon.GetComponent<SpriteRenderer>().sprite = weaponSO.weaponSprite;

        // Instantiate the weapon's info panel
        RectTransform infoPanel = Instantiate(infoPanelPrefabs[weaponRoll], pickupWeapon);
        // Set its position and scale so that it appears above the weapon
        infoPanel.position = new Vector3(pickupWeapon.position.x, pickupWeapon.position.y + 2f, pickupWeapon.position.z);
        infoPanel.localScale = new Vector3(0.01f, 0.01f, 1f);
        // Initialise it with the weapon's stats
        weaponSO.InitialiseInfoPanel(infoPanel);
        // Hide it
        infoPanel.gameObject.SetActive(false);

        // Give the weapon a slight force so it's thrown in a random direction
        Vector2 dropForceX = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(minDropForceX, maxDropForceX);
        Vector3 dropForce = new Vector3(dropForceX.x, dropForceY, dropForceX.y);
        pickupWeapon.GetComponent<Rigidbody>().AddForce(dropForce, ForceMode.Impulse);
    }
}
