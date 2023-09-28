using System;
using Common;
using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        // Define custom events
        public event Action OnAmmoAmountChanged;
        public event Action OnPlayerHealthDamageTaken;
        public event Action<IDamageable, IHealthSystem> OnCharacterGotHit;

        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Publish events
        public void PublishAmmoAmountChanged()
        {
            OnAmmoAmountChanged?.Invoke();
        }
        
        public void PublishCharacterGotHit(IDamageable attacker, IHealthSystem characterHealthSystem)
        {
            OnCharacterGotHit?.Invoke(attacker, characterHealthSystem);
        }

        public void PublishPlayerHealthDamageTaken()
        {
            OnPlayerHealthDamageTaken?.Invoke();
        }
    }
}
