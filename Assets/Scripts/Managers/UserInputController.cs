using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UserInputController : MonoBehaviour
    {
        private const int FireButton = (int)MouseButton.LeftMouse;
        private const int WeaponZoomButton = (int)MouseButton.RightMouse;

        private PlayerController _playerController;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag(GameManager.Instance.PlayerTag);
            _playerController = player.GetComponent<PlayerController>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(FireButton))
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
