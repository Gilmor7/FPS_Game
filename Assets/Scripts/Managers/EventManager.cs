using System;
using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        // Define custom events
        public event Action OnAmmoAmountChanged;
        // public event Action OnHealthChanged;
        // public event Action OnCharacterGotHit;

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

        // public void PublishHealthChanged()
        // {
        //     OnHealthChanged?.Invoke();
        // }
        //
        // public void PublishCharacterGotHit()
        // {
        //     OnCharacterGotHit?.Invoke();
        // }
    }
}
