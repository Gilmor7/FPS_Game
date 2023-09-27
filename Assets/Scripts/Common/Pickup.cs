using DataTypes;
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
                AudioManager.Instance.PlaySoundEffect(SoundsEffectsRepository.GetPlayerSoundEffect(ActionType.PlayerAction.ItemPickup));
                Destroy(gameObject);
            }
        }

        protected abstract void HandlePickupAction();
    }
}
