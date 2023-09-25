using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UserInputController : MonoBehaviour
    {
        private const int FireButton = (int)MouseButton.LeftMouse;
        private const int WeaponZoomButton = (int)MouseButton.RightMouse;
        private const KeyCode Weapon1SelectionButton = KeyCode.Alpha1;
        private const KeyCode Weapon2SelectionButton = KeyCode.Alpha2;
        private const KeyCode Weapon3SelectionButton = KeyCode.Alpha3;
        private const string ScrollWheel = "Mouse ScrollWheel";

        private PlayerController _playerController;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag(GameManager.Instance.PlayerTag);
            _playerController = player.GetComponent<PlayerController>();
        }

        void Update()
        {
            if (GameManager.Instance.IsPlaying == false)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(FireButton))
            {
                _playerController.FireButtonClicked();
            }
            
            if (Input.GetMouseButtonDown(WeaponZoomButton))
            {
                _playerController.ZoomButtonClicked();
            }
            
            if (Input.GetKeyDown(Weapon1SelectionButton))
            {
                _playerController.WeaponSelectionButtonClicked(0);
            }
            
            if (Input.GetKeyDown(Weapon2SelectionButton))
            {
                _playerController.WeaponSelectionButtonClicked(1);
            }
            
            if (Input.GetKeyDown(Weapon3SelectionButton))
            {
                _playerController.WeaponSelectionButtonClicked(2);
            }

            if (Input.GetAxis(ScrollWheel) != 0)
            {
                _playerController.ScrollInputDetected(Input.GetAxis(ScrollWheel));
            }
        }
    }
}
