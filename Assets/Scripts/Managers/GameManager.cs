using System;
using Common;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public static GameManager Instance { get; private set; }
        public string EnemyTag => "Enemy"; 
        public string PlayerTag => "Player"; 

        protected void Awake()
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

        private void Start()
        {
            FPSRaycaster.FpCamera = _camera;
        }

        public void EnemyGotHit(Weapon weapon, EnemyHealth enemyHealth)
        {
            if (enemyHealth != null)
            {
                bool shouldDestroy = enemyHealth.TakeDamage(weapon.Damage);

                if (shouldDestroy)
                {
                    enemyHealth.Die();
                }
            }
        }

        //TODO: Find good way to prevent code duplication between PlayerGotHit and EnemyGotHit - It may be related to
        // a generic health system class to both enemy and player that uses a controller reference to invoke die handler
        // idea - create an interface with die method and both controllers will implement the die method
        // Also both weapon and enemyAttack will implement IDamageable with Damage prop and methods if necessary
        public void PlayerGotHit(EnemyAttack enemyAttack, PlayerHealth playerHealth)
        {
            if (playerHealth != null)
            {
                bool shouldDestroy = playerHealth.TakeDamage(enemyAttack.Damage);

                if (shouldDestroy)
                {
                    playerHealth.Die();
                }
            }
        }

        private void OnDestroy()
        {
            FPSRaycaster.FpCamera = null;
        }
    }
}
