using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    [SerializeField] LavaPit prefab;
    public Transform target;
    public float instantiateAfterX = 4.5f;




    private void Start()
    {
        if (target != null)
        {
            Debug.Log("start target is something");
            // Use LeanTween to move towards the target
            LeanTween.move(gameObject, target.position, instantiateAfterX)
                .setOnComplete(OnInstantiateAfter);
        }

    }
    private void OnInstantiateAfter()
    {
        // Instantiate the prefab at the current position
        Instantiate(prefab, transform.position, Quaternion.identity);

        // Destroy the game object
        Destroy(gameObject);
    }

/*    void Update()
    {
        if (target == null) return;
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }*/
}
