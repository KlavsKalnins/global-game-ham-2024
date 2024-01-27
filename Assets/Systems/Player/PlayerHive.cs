using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHive : MonoBehaviour
{
    public static PlayerHive Instance;
    public bool isMeleeInvulnerability;
    public bool isJumpSmashInvulnerability;

    [SerializeField] HumanoidStatsSO stats;

    private void OnEnable()
    {
        Instance = this;
    }

    public IEnumerator MeleeAction()
    {
        isMeleeInvulnerability = true;
        yield return new WaitForSeconds(0.75f);
        isMeleeInvulnerability = false;
        Debug.Log("END isMeleeInvulnerability");
    }

    public int GetDamageAmount()
    {
        return stats.Damage;
    }

    public void TakeDamage(int value)
    {
        stats.Health -= value;
        if (stats.Health <= 0)
        {
            Destroy(gameObject);
            // SceneManager.LoadScene(0);
        }
    }
}
