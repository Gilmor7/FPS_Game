using System;
using System.Collections.Generic;
using Common;
using DataTypes;
using Managers;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FirstPersonController _fpsController;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private List<Weapon> _weapons;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    
    [Header("Player Configurations")]
    [SerializeField] private float _zoomInRotationsSpeed = 0.8f;
    private float _zoomOutRotationsSpeed;

    private void Start()
    {
        _zoomOutRotationsSpeed = _fpsController.RotationSpeed;
        EventManager.Instance.OnAmmoAmountChanged += HandleAmmoAmountChanged;
        EventManager.Instance.OnPlayerHealthDamageTaken += HandleHealthDamageTaken;
        SetActiveWeapon();
    }

    public void FireButtonClicked()
    {
        if (_currentWeapon.CanShoot)
        {
            _currentWeapon.Shoot();
        }
    }

    public void ZoomButtonClicked()
    {
        if (_currentWeapon.IsWeaponCanZoom())
        {
            bool isZoomedIn = _currentWeapon.ToggleZoom();
            _fpsController.RotationSpeed = isZoomedIn ? _zoomInRotationsSpeed : _zoomOutRotationsSpeed;
        }
    }

    public void WeaponSelectionButtonClicked(int index)
    {
        int previousWeaponIndex = _currentWeaponIndex;

        if (index >= 0 && index <= _weapons.Count - 1)
        {
            _currentWeaponIndex = index;
        }

        if (previousWeaponIndex != _currentWeaponIndex)
        {
            SetActiveWeapon();
        }
    }
    
    public void ScrollInputDetected(float scrollWheelInput)
    {
        int previousWeaponIndex = _currentWeaponIndex;
        
        ProcessScrollWheelInput(scrollWheelInput);

        if (previousWeaponIndex != _currentWeaponIndex)
        {
            SetActiveWeapon();
        }
    }

    private void ProcessScrollWheelInput(float scrollInput)
    {
        if (scrollInput < 0)
        {
            if (_currentWeaponIndex >= _weapons.Count - 1)
            {
                _currentWeaponIndex = 0;
            }
            else
            {
                _currentWeaponIndex++;
            }
        }

        if (scrollInput > 0)
        {
            if (_currentWeaponIndex <= 0)
            {
                _currentWeaponIndex = _weapons.Count - 1;
            }
            else
            {
                _currentWeaponIndex--;
            }
        }
    }

    private void SetActiveWeapon()
    {
        int currentIndex = 0;
        
        foreach (Weapon weapon in _weapons)
        {
            if (_currentWeaponIndex == currentIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            currentIndex++;
        }

        _currentWeapon = _weapons[_currentWeaponIndex];
        EventManager.Instance.PublishAmmoAmountChanged();
    }

    private void HandleAmmoAmountChanged()
    {
        GameManager.Instance.UpdateAmmoAmountDisplay(_currentWeapon.AmmoAmount);
    }

    private void HandleHealthDamageTaken(float hp)
    {
        AudioManager.Instance.PlaySoundEffect(
            _audioSource, SoundsEffectsRepository.GetPlayerSoundEffect(ActionType.PlayerAction.GetHurt));
        GameManager.Instance.HandlePlayerTakeDamage(hp);
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnAmmoAmountChanged -= HandleAmmoAmountChanged;
        EventManager.Instance.OnPlayerHealthDamageTaken -= HandleHealthDamageTaken;
    }
}
