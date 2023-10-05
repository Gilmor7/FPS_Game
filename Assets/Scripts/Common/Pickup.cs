using DataTypes;
using Managers;
using UnityEngine;

namespace Common
{
    public abstract class Pickup : MonoBehaviour
    {
        [Header("Floating Configurations")]
        [SerializeField] private float _floatingHeight = 0.02f; // The maximum height the object will float
        [SerializeField] private float _floatingSpeed = 8f; // The speed at which the object will float

        private float _startY; // The initial Y position of the object

        private void Start()
        {
            _startY = transform.position.y;
        }

        private void Update()
        {
            Vector3 position = transform.position;
            float newY = _startY + Mathf.Sin(Time.time * _floatingSpeed) * _floatingHeight;

            position = new Vector3(position.x, newY, position.z);
            transform.position = position;
        }
        
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
