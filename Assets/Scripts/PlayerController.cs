using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FirstPersonController _fpsController;
    [SerializeField] private Weapon _currentWeapon;
    
    [Header("Player Configurations")]
    [SerializeField] private float _zoomInRotationsSpeed = 0.8f;
    private float _zoomOutRotationsSpeed;

    private void Start()
    {
        _zoomOutRotationsSpeed = _fpsController.RotationSpeed;
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
}
