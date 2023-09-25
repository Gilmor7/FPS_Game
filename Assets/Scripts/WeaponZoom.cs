using Cinemachine;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    
    [Header("Zoom Configurations")]
    [SerializeField] private float _zoomOutFOV = 40f;
    [SerializeField] private float _zoomInFOV = 20f;
    private bool _isZoomedIn = false;

    public bool IsZoomedIn => _isZoomedIn;
    
    public void ToggleZoom()
    {
        if (_isZoomedIn)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    private void ZoomIn()
    {
        _isZoomedIn = true;
        _cinemachineCamera.m_Lens.FieldOfView = _zoomInFOV;
    }
    
    private void ZoomOut()
    {
        _isZoomedIn = false;
        _cinemachineCamera.m_Lens.FieldOfView = _zoomOutFOV;
    }
}
