using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] AnimationCurve arc;
    public float speed;
    public int damage = 1;

/*    void Update()
    {
        //transform.position += transform.forward * speed;
        // transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }*/

    public IEnumerator DeathTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemyy"))
        {
            other.GetComponent<IDamagable>().TakeDamage(damage);
        }
        if (other.CompareTag("EnvObj"))
        {
            Destroy(gameObject);
        }
    }
}
