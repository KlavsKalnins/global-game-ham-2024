using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float radius = 4f;
    [SerializeField] int damage = 2;
    [SerializeField] float timeUntilDetonate = 2f;

    [SerializeField] ParticleSystem particle;

    public void Explode()
    {
        LeanTween.delayedCall(timeUntilDetonate, () =>
        {
            if (particle != null)
            {
                particle.Play();
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player") || collider.CompareTag("Enemyy"))
                {
                    IDamagable damagable = collider.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(damage);
                    }
                }
            }
            gameObject.SetActive(false);
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
