using System.Collections.Generic;

using Navigation.Controllers;

namespace MiniGame
{
    public class UpdaterService : IUpdatable
    {
        private readonly List<IUpdatable> _updatableList = new();

        public void Update(float deltaTime)
        {
            foreach (IUpdatable updatable in _updatableList)
                updatable.Update(deltaTime);
        }

        public void Add(IUpdatable updatable)
        {
            _updatableList.Add(updatable);
        }
    }
}