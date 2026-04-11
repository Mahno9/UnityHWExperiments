using System;

using Navigation.Characters.Interfaces;

using UnityEngine;

using Object = UnityEngine.Object;

namespace MiniGame.CoreMechanics.Shooting
{
    public class Shooter
    {
        private readonly Projectile _projectilePrefab;

        public Shooter(Projectile projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;
        }

        public void Shoot(Transform muzzle, params IDamageable[] friends)
        {
            Object.Instantiate(_projectilePrefab, muzzle.position, muzzle.rotation).Initialize(friends);
        }
    }
}