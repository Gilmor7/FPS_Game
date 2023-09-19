using UnityEngine;

namespace Managers
{
    public class UserInputController : MonoBehaviour
    {
        private const string FireButton = "Fire1"; 
        
        void Update()
        {
            if (Input.GetButtonDown(FireButton))
            {
                PlayerController.Instance.Attack();
            }
        }
    }
}
