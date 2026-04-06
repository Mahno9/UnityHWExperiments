using UnityEngine;

namespace MiniGame
{
    public class ControllersFactory
    {
        private readonly UpdaterService _updaterService;

        public ControllersFactory(UpdaterService updaterService)
        {
            _updaterService = updaterService;
        }

        public ArrowsMoveController GetArrowsMoveController(ISimpleMovable movable, float moveSpeed)
        {
            var controller = new ArrowsMoveController(movable, moveSpeed);
            _updaterService.Add(controller);
            return controller;
        }
    }
}