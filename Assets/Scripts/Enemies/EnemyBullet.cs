using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField] private Rigidbody rb;
    [SerializeField][Range(1f, 100f)] private float speed = 10f; // Dynamic speed, can be set in the inspector
    [SerializeField] private int damage = 10;    // Dynamic damage, can be set in the inspector
    //[SerializeField] private LayerMask playerLayer; // LayerMask to specify which layer the bullet can hit
    [SerializeField] private string enemyLayer = "Enemy";

    //void Start()
    //{
    //    // Move the bullet forward at the specified speed
    //    if (rb != null)
    //    {
    //        rb.velocity = transform.up * speed;
    //    }
    //    else
    //    {
    //        Debug.LogError("No Rigidbody2D found on the bullet. Please add one.");
    //    }
    //}

    public void Initialise(Vector3 shootDir)
    {
        // Set velocity
        rb.velocity = shootDir * speed;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // Check if the collision is with a player
    //    if (((1 << collision.gameObject.layer) & playerLayer) != 0)
    //    {
    //        // If the bullet hits the player, apply damage and destroy the bullet
    //        Debug.Log("Bullet hit the player. Applying damage: " + damage);
    //        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>(); // Assuming the player has a PlayerHealth script
    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(damage);
    //        }
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        // If it hits anything other than the player, destroy itself
    //        Debug.Log("Bullet hit something else. Destroying bullet.");
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collision with other enemies
        if (other.gameObject.layer == LayerMask.NameToLayer(enemyLayer)) { return; }

        // Ignore collision with the player if they are mid-dash
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && playerController.isDashing) { return; }

        // Deal damage if it hit a damageable object
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Hit a damageable object
            damageable.TakeDamage(damage);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
