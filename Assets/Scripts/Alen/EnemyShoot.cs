using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform firepoint;
    [SerializeField] private LayerMask player;

    [Header("Weapon stats")]
    [Range(0.1f, 3f)] public float fireRate; // Delay between full shooting cycles
    [Range(1, 3)] public int bulletsPerShot;
    [Range(0.1f, 10f)] public float rotateSpeed = 5f; // Rotation speed of firepoint
    [Range(5f, 50f)] public float attackRange = 15f;
    public float initialShotFire = 0.5f;

    private Transform playerTransform;
    private Vector3 shootDir;
    private bool canShoot = false;
    private bool isShooting = false; // To prevent multiple coroutines running at the same time

    void Start()
    {
        StartCoroutine(InitialDelay());
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, player);

        if (hitColliders.Length > 0)
        {
            playerTransform = hitColliders[0].transform;

            // Rotate firepoint to face player
            RotateFirepoint();

            if (canShoot && !isShooting)
            {
                isShooting = true;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            playerTransform = null;
        }
    }

    void RotateFirepoint()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - firepoint.position).normalized;
            shootDir = direction;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            firepoint.rotation = targetRotation;
        }
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(initialShotFire);
        canShoot = true;
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            //Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);
            Transform projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            projectile.GetComponent<EnemyBullet>().Initialise(shootDir);
            yield return new WaitForSeconds(0.2f); 
        }
        yield return new WaitForSeconds(fireRate); 
        isShooting = false; 
    }

    public bool IsPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, player);
        return hitColliders.Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackRange > 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }

}
