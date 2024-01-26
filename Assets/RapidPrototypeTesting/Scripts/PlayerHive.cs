using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHive : MonoBehaviour
{
    public static PlayerHive Instance;
    public bool isMeleeInvulnerability;

    [SerializeField] HumanoidStatsSO stats;

    private void OnEnable()
    {
        Instance = this;
    }

    void Update()
    {
        
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
}
