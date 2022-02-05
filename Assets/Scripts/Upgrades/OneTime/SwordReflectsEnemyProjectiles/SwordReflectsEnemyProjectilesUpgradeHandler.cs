using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles
{
    public class SwordReflectsEnemyProjectilesUpgradeHandler : IOneTimeProjectileUpgradeHandler
    {
        //Adjust it to blade length
        private readonly float _sphereRadius = 2f;

        private readonly Sword _sword;
        private float _startAngleDeg;
        private float _stopAngleDeg;
        private float _rangeAngleDegOver2;
        private float _cosRangeAngleDegOver2;
        private float _capsuleRadius;

        private GameObject _owner;

        private Vector3 _direction;
        private Vector3 _cubeCenter;
        private Vector3 _point0;
        private Vector3 _point1;

        public SwordReflectsEnemyProjectilesUpgradeHandler(Sword sword)
        {
            _sword = sword;
        }

        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy)
        {

        }

        public void OnTerrainHit(GameObject projectile, Collider terrain)
        {

        }

        public void OnUpdate(ProjectileBase projectile)
        {
            _direction = projectile.transform.forward;
            var position = projectile.transform.position;

            var point0 = position + _capsuleRadius * _direction;
            var point1 = position + (_sphereRadius -_capsuleRadius) * _direction;

            _point0 = point0;
            _point1 = point1;

            _cubeCenter = position + 0.5f * _sphereRadius * _direction;

            int layermask = 1 << 10;

            var colliders = Physics.OverlapCapsule(point0, point1, _sphereRadius, layermask);

            foreach (var collider in colliders)
            {
                if (collider.gameObject != projectile.gameObject)
                {
                    Debug.Log($"W cos walnalem {colliders.Length}");
                }
            }
        }


        public void OnDrawGizmos(ProjectileBase projectile)
        {
            //Gizmos.DrawCube(_cubeCenter, 0.5f * _sphereRadius * Vector3.one);
            DrawWireCapsule(_point0, _point1, _sphereRadius);
        }

        public void OnLaunch(ProjectileBase projectile)
        {
            if (projectile is Blade blade)
            {
                _startAngleDeg = blade.startAngle;
                _stopAngleDeg = blade.stopAngle;
                _rangeAngleDegOver2 = (_startAngleDeg + _stopAngleDeg) / 2f;
                _cosRangeAngleDegOver2 = Mathf.Cos(_rangeAngleDegOver2);

                _capsuleRadius = _sphereRadius * _cosRangeAngleDegOver2;
                //_secondRadiusLength = (0.5f * _sphereRadius) / _cosRangeAngleDegOver2;
            }
        }

        public void OnDestroy(ProjectileBase projectile)
        {

        }

        public static void DrawWireCapsule(Vector3 _pos, Vector3 _pos2, float _radius, Color _color = default)
        {
            if (_color != default) Handles.color = _color;

            var forward = _pos2 - _pos;
            var _rot = Quaternion.LookRotation(forward);
            var pointOffset = _radius / 2f;
            var length = forward.magnitude;
            var center2 = new Vector3(0f, 0, length);

            Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);

            using (new Handles.DrawingScope(angleMatrix))
            {
                Handles.DrawWireDisc(Vector3.zero, Vector3.forward, _radius);
                Handles.DrawWireArc(Vector3.zero, Vector3.up, Vector3.left * pointOffset, -180f, _radius);
                Handles.DrawWireArc(Vector3.zero, Vector3.left, Vector3.down * pointOffset, -180f, _radius);
                Handles.DrawWireDisc(center2, Vector3.forward, _radius);
                Handles.DrawWireArc(center2, Vector3.up, Vector3.right * pointOffset, -180f, _radius);
                Handles.DrawWireArc(center2, Vector3.left, Vector3.up * pointOffset, -180f, _radius);

                DrawLine(_radius, 0f, length);
                DrawLine(-_radius, 0f, length);
                DrawLine(0f, _radius, length);
                DrawLine(0f, -_radius, length);
            }
        }

        private static void DrawLine(float arg1, float arg2, float forward)
        {
            Handles.DrawLine(new Vector3(arg1, arg2, 0f), new Vector3(arg1, arg2, forward));
        }
    }
}

/*
interface IUpgradeHandler
{
    OnEnemyHit(ProjectileBase projectile, Enemy enemy);
    OnTerrainHit(GameObject projectile, Collider terrain);
    OnUpdate(ProjectileBase projectile);
    OnLaunch(ProjectileBase projectile);
    // Jakieś inne potrzebne metody?
}

public abstract class ProjectileBase : Entity
{
    public List<IUpgradeHandler> upgradeHandlers = new List<IUpgradeHandler>();

    // inne pola

    protected override void Update()
    {
        // to co było

        foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnUpdate(this);
    }

    public void ApplyDamage(Collider other, float damage)
    {
        Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
        Player playerHit = other.gameObject.GetComponent<Player>();
        if (Owner != null && Owner.GetComponent<Player>() != null) // Player was shooting
        {
            if (enemyHit != null)
            {
                enemyHit.TakeDamage(damage, Owner.GetComponent<ScoreSystem>());
                foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnEnemyHit(this, enemyHit);

            }
        }
        if (Owner == null || (Owner != null && Owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
        {
            if (playerHit != null)
            {
                playerHit.healthSystem.Health -= damage;
            }
        }

        if //(/*Jakieś wykrywanie czy trafiło w teren/)
        {
            foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnTerrainHit(this, other);
        }
    }

    // Tą metodę może tu dodać bo jest w granade i bullet:
    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnLaunch(this);
    }
}
*/