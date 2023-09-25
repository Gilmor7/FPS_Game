using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private static readonly int DieAnimationTrigger = Animator.StringToHash("dieTrigger");
    
    [SerializeField] private float _hitPoints = 100f;
    public bool IsDead { get; private set; } = false; //optional somehow move this in to enemyController component with die functionality (Interface for both enemy and player?)
    
    public bool TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); // TODO: Replace with event system?
        _hitPoints -= damage; 
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
