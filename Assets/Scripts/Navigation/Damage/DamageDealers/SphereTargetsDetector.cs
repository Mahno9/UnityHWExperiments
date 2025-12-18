using System.Collections.Generic;

using Navigation.Damage.Interfaces;
using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Damage.DamageDealers
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
            {
                if (foundCollider.transform.TryGetComponent(out IDamageable target))
                    result.Add(target);
            }

            return result.ToArray();
        }
    }
}