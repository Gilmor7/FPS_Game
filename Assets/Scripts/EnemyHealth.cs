using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;
    
    public bool TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); // TODO: Replace with event system?
        _hitPoints -= damage; 
        return _hitPoints <= 0;
    }
    
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
