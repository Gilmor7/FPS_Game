using UnityEngine;

namespace Managers
{
    public class PlayerHUDManager : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _weaponReticleCanvas; 

        public static PlayerHUDManager Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                _gameOverCanvas.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetGameOverScreen()
        {
            _weaponReticleCanvas.gameObject.SetActive(false);
            _gameOverCanvas.gameObject.SetActive(true);
        }

        public void SetNewLevelScreen()
        {
            _gameOverCanvas.gameObject.SetActive(false);
            _weaponReticleCanvas.gameObject.SetActive(true);
        }
    }
}
