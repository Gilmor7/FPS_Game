using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] _ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType AmmoType;
        public int AmmoAmount;
    }

    public void ReduceCurrentAmount(AmmoType ammoType)
    {
        if (!IsSlotEmpty(ammoType))
        {
            GetAmmoSlotByType(ammoType).AmmoAmount -= 1;
        }
        else
        {
            throw new Exception("You are trying to reduce ammo from an empty slot");
        }
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
