using Common;
using Cysharp.Threading.Tasks;
using DataTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const int NumOfLevels = 3;
        
        public static GameManager Instance { get; private set; }
        public string EnemyTag => "Enemy"; 
        public string PlayerTag => "Player";
        public bool IsPlaying { get; private set; } = true;

        private int _currentLevel = 1;

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                EventManager.Instance.OnCharacterGotHit += HandleCharacterGotHit;
                EventManager.Instance.OnLevelCompleted += HandleLevelCompletion;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private async void PlayStartingGameSfx()
        {
            await UniTask.Delay(1000);
            AudioManager.Instance.PlaySoundEffect(SoundsEffectsRepository.GetEnemySoundEffect(ActionType.EnemyAction.CreepyLaugh));
        }

        private void HandleCharacterGotHit(IDamageable attacker, IHealthSystem healthSystem)
        {
            if (healthSystem != null)
            {
                bool shouldDie = healthSystem.TakeDamage(attacker.Damage);

                if (shouldDie)
                {
                    healthSystem.Die();
                }
            }
        }

        public void UpdateAmmoAmountDisplay(int ammoAmount)
        {
            PlayerHUDManager.Instance.SetAmmoAmountDisplay(ammoAmount);
        }

        public async void HandlePlayerTakeDamage()
        {
            PlayerHUDManager.Instance.DisplayPlayerDamagedScreen();
            await UniTask.Delay(300);
            PlayerHUDManager.Instance.HidePlayerDamagedScreen();
        }
        
        public void HandlePlayerDeath()
        {
            PlayerHUDManager.Instance.SetGameOverScreen();
            StopGame();
        }

        public void MainMenuStart()
        {
            _currentLevel = 1;
            SceneManager.LoadSceneAsync(_currentLevel);
            StartGame();
        }

        private void HandleLevelCompletion()
        {
            StopGame();

            if (_currentLevel < NumOfLevels)
            {
                _currentLevel++;
                StartNewLevel(_currentLevel);
            }
            else
            {
                SceneManager.LoadSceneAsync(0);
            }
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
            PlayStartingGameSfx();
        }

        private void StartNewLevel(int levelNumber)
        {
            SceneManager.LoadScene(levelNumber);
            PlayerHUDManager.Instance.SetNewLevelScreen();
            StartGame();
        }

        public void RestartLevel()
        {
            StartNewLevel(_currentLevel);
        }

        public void QuitLevel()
        {
            SceneManager.LoadSceneAsync(0); //Get back to main menu
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnCharacterGotHit -= HandleCharacterGotHit;
            EventManager.Instance.OnLevelCompleted -= HandleLevelCompletion;
        }
    }
}
