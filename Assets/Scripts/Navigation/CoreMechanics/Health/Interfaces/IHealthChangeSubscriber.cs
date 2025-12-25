namespace Navigation.CoreMechanics.Health.Interfaces
{
    public interface IHealthChangeSubscriber
    {
        void DamageTaken(float damage);
    }
}