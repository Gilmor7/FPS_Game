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
                enemyHealth.TakeDamage(weapon.Damage);
            }
        }

        private void OnDestroy()
        {
            FPSRaycaster.FpCamera = null;
        }
    }
}
