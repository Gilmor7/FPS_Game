using Managers;
using UnityEngine;

namespace Common
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameManager.Instance.PlayerTag))
            {
                HandlePickupAction();
                Destroy(gameObject);
                //Make pickup sound effect
            }
        }

        protected abstract void HandlePickupAction();
    }
}
