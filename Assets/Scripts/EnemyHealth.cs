using Common;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthSystem
{
    [SerializeField] private float _hitPoints = 100f;
    public bool IsDead { get; private set; } = false;
    
    public bool TakeDamage(float damage)
    {
        _hitPoints -= damage; 
        
        if (!IsDead)
        {
            BroadcastMessage("OnDamageTaken");
        }
        
        return _hitPoints <= 0;
    }
    
    public void Die()
    {
        if (IsDead == false)
        {
            IsDead = true;
            GetComponent<EnemyController>().AnimateEnemyDie();
        }
    }
}
