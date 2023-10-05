using Common;
using DataTypes;
using Managers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private static readonly int AttackAnimationParam = Animator.StringToHash("attack");
    private static readonly int MoveAnimationTrigger = Animator.StringToHash("moveTrigger");
    private static readonly int StunnedAnimationTrigger = Animator.StringToHash("stunnedTrigger");
    private static readonly int DieAnimationTrigger = Animator.StringToHash("dieTrigger");

    [Header("Components")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [Header("Agent Config")] 
    [SerializeField] private float _chaseRange = 15f;
    [SerializeField] private float _turnSpeed = 6f;

    private Transform _targetTransform;
    private float _distanceFromTarget = Mathf.Infinity;
    private bool _isProvoked = false;
    private EnemyHealth _health;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag(GameManager.Instance.PlayerTag);
        
        _targetTransform = player.GetComponent<Transform>();
        _health = GetComponent<EnemyHealth>();
    }
    
    void Update()
    {
        if (_health.IsDead)
        {
            enabled = false;
            return;
        }
        
        _distanceFromTarget = Vector3.Distance(_targetTransform.position, transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceFromTarget <= _chaseRange)
        {
            Provoke(isProvokeByShoot: false);
        }
    }

    private void Provoke(bool isProvokeByShoot)
    {
        bool isAlreadyProvoked = _isProvoked;

        if (!isAlreadyProvoked)
        {
            ActionType.EnemyAction actionType = isProvokeByShoot
                ? ActionType.EnemyAction.ProvokedByShoot
                : ActionType.EnemyAction.ProvokedByRange;
            AudioManager.Instance.PlaySoundEffect(_audioSource, SoundsEffectsRepository.GetEnemySoundEffect(actionType));  
        }
        
        _isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
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
        _animator.SetBool(AttackAnimationParam, false);
        _animator.SetTrigger(MoveAnimationTrigger);
        _agent.SetDestination(_targetTransform.position);
    }

    private void AttackTarget()
    {
        _animator.SetBool(AttackAnimationParam, true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (_targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    }

    public void OnDamageTaken()
    {
        Provoke(isProvokeByShoot: true);
        _animator.SetTrigger(StunnedAnimationTrigger);
        AudioManager.Instance.PlaySoundEffect(_audioSource, 
            SoundsEffectsRepository.GetEnemySoundEffect(ActionType.EnemyAction.GetHurt));
    }

    public void AnimateEnemyDie()
    {
        _animator.SetTrigger(DieAnimationTrigger);
    }

    public void CorpseFallEvent()
    {
        AudioManager.Instance.PlaySoundEffect(_audioSource, 
            SoundsEffectsRepository.GetEnemySoundEffect(ActionType.EnemyAction.Die));
    }

    private void OnDisable()
    {
        _agent.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        // Display the chase range radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}
