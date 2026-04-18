namespace MiniGame.CoreMechanics.Damage
{
    public class DamageDealer : IDamageDealer
    {
        private readonly float  _damage;
        private readonly TeamId _friendTeamId;

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

        private bool IsFriend(IDamageable target)
        {
            return target.GetTeamId() == _friendTeamId;
        }
    }
}