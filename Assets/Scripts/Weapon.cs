using Common;
using Managers;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DataTypes;

public class Weapon : MonoBehaviour, IDamageable
{
    [Header("Weapon Configurations")]
    [SerializeField] private Camera _raySourceCamera;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _timeBetweenShots = 0.5f;
    [SerializeField] private Ammo _ammoSlot;
    [SerializeField] private AmmoType _ammoType;
    private bool _canShoot = true;
    
    [Header("Weapon Components")] 
    [SerializeField] private AudioSource _audioSource;
    private WeaponZoom _weaponZoom;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _hitEffect;
    
    public float Damage => _damage;
    public bool CanShoot => _canShoot;
    public int AmmoAmount => _ammoSlot.GetCurrentAmount(_ammoType);

    private void Start()
    {
        _weaponZoom = GetComponentInParent<WeaponZoom>();
    }
    
    private void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundEffect(_audioSource,
                SoundsEffectsRepository.GetWeaponSoundEffect(_weaponType, ActionType.WeaponAction.Reload));
        }
        _canShoot = true;
    }

    public async void Shoot()
    {
        _canShoot = false;
        
        if (_ammoSlot.IsSlotEmpty(_ammoType) == false)
        {
            AudioManager.Instance.PlaySoundEffect(_audioSource,
                SoundsEffectsRepository.GetWeaponSoundEffect(_weaponType, ActionType.WeaponAction.Fire));
            PlayMuzzleFlush();
            ShootRaycast();
            _ammoSlot.ReduceCurrentAmount(_ammoType);
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(_audioSource,
                SoundsEffectsRepository.GetWeaponSoundEffect(_weaponType, ActionType.WeaponAction.FireNoAmmo));
        }

        await UniTask.Delay((int)(_timeBetweenShots * 1000));
        _canShoot = true;
    }

    public bool ToggleZoom()
    {
        bool zoomInStatus;
        
        if (_weaponZoom != null)
        {
            _weaponZoom.ToggleZoom();
            zoomInStatus = _weaponZoom.IsZoomedIn;
        }
        else
        {
            zoomInStatus = false;
        }

        return zoomInStatus;
    }

    public bool IsWeaponCanZoom()
    {
        return _weaponZoom != null;
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
                EventManager.Instance.PublishCharacterGotHit(this,
                    hit.transform.gameObject.GetComponent<EnemyHealth>());
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
