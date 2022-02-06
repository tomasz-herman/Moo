using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles
{
    public class SwordReflectsEnemyProjectilesUpgradeHandler : IOneTimeProjectileUpgradeHandler
    {
        //it looks fun when reflected projectile is faster
        private float _projectileSpeedModifier = 1.5f;

        private float _swordLength;
        private float _swordLengthOver2;
        private float _swordLengthSquared;

        private float _startAngleDeg;
        private float _stopAngleDeg;
        private float _rangeAngleDegOver2;
        private float _cosRangeAngleDegOver2;
        private float _rayCastSphereRadius;

        private Vector3 _bladeDirection;
        private Vector3 _playerDirection;
        private Vector3 _rayCastSphereCenter;

        private bool _launched;

        private readonly Sword _sword;
        private readonly HashSet<ProjectileBase> _reflectedProjectiles = new HashSet<ProjectileBase>();

        public SwordReflectsEnemyProjectilesUpgradeHandler(Sword sword)
        {
            _sword = sword;
            if (!_sword.BladeUpgrades.OfType<SwordReflectsEnemyProjectilesUpgradeHandler>().Any())
            {
                _sword.BladeUpgrades.Add(this);
            }
        }

        public void OnUpdate(ProjectileBase projectile)
        {
            if (!_launched) return;

            _bladeDirection = projectile.transform.forward;
            _playerDirection = projectile.Owner.transform.forward;
            var position = projectile.transform.position;

            position -= _swordLengthOver2 * _bladeDirection;

            _rayCastSphereCenter = position + _rayCastSphereRadius * _playerDirection;

            var projectileLayer = Layers.GetLayer(Layers.Projectile);
            int layerMask = 1 << projectileLayer;

            var colliders = Physics.OverlapSphere(_rayCastSphereCenter, _rayCastSphereRadius, layerMask);

            foreach (var collider in colliders)
            {
                if (collider.gameObject != projectile.gameObject)
                {
                    var offset = collider.transform.position - position;
                    var distSquared = Vector3.SqrMagnitude(offset);

                    if (distSquared < _swordLengthSquared)
                    {
                        var otherProjectile = collider.attachedRigidbody.GetComponent<ProjectileBase>();

                        if (otherProjectile != null)
                        {
                            if (!_reflectedProjectiles.Contains(otherProjectile))
                            {
                                _reflectedProjectiles.Add(otherProjectile);

                                var direction = otherProjectile.Owner.transform.position - otherProjectile.transform.position;

                                //TODO: do sth with that
                                direction += Vector3.up;

                                direction.Normalize();

                                otherProjectile.Owner = projectile.Owner;

                                var otherProjectileRigidBody = otherProjectile.gameObject.GetComponent<Rigidbody>();
                                var otherProjectileSpeed = otherProjectileRigidBody.velocity.magnitude * _projectileSpeedModifier;
                                var velocity = otherProjectileSpeed * direction;
                                otherProjectileRigidBody.velocity = velocity;
                                otherProjectile.gameObject.transform.LookAt(otherProjectile.transform.position + velocity);
                            }
                        }
                    }
                }
            }
        }

        public void OnDrawGizmos(ProjectileBase projectile)
        {
            Gizmos.DrawWireSphere(_rayCastSphereCenter, _rayCastSphereRadius);
        }

        public void OnLaunch(ProjectileBase projectile)
        {
            if (projectile is Blade blade)
            {
                _launched = true;
                _swordLength = blade.length;
                _swordLengthOver2 = _swordLength / 2f;
                _swordLengthSquared = _swordLength * _swordLength;
                _startAngleDeg = blade.startAngle;
                _stopAngleDeg = blade.stopAngle;
                _rangeAngleDegOver2 = Mathf.Abs(_startAngleDeg - _stopAngleDeg) / 2f;
                _cosRangeAngleDegOver2 = Mathf.Cos(_rangeAngleDegOver2);

                _rayCastSphereRadius = _swordLengthOver2 / _cosRangeAngleDegOver2;

                _reflectedProjectiles.Clear();

                OnUpdate(projectile);
            }
        }

        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy)
        {

        }

        public void OnTerrainHit(GameObject projectile, Collider terrain)
        {

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
