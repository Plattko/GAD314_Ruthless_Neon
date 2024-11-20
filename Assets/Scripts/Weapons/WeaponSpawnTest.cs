using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnTest : MonoBehaviour
{
    [SerializeField] private Transform pickupWeaponPrefab;
    //[SerializeField] private SMG origSMGScriptableObject;
    [SerializeField] private PickupWeapon[] origPickupWeaponSOs;
    private Transform pickupWeapon;
    private PickupWeapon spawnedPickupWeaponSO;

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
        pickupWeapon = Instantiate(pickupWeaponPrefab, transform.position, Quaternion.identity);
        //spawnedSMG = Instantiate(origSMGScriptableObject);
        spawnedPickupWeaponSO = Instantiate(origPickupWeaponSOs[Random.Range(0, origPickupWeaponSOs.Length)]);
        spawnedPickupWeaponSO.CreateWeapon();
        //spawnedSMG.RarityTest();

        Vector2 dropForceX = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(minDropForceX, maxDropForceX);
        Vector3 dropForce = new Vector3(dropForceX.x, dropForceY, dropForceX.y);
        pickupWeapon.GetComponent<Rigidbody>().AddForce(dropForce, ForceMode.Impulse);
        pickupWeapon.GetComponent<SpriteRenderer>().sprite = spawnedPickupWeaponSO.weaponSprite;
    }
}
