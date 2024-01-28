using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public void SawPlayer()
    {
        StartCoroutine(SawThePlayer());
    }

    IEnumerator SawThePlayer()
    {
        PlayerHive.Instance.playerInSaw = true;
        Transform playerTransform = PlayerHive.Instance.transform;
        
        float duration = 0.55f;
        float elapsedTime = 0f;

        Vector3 initialPosition = playerTransform.position;
        Vector3 targetPosition = gameObject.transform.position;

        while (elapsedTime < duration)
        {
            if (playerTransform == null)
            {
                break;
            }
            playerTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (playerTransform != null)
        {
            playerTransform.transform.position = targetPosition;
        }

        yield return new WaitForSeconds(0.1f);

        PlayerHive.Instance.TakeDamage(100);
    }
}
