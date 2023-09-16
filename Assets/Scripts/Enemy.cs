using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Agent Config")] 
    [SerializeField] private float _chaseRange = 15f;

    private float _distanceFromTarget = Mathf.Infinity;
    private bool _isProvoked = false;

    void Update()
    {
        _distanceFromTarget = Vector3.Distance(_targetTransform.position, transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceFromTarget <= _chaseRange)
        {
            _isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (_distanceFromTarget > _agent.stoppingDistance)
        {
            ChaseTarget();
        }

        else
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        _agent.SetDestination(_targetTransform.position);
    }

    private void AttackTarget()
    {
        Debug.Log("EnemyAttacking target");
    }
    
    void OnDrawGizmosSelected()
    {
        // Display the chase range radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}
