using System;
using System.Collections.Generic;

using Navigation.Controllers;

namespace MiniGame
{
    public class ControllersUpdaterService : IUpdatable
    {
        private readonly List<(ControllerBase controller, Func<bool> shouldRemove)> _entries = new();

        public void Update(float deltaTime)
        {
            for (int i = _entries.Count - 1; i >= 0; i--)
            {
                if (_entries[i].shouldRemove())
                {
                    _entries.RemoveAt(i);
                    continue;
                }

                _entries[i].controller.Update(deltaTime);
            }
        }

        public void Add(ControllerBase controller, Func<bool> shouldRemove)
        {
            controller.Enable();
            _entries.Add((controller, shouldRemove));
        }
    }
}