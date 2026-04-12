using System;

using MiniGame.CoreMechanics.Damage;

using UnityEngine;

using Object = UnityEngine.Object;

namespace MiniGame.CoreMechanics.Shooting
{
    public class Shooter
    {
        private readonly Projectile _projectilePrefab;
        private readonly float      _damage;
        private readonly TeamId     _teamId;

        public TeamId GetTeamId() => _teamId;

        public Shooter(Projectile projectilePrefab, float damage, TeamId teamId)
        {
            _projectilePrefab = projectilePrefab;
            _damage = damage;
            _teamId = teamId;
        }

        public void Shoot(Transform muzzle)
        {
            Object.Instantiate(_projectilePrefab, muzzle.position, muzzle.rotation)
                .Initialize(new DamageDealer(_damage, _teamId)
                );
        }
    }
}