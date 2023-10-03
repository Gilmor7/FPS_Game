using Managers;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag(GameManager.Instance.PlayerTag))
      {
         EventManager.Instance.PublishLevelCompleted();
      }
   }
}
