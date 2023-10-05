using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class DefeatedMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button buttonPlayAgain;
        [SerializeField] private Button buttonQuit;

        private void Awake()
        {
            buttonPlayAgain.onClick.AddListener(Play);
            buttonQuit.onClick.AddListener(Quit);
        }

        private void Play()
        {
            GameManager.Instance.RestartLevel();
        }

        private void Quit()
        {
            GameManager.Instance.QuitLevel();
        }

        private void OnDestroy()
        {
            buttonPlayAgain.onClick.RemoveListener(Play);
            buttonQuit.onClick.RemoveListener(Quit);
        }
    }
}
