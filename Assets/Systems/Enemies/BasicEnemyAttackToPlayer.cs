using UnityEngine;


public class BasicEnemyAttackToPlayer : MonoBehaviour, IAttackable
{
    [SerializeField] int damage;
    public void PerformAttack()
    {
        PlayerHive.Instance.TakeDamage(damage);
    }
}
