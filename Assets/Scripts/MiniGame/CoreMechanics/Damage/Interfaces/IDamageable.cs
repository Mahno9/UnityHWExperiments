namespace MiniGame.CoreMechanics.Damage
{
    public interface IDamageable
    {
        void   TakeDamage(float damage);
        TeamId GetTeamId();
    }
}