using Common;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [Header("Ammo Configurations")]
    [SerializeField] private int _ammoAmount = 5;
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private Ammo _ammo;
    
    protected override void HandlePickupAction()
    {
        _ammo.IncreaseCurrentAmount(_ammoType, _ammoAmount);
    }
}
