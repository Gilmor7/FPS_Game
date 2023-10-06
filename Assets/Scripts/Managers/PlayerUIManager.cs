using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerUIManager : MonoBehaviour
    {
        private const string AmmoCountDisplayObject = "AmmoCountText";
        private const string HpCountDisplayObject = "HPCountText";
        private const string LevelDisplayObject = "LevelNumText";
        
        [Header("Canvas")]
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _weaponReticleCanvas; 
        [SerializeField] private Canvas _HudCanvas;
        [SerializeField] private Canvas _playerDamagedDisplayCanvas;

        private TextMeshProUGUI _ammoCountDisplayText;
        private TextMeshProUGUI _hpCountDisplayText;
        private TextMeshProUGUI _levelNumberDisplayText;

        public static PlayerUIManager Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            _gameOverCanvas.gameObject.SetActive(false);
            _playerDamagedDisplayCanvas.gameObject.SetActive(false);
            InitializeUIElementReferences();
        }

        private void InitializeUIElementReferences()
        {
            _ammoCountDisplayText = _HudCanvas.transform.Find(AmmoCountDisplayObject).GetComponent<TextMeshProUGUI>();
            _hpCountDisplayText = _HudCanvas.transform.Find(HpCountDisplayObject).GetComponent<TextMeshProUGUI>();
            _levelNumberDisplayText = _HudCanvas.transform.Find(LevelDisplayObject).GetComponent<TextMeshProUGUI>();
        }

        public void SetGameOverScreen()
        {
            HideGamePlayUI();
            _gameOverCanvas.gameObject.SetActive(true);
        }

        public void SetNewLevelScreen()
        {
            DisplayGameplayUI();
            _gameOverCanvas.gameObject.SetActive(false);
            _playerDamagedDisplayCanvas.gameObject.SetActive(false);
        }

        private void DisplayGameplayUI()
        {
            _weaponReticleCanvas.gameObject.SetActive(true);
            _HudCanvas.gameObject.SetActive(true);
        }
        
        public void HideGamePlayUI()
        {
            _weaponReticleCanvas.gameObject.SetActive(false);
            _HudCanvas.gameObject.SetActive(false);
        }
        
        public void SetAmmoAmountDisplay(int ammoAmount)
        {
            _ammoCountDisplayText.text = ammoAmount.ToString();
        }
        
        public void SetHpAmountDisplay(float hp)
        {
            int hpInt = (int)hp;
            _hpCountDisplayText.text = hpInt.ToString();
        }
        
        public void SetLevelDisplay(int levelNum)
        {
            _levelNumberDisplayText.text = levelNum.ToString();
        }
        
        public void DisplayPlayerDamagedScreen()
        {
            _playerDamagedDisplayCanvas.gameObject.SetActive(true);
        }
        
        public void HidePlayerDamagedScreen()
        {
            _playerDamagedDisplayCanvas.gameObject.SetActive(false);
        }
    }
}
