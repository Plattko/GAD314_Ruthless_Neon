using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Camera playerCamera;

    [Header("Weapon stats")]
    [Range(0.1f, 3f)] public float fireRate;
    [Range(1, 3)] public int bulletsPerShot;
    [Range(0.1f, 1f)] public float rotateSpeed;

    private Rigidbody rb;
    public float initialShotFire = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        Shoot();
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Shoot()
    {
        if(initialShotFire <= 0)
        {
            Instantiate(projectile, firepoint.position, firepoint.rotation);
            initialShotFire = fireRate;
        }
        else
        {
            initialShotFire -= Time.deltaTime;
        }
    }
    private void PlayerFound()
    {
       
    }
}
