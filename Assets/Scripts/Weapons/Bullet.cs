using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int playerLayer;
    [SerializeField] private int bulletLayer;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float destroyDelay = 5f;
    private float damage = 1f;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Initialise(Vector3 shootDir, float _damage)
    {
        // Set damage
        damage = _damage;
        // Set rotation
        if (shootDir.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
        // Set velocity
        rb.velocity = shootDir * moveSpeed;
        // Destroy after delay
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collision with the player and other bullets
        if (other.gameObject.layer == playerLayer || other.gameObject.layer == bulletLayer) { return; }
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Hit a damageable object
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
