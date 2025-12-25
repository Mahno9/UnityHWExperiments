namespace Navigation.Damage.Interfaces
{
    public interface IHealthChangeSubscriber
    {
        void DamageTaken(float damage);
    }
}