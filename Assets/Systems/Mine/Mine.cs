using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Mine : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float radius = 4f;
    [SerializeField] int damage = 2;
    [SerializeField] float timeUntilDetonate = 2f;

    [SerializeField] ParticleSystem particle;
    [SerializeField] bool didExplode = false;
    [SerializeField] ParticleSystem particledetonateRed;

    public void Explode()
    {
        if (didExplode)
        {
            return;
        }
        didExplode = true;

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
            meshRenderer.enabled = false;
            if (particledetonateRed != null)
            {
                particledetonateRed.Stop();
            }
            LeanTween.delayedCall(1f, () =>
            {
                particle.Stop();
            });
            // gameObject.SetActive(false);
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
