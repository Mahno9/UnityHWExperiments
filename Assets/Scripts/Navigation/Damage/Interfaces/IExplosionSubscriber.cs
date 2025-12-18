using Navigation.Interfaces;

namespace Navigation.Damage.Interfaces
{
    public interface IExplosionSubscriber
    {
        void Exploded(IDamageable[] targets, float[] damage);
    }
}