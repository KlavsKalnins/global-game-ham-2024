using System.Collections;
using UnityEngine;

public class LavaPit : MonoBehaviour
{
    [SerializeField] bool isPlayerInLava;
    [SerializeField] float damageEveryX = 3f;


    private void Awake()
    {
        PitAudio.Instance.PlayAudio();
        StartCoroutine(LavaDeath());
    }

    IEnumerator LavaDeath()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    IEnumerator WhileInLava()
    {
        while (isPlayerInLava)
        {
            if (PlayerHive.Instance == null)
            {
                break;
            }
            PlayerHive.Instance.TakeDamage(1);
            yield return new WaitForSeconds(damageEveryX);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInLava = true;
            StartCoroutine(WhileInLava());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInLava = false;
        }
    }
}
