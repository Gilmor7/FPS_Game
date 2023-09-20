using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    public bool TakeDamage(float damage)
    {
        _hitPoints -= damage;
        return _hitPoints <= 0;
    }

    public void Die()
    {
        Debug.Log("Player is dead now");
    }
}
