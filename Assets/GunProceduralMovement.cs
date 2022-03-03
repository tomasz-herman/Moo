using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProceduralMovement : MonoBehaviour
{
    [SerializeField] float ShootOffset = 0.1f;
    [SerializeField] float SpeedGun = 1f;
    [SerializeField] float SpeedBlade = 1f;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform root;
    private Shooting shooting;
    private float lerp = 1;
    private float slerp = 1;
    private Vector3 localGunPosition;
    private Vector3 ShootGunPosition;
    private Quaternion SwordStart;
    private Quaternion SwordEnd;
    private Quaternion RootStart;
    private Quaternion RootEnd;
    private Vector3 BladeStartPosition;
    private void Start()
    {
        shooting = gameObject.GetComponent<Shooting>();
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            shooting[type].WeaponShoot += Shoot;
        }
        localGunPosition = Gun.transform.localPosition;
        ShootGunPosition = localGunPosition - Vector3.forward * ShootOffset;
        SwordEnd = Gun.transform.localRotation;
        SwordStart = SwordEnd * Quaternion.AngleAxis(45, Vector3.up);
        RootEnd = root.localRotation;
        RootStart = RootEnd * Quaternion.AngleAxis(30, Vector3.up);
        BladeStartPosition = localGunPosition;
    }
    private void FixedUpdate()
    {
        if (slerp < 1)
        {
            Gun.transform.localRotation = Quaternion.Slerp(SwordStart, SwordEnd, slerp);
            Gun.transform.localPosition = Gun.transform.localRotation * BladeStartPosition;
            root.localRotation = Quaternion.Slerp(RootStart, RootEnd, slerp);
            slerp += Time.fixedDeltaTime * SpeedBlade;
        }
        if (lerp < 1)
        {
            Gun.transform.localPosition = Vector3.Lerp(ShootGunPosition, localGunPosition, lerp);

            lerp += Time.fixedDeltaTime * SpeedGun;
        }
    }

    void Shoot(object sender, (float Timeout, WeaponType type) args)
    {
        if (args.type == WeaponType.Sword)
            slerp = 0;
        else
            lerp = 0;
    }

    private void OnValidate()
    {
        ShootGunPosition = localGunPosition - Vector3.forward * ShootOffset;
    }
}
