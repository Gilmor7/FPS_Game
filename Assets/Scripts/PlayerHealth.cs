using Common;
using DataTypes;
using Managers;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthSystem
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _hitPoints = 100f;

    public bool TakeDamage(float damage)
    {
        _hitPoints -= damage;
        bool isDead = _hitPoints <= 0;

        if (!isDead)
        {
            AudioManager.Instance.PlaySoundEffect(
                _audioSource, SoundsEffectsRepository.GetPlayerSoundEffect(ActionType.PlayerAction.GetHurt));
        }

        return isDead;
    }

    public void Die()
    {
        GameManager.Instance.HandlePlayerDeath();
    }
}
