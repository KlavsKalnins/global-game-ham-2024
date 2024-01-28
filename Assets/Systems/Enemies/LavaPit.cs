using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPit : MonoBehaviour
{
    [SerializeField] bool isPlayerInLava;
    [SerializeField] float damageEveryX = 3f;

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
