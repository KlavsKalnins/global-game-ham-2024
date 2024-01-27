using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] float knockbackForce = 5f;
    [SerializeField] float knockbackAngleOffset = 45f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IHealthBehavior>().Damage(1, false, true);
            Debug.Log("collided with enemy");
            Knockback(other);
        }
    }

    void Knockback(Collider other)
    {
        Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();

        // Check if the enemy has a Rigidbody
        if (enemyRigidbody != null)
        {
            // Calculate the knockback direction from the closest point on the collider
            Vector3 knockbackDirection = Quaternion.Euler(0f, knockbackAngleOffset, 0f) *
                                         -(other.ClosestPointOnBounds(transform.position) - transform.position).normalized;

            // Apply knockback force
            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }

        /*        Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();

                // Check if the enemy has a Rigidbody
                if (enemyRigidbody != null)
                {
                    // Calculate the knockback direction (opposite to player's forward direction)
                    // Vector3 knockbackDirection = -(other.transform.position - transform.position).normalized;
                    Vector3 knockbackDirection = Quaternion.Euler(0f, knockbackAngleOffset, 0f) * -(other.transform.position - transform.position).normalized;

                    // Apply knockback force
                    enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }*/
    }
}
