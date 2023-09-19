using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        _hitPoints -= damage;
        if (_hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
