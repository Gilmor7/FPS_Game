using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _amount = 10;
    public int Amount => _amount;

    public void ReduceCurrentAmount()
    {
        if (!IsEmpty())
        {
            _amount -= 1;
        }
        else
        {
            throw new Exception("You are trying to reduce ammo from an empty slot");
        }
    }

    public bool IsEmpty()
    {
        return _amount == 0;
    }
}
