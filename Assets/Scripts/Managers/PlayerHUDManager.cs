using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerHUDManager : MonoBehaviour
    {
        private const string AmmoCountDisplayObject = "AmmoCountText";
        
        [Header("Canvas")]
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _weaponReticleCanvas; 
        [SerializeField] private Canvas _ammoDisplayCanvas;

        private TextMeshProUGUI _ammoCountDisplayText;

        public static PlayerHUDManager Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
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
            InitializeUIElementReferences();
        }

        private void InitializeUIElementReferences()
        {
            _ammoCountDisplayText = _ammoDisplayCanvas.transform.Find(AmmoCountDisplayObject).GetComponent<TextMeshProUGUI>();
        }

        public void SetGameOverScreen()
        {
            _weaponReticleCanvas.gameObject.SetActive(false);
            _ammoDisplayCanvas.gameObject.SetActive(false);
            _gameOverCanvas.gameObject.SetActive(true);
        }

        public void SetNewLevelScreen()
        {
            _gameOverCanvas.gameObject.SetActive(false);
            _ammoDisplayCanvas.gameObject.SetActive(true);
            _weaponReticleCanvas.gameObject.SetActive(true);
        }
        
        public void SetAmmoAmountDisplay(int ammoAmount)
        {
            _ammoCountDisplayText.text = ammoAmount.ToString();
        }
    }
}
