using Managers;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Configurations")]
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 25f;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;

    public float Damage => _damage;

    public void Shoot()
    {
        PlayMuzzleFlush();
        ShootARaycast();
    }

    private void PlayMuzzleFlush()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play();
        }
    }

    private void ShootARaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPSRaycaster.GenerateRay(), out hit, _range))
        {
            Debug.Log("I hit this thing: " + hit.transform.name);
            bool hitAnEnemy = hit.transform.gameObject.CompareTag(GameManager.Instance.EnemyTag);

            if (hitAnEnemy)
            {
                GameManager.Instance.EnemyGotHit(this, hit.transform.gameObject.GetComponent<EnemyHealth>());
            }
        }
        else
        {
            return;
        }
    }
}
