using System.Collections;
using UnityEngine;

public class PlayerHive : MonoBehaviour, IDamagable
{
    public static PlayerHive Instance;
    public bool isMeleeInvulnerability;
    public bool isJumpSmashInvulnerability;

    [SerializeField] HumanoidStatsSO stats;

    private void OnEnable()
    {
        Instance = this;
        stats.Health = 3;
    }
    private void Start()
    {
        PlayerHealthUI.Instance.UpdateHealth(stats.Health);
    }

    public IEnumerator MeleeAction()
    {
        isMeleeInvulnerability = true;
        yield return new WaitForSeconds(0.75f);
        isMeleeInvulnerability = false;
        // Debug.Log("END isMeleeInvulnerability");
    }

    public int GetDamageAmount()
    {
        return stats.Damage;
    }
    public int GetHealthAmount()
    {
        return stats.Health;
    }

    public void TakeDamage(int value)
    {
        stats.Health -= value;
        PlayerHealthUI.Instance.UpdateHealth(stats.Health);
        if (stats.Health <= 0)
        {
            Destroy(gameObject);
            // SceneManager.LoadScene(0);
        }
    }
}
