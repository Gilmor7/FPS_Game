using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public void FireButtonClicked()
    {
        _currentWeapon.Shoot();
    }
}
