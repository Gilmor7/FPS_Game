using System;
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
