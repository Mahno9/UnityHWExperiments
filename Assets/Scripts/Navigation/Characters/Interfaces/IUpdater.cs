using Navigation.Controllers;

namespace Delegates.Enemies.Enemy
{
    public interface IUpdater
    {
        void AddUpdatable(IUpdatable updatable);
        void Update(float            deltaTime);
    }
}