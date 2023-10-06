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
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
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
            PlayerUIManager.Instance.SetAmmoAmountDisplay(ammoAmount);
        }

        public async void HandlePlayerTakeDamage(float hp)
        {
            PlayerUIManager.Instance.SetHpAmountDisplay(hp);
            PlayerUIManager.Instance.DisplayPlayerDamagedScreen();
            await UniTask.Delay(300);
            PlayerUIManager.Instance.HidePlayerDamagedScreen();
        }
        
        public void HandlePlayerDeath()
        {
            StopGame();
            PlayerUIManager.Instance.SetGameOverScreen();
        }

        public void MainMenuStart()
        {
            SceneManager.LoadScene(_currentLevel);
        }

        private void HandleLevelCompletion()
        {
            PlayerUIManager.Instance.HideGamePlayUI();
            StopGame();

            if (_currentLevel < NumOfLevels)
            {
                _currentLevel++;
                StartNewLevel(_currentLevel);
            }
            else
            {
                GetBackToMainMenu();
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
        }

        private void StartNewLevel(int levelNumber)
        {
            SceneManager.LoadSceneAsync(levelNumber);
            PlayerUIManager.Instance.SetNewLevelScreen();
        }

        public void RestartLevel()
        {
            StartNewLevel(_currentLevel);
        }

        public void QuitLevel()
        {
            GetBackToMainMenu();
        }

        private void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex > 0)
            {
                PlayerUIManager.Instance.SetLevelDisplay(_currentLevel);
                SetPlayerStartingHpDisplay();
                StartGame();
                PlayStartingGameSfx();
            }
        }

        private void SetPlayerStartingHpDisplay()
        {
            float hp = FindObjectOfType<PlayerHealth>().HP;
            PlayerUIManager.Instance.SetHpAmountDisplay(hp);
        }
        
        private async void PlayStartingGameSfx()
        {
            await UniTask.Delay(1000);
            AudioManager.Instance.PlaySoundEffect(SoundsEffectsRepository.GetEnemySoundEffect(ActionType.EnemyAction.CreepyLaugh));
        }

        private void GetBackToMainMenu()
        {
            _currentLevel = 1;
            SceneManager.LoadSceneAsync(0);
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnCharacterGotHit -= HandleCharacterGotHit;
            EventManager.Instance.OnLevelCompleted -= HandleLevelCompletion;
        }
    }
}
