using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHive : MonoBehaviour, IDamagable
{
    public static PlayerHive Instance;
    public bool isMeleeInvulnerability;
    public bool isJumpSmashInvulnerability;
    public bool playerInSaw;

    [SerializeField] HumanoidStatsSO stats;
    [SerializeField] int playerGameHealth;
    [SerializeField] bool cheatsEnabled;

    [SerializeField] KeyCode firstKey = KeyCode.Tilde; // key manager
    [SerializeField] KeyCode secondKey = KeyCode.LeftAlt;

    [SerializeField] ParticleSystem playerDieParticle;

    [SerializeField] List<GameObject> visualGameObjects;
    public static Action OnPlayerDeath;

    [SerializeField] 

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
        if (isJumpSmashInvulnerability)
        {
            return;
        }
        playerGameHealth -= value;
        PlayerHealthUI.Instance.UpdateHealth(playerGameHealth);
        if (playerGameHealth <= 0)
        {
            OnGameObjectDestrojed();
            //ParticleManager
            // playerDieParticle.Play();
            //Destroy(gameObject);



            // IDeathParticle
            /*LeanTween.delayedCall(particleWaitTillRun, () =>
            {
                if (playerDieParticle != null)
                {
                    playerDieParticle.Play();
                    Destroy(gameObject);
                }
            });*/


            // SceneManager.LoadScene(0);
        }
    }

    void OnGameObjectDestrojed()
    {
        AudioManager.Instance.PlayAudio(AudioTypes.PlayerDeath);
        AudioManager.Instance.PlayAudio(AudioTypes.Idle, false);
        AudioManager.Instance.PlayAudio(AudioTypes.BattleSound, false);
        Vector3 currentPosition = PlayerGroundShadow.Instance.transform.position;
        OnPlayerDeath?.Invoke();

        // HudMa
/*        foreach (GameObject v in visualGameObjects)
        {
            v.SetActive(false);
        }*/

        ParticleManager.Instance.SpawnParticle(currentPosition, playerDieParticle);
        gameObject.SetActive(false);
        //Destroy(gameObject);
/*        LeanTween.delayedCall(0.01f, () =>
        {
            Destroy(gameObject);
        });*/
        //playerDieParticle.Play();

        // call player manager
        // disable
        //gameObject.SetActive(false);
    }
}
