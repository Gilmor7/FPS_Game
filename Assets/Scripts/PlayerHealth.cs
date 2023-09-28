using Common;
using Managers;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthSystem
{
    [SerializeField] private float _hitPoints = 100f;

    public bool IsDead => (_hitPoints <= 0);

    public bool TakeDamage(float damage)
    {
        _hitPoints -= damage;

        if (!IsDead)
        {
            EventManager.Instance.PublishPlayerHealthDamageTaken();
        }

        return IsDead;
    }

    public void Die()
    {
        GameManager.Instance.HandlePlayerDeath();
    }
}
