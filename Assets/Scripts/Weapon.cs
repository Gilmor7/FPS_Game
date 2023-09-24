using Cinemachine;
using Managers;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Configurations")]
    [SerializeField] private Camera _raySourceCamera;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _timeBetweenShots = 0.5f;
    [SerializeField] private Ammo _ammoSlot;
    
    [Header("Zoom Configurations")]
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] private float _zoomOutFOV = 40f;
    [SerializeField] private float _zoomInFOV = 20f;
    private bool _isZoomedIn = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _hitEffect;

    private bool _canShoot = true;
    
    public float Damage => _damage;
    public bool CanShoot => _canShoot;

    public async void Shoot()
    {
        _canShoot = false;
        
        if (_ammoSlot.IsEmpty() == false)
        {
            PlayMuzzleFlush();
            ShootRaycast();
            _ammoSlot.ReduceCurrentAmount();
        }
        else
        {
            //Play empty slot sound effect
        }

        await UniTask.Delay((int)_timeBetweenShots * 1000);
        _canShoot = true;
    }

    public bool ToggleZoom()
    {
        if (_isZoomedIn)
        {
            _isZoomedIn = false;
            _cinemachineCamera.m_Lens.FieldOfView = _zoomOutFOV;
        }
        else
        {
            _isZoomedIn = true;
            _cinemachineCamera.m_Lens.FieldOfView = _zoomInFOV;
        }

        return _isZoomedIn;
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
