using System;
using UnityEngine;

namespace Managers
{
    public class UserInputController : MonoBehaviour
    {
        private const string FireButton = "Fire1";

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
        }
    }
}
