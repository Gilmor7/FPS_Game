using UnityEngine;

namespace Managers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon;
        
        public static PlayerController Instance { get; private set; }

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

        public void Attack()
        {
            _currentWeapon.Shoot();
        }
    }
}
