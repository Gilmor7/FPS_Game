using Managers;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Configurations")]
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private Camera _raySourceCamera;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _hitEffect;

    public float Damage => _damage;

    public void Shoot()
    {
        PlayMuzzleFlush();
        ShootRaycast();
    }

    private void PlayMuzzleFlush()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play();
        }
    }

    private void ShootRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(_raySourceCamera.transform.position, _raySourceCamera.transform.forward, out hit, _range))
        {
            CreateHitEffect(hit);
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

    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject effect = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 0.1f);
    }
}
