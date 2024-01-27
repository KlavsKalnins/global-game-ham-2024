using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] AnimationCurve arc;
    public float speed;

    void Update()
    {
        //transform.position += transform.forward * speed;
        // transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public IEnumerator DeathTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}
