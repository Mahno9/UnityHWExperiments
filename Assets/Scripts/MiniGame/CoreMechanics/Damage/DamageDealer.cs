using UnityEngine;

namespace MiniGame.CoreMechanics.Damage
{
    public class DamageDealer : IDamageDealer
    {
        private readonly TeamId _friendTeamId;
        private readonly float  _damage;

        public DamageDealer(float damage, TeamId friendTeamId)
        {
            _friendTeamId = friendTeamId;
            _damage = damage;
        }

        public bool Damage(IDamageable target)
        {
            if (IsFriend(target))
                return false;

            target.TakeDamage(_damage);
            return true;
        }

        private bool IsFriend(IDamageable target) => target.GetTeamId() == _friendTeamId;
    }
}