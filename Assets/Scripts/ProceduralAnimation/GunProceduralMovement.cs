using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProceduralMovement : MonoBehaviour
{
    [SerializeField] float ShootOffset = 0.1f;
    [SerializeField] float SpeedGun = 1f;
    [SerializeField] float SpeedBlade = 1f;
    [SerializeField] float BladeAngle = 60f;
    [SerializeField] float BodyMultiplayer = 0.5f;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform root;
    private Shooting shooting;
    private float lerp = 1;
    private float slerp = 1;
    private Vector3 localGunPosition;
    private Vector3 ShootGunPosition;
    private Vector3 localRootPosition;
    private Vector3 ShootRootPosition;
    private Quaternion SwordStart;
    private Quaternion SwordEnd;
    private Quaternion RootStart;
    private Quaternion RootEnd;
    private void Start()
    {
        shooting = gameObject.GetComponent<Shooting>();
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            shooting[type].WeaponShoot += Shoot;
        }
        localGunPosition = Gun.transform.localPosition;
        ShootGunPosition = localGunPosition - Vector3.forward * ShootOffset;
        localRootPosition = root.transform.localPosition;
        ShootRootPosition = localRootPosition - Vector3.forward * ShootOffset*BodyMultiplayer;
        SwordEnd = Gun.transform.localRotation;
        SwordStart = SwordEnd * Quaternion.AngleAxis(BladeAngle, Vector3.up);
        RootEnd = root.localRotation;
        RootStart = RootEnd * Quaternion.AngleAxis(BladeAngle*BodyMultiplayer, Vector3.up);
    }
    private void OnDestroy()
    {
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            shooting[type].WeaponShoot -= Shoot;
        }
    }
    private void FixedUpdate()
    {
        if (slerp < 1)
        {
            Gun.transform.localRotation = Quaternion.Slerp(SwordStart, SwordEnd, slerp);
            root.localRotation = Quaternion.Slerp(RootStart, RootEnd, slerp);
            slerp += Time.fixedDeltaTime * SpeedBlade;
        }
        if (lerp < 1)
        {
            Gun.transform.localPosition = Vector3.Lerp(ShootGunPosition, localGunPosition, lerp);
            root.localPosition = Vector3.Lerp(ShootRootPosition, localRootPosition, lerp);
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
