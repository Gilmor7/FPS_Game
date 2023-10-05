using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private Canvas canvasMenu;
        [SerializeField] private Canvas canvasInstructions;

        [Header("Buttons")]
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonInstructions;
        [SerializeField] private Button buttonQuit;
        [SerializeField] private Button buttonBack;

        private void Awake()
        {
            buttonPlay.onClick.AddListener(Play);
            buttonInstructions.onClick.AddListener(DisplayInstructions);
            buttonQuit.onClick.AddListener(Quit);
            buttonBack.onClick.AddListener(DisplayMenu);
            
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            canvasInstructions.gameObject.SetActive(false);
            canvasMenu.gameObject.SetActive(true);
        }

        private void DisplayInstructions()
        {
            canvasMenu.gameObject.SetActive(false);
            canvasInstructions.gameObject.SetActive(true);
        }

        private void Play()
        {
            GameManager.Instance.MainMenuStart();
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            buttonPlay.onClick.RemoveListener(Play);
            buttonInstructions.onClick.RemoveListener(DisplayInstructions);
            buttonQuit.onClick.RemoveListener(Quit);
            buttonBack.onClick.RemoveListener(DisplayMenu);
        }
    }
}
