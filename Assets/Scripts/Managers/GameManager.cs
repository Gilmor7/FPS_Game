using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public string EnemyTag => "Enemy"; 
        public string PlayerTag => "Player";
        public bool IsPlaying { get; private set; } = true;

        private int _currentLevel = 0;

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

        public void CharacterGotHit(IDamageable attacker, IHealthSystem healthSystem)
        {
            if (healthSystem != null)
            {
                bool shouldDestroy = healthSystem.TakeDamage(attacker.Damage);

                if (shouldDestroy)
                {
                    healthSystem.Die();
                }
            }
        }
        
        public void HandlePlayerDeath()
        {
            PlayerHUDManager.Instance.SetGameOverScreen();
            StopGame();
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            IsPlaying = false;
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            IsPlaying = true;
        }

        public void RestartLevel()
        {
            SceneManager.LoadSceneAsync(_currentLevel);
            PlayerHUDManager.Instance.SetNewLevelScreen();
            StartGame();
        }

        public void QuitLevel()
        {
            Application.Quit();
            //In the future goes back to Main Menu
        }
    }
}
