using Common;
using DataTypes;
using Managers;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IDamageable
{
    [SerializeField] private float _damage = 20f;
    private PlayerHealth _target;
    public float Damage => _damage;
    
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag(GameManager.Instance.PlayerTag);

        if (player != null)
        {
            _target = player.GetComponent<PlayerHealth>();
            if (_target == null)
            {
                Debug.LogError("Player object does not have a PlayerHealth component.");
            }
        }
        else
        {
            Debug.LogError("Player object not found with tag 'Player'.");
        }
    }

    public void AttackHitEvent()
    {
        if (_target != null)
        {
            EventManager.Instance.PublishCharacterGotHit(this, _target);
            AudioManager.Instance.PlaySoundEffect(SoundsEffectsRepository.GetEnemySoundEffect(ActionType.EnemyAction.Attack));
        }
    }
}
