using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _FPCamera;
    [SerializeField] private float _range;
    
    public void Shoot()
    {
        Physics.Raycast(_FPCamera.transform.position, _FPCamera.transform.forward, out var hit, _range);
        Debug.Log("I hit this thing: " + hit.transform.name);
    }
}
