using System.Collections;
using UnityEngine;

public class PlayerHive : MonoBehaviour, IDamagable
{
    public static PlayerHive Instance;
    public bool isMeleeInvulnerability;
    public bool isJumpSmashInvulnerability;

    [SerializeField] HumanoidStatsSO stats;
    [SerializeField] int playerGameHealth;
    [SerializeField] bool cheatsEnabled;

    [SerializeField] KeyCode firstKey = KeyCode.Tilde; // key manager
    [SerializeField] KeyCode secondKey = KeyCode.LeftAlt;

    private void OnEnable()
    {
        Instance = this;
        playerGameHealth = stats.Health;

    }
    private void Start()
    {
        PlayerHealthUI.Instance.UpdateHealth(playerGameHealth);
    }

    private void Update()
    {
        if (Input.GetKey(firstKey) && Input.GetKeyDown(secondKey))
        {
            
        }
        if (cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

            }
            if (Input.GetKeyDown(KeyCode.O))
            {

            }
        }
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
        return playerGameHealth;
    }

    public void TakeDamage(int value)
    {
        playerGameHealth -= value;
        PlayerHealthUI.Instance.UpdateHealth(playerGameHealth);
        if (playerGameHealth <= 0)
        {
            Destroy(gameObject);
            // SceneManager.LoadScene(0);
        }
    }
}
