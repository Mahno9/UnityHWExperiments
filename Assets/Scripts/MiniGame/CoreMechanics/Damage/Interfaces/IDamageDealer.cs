namespace MiniGame.CoreMechanics.Damage
{
    public interface IDamageDealer
    {
        bool Damage(IDamageable target);
    }
}