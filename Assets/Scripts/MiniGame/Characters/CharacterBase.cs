using System.Collections.Generic;

using Navigation.Controllers;

namespace MiniGame.Characters
{
    public class CharacterBase : IUpdatable
    {
        private readonly List<ControllerBase> _controllers;

        protected CharacterBase(params ControllerBase[] controllers) => _controllers = new List<ControllerBase>(controllers);

        public void AddController(ControllerBase controller)
        {
            _controllers.Add(controller);
            controller.Enable();
        }

        public virtual void Update(float deltaTime)
        {
            foreach (ControllerBase controller in _controllers)
                controller.Update(deltaTime);
        }
    }
}