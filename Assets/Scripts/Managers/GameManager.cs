using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    { 
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
    
        public void EnemyGotHit(Enemy enemy)
        {

        }
    }
}
