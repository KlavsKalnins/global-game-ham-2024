using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SpawnParticle(Vector3 position, ParticleSystem particleSystem, float time = 0)
    {
        Debug.Log("SpawnPatricle");
        //var particle = Instantiate(particleSystem, position.transform.position, Quaternion.identity);

        LeanTween.delayedCall(time, () =>
        {
            if (particleSystem != null)
            {
                Debug.Log("particleSystem yayy");
                var particle = Instantiate(particleSystem, position, Quaternion.identity);
            } else
            {
                Debug.Log("wut");
            }
        });
    }
}
