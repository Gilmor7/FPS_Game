using Common;
using UnityEngine;

//TODO: spare code duplication in HealthSystems by using Template Method or strategy patterns.
public class EnemyHealth : MonoBehaviour, IHealthSystem
{
    private static readonly int DieAnimationTrigger = Animator.StringToHash("dieTrigger");
    
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
            GetComponent<Animator>().SetTrigger(DieAnimationTrigger);
        }
    }
}
