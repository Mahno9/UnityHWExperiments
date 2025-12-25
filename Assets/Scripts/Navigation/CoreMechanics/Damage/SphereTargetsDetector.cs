using System.Collections.Generic;

using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Damage.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Damage
{
    public class SphereTargetsDetector : ITargetsDetector
    {
        private readonly Vector3 _position;
        private readonly float   _radius;

        public SphereTargetsDetector(Vector3 position, float radius)
        {
            _position = position;
            _radius = radius;
        }

        public IDamageable[] GetTargets()
        {
            Collider[] colliders = Physics.OverlapSphere(_position, _radius);

            List<IDamageable> result = new();
            foreach (Collider foundCollider in colliders)
                if (foundCollider.transform.TryGetComponent(out IDamageable target))
                    result.Add(target);

            return result.ToArray();
        }
    }
}