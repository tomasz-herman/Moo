using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    public GameWorld gameWorld;

    private GameObject gun;
    private bool isShooting;

    void Start()
    {
        shooting = GetComponent<Shooting>();

        gun = GameObject.Find("Gun");
    }

    void Update()
    {
        if (!Application.isFocused) return;
        if (gameWorld.IsPaused()) return;

        if (isShooting)
        {
            shooting.TryShoot(gameObject, gun.transform.position/*+gun.transform.forward/2*/, gameObject.transform.forward);
        }
    }
    
    void OnSelectWeapon(InputValue nextWeaponValue)
    {
        float next = nextWeaponValue.Get<float>();

        if (next > 0) shooting.PrevWeapon();
        else if (next < 0) shooting.NextWeapon();
    }
    
    void OnSelectMachineGun()
    {
        shooting.SelectWeapon(WeaponType.MachineGun);
    }
    
    void OnSelectShotgun()
    {
        shooting.SelectWeapon(WeaponType.Shotgun);
    }
    
    void OnSelectPistol()
    {
        shooting.SelectWeapon(WeaponType.Pistol);
    }
    
    void OnSelectSword()
    {
        shooting.SelectWeapon(WeaponType.Sword);
    }
    
    void OnSelectGrenade()
    {
        shooting.SelectWeapon(WeaponType.GrenadeLauncher);
    }

    void OnFire()
    {
        isShooting = !isShooting;
    }
}
