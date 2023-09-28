using System;
using Managers;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] _ammoSlots;

    [Serializable]
    private class AmmoSlot
    {
        public AmmoType AmmoType;
        public int AmmoAmount;
    }

    public int GetCurrentAmount(AmmoType ammoType)
    {
        return GetAmmoSlotByType(ammoType).AmmoAmount;
    }
    
    public void ReduceCurrentAmount(AmmoType ammoType)
    {
        if (!IsSlotEmpty(ammoType))
        {
            GetAmmoSlotByType(ammoType).AmmoAmount -= 1;
            EventManager.Instance.PublishAmmoAmountChanged();
        }
        else
        {
            throw new Exception("You are trying to reduce ammo from an empty slot");
        }
    }
    
    public void IncreaseCurrentAmount(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlotByType(ammoType).AmmoAmount += ammoAmount;
        EventManager.Instance.PublishAmmoAmountChanged();
    }
    
    public bool IsSlotEmpty(AmmoType ammoType)
    {
        int amount = GetAmmoSlotByType(ammoType).AmmoAmount;
        return amount == 0;
    }

    private AmmoSlot GetAmmoSlotByType(AmmoType ammoType)
    {
        AmmoSlot resultSlot = null;
        
        foreach (AmmoSlot slot in _ammoSlots)    
        {
            if (slot.AmmoType == ammoType)
            {
                resultSlot = slot;
            }
        }

        if (resultSlot == null)
        {
            throw new ArgumentException($"There is no slot for AmmoType: {ammoType}");
        }

        return resultSlot;
    }
}
