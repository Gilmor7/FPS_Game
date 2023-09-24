using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UserInputController : MonoBehaviour
    {
        private const string FireButton = "Fire1";
        private const int WeaponZoomButton = (int)MouseButton.RightMouse;

        private PlayerController _playerController;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag(GameManager.Instance.PlayerTag);
            _playerController = player.GetComponent<PlayerController>();
        }

        void Update()
        {
            if (Input.GetButtonDown(FireButton))
            {
                _playerController.FireButtonClicked();
            }
            
            else if (Input.GetMouseButtonDown(WeaponZoomButton))
            {
                _playerController.ZoomButtonClicked();
            }
        }
    }
}
