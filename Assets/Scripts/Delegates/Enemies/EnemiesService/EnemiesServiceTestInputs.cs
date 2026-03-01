using Navigation.Controllers;

using UnityEngine;

namespace Delegates.Enemies.EnemiesService
{
    public class EnemiesServiceTestInputs : IUpdatable
    {
        private readonly EnemiesService _service;

        public EnemiesServiceTestInputs(EnemiesService service)
        {
            _service = service;
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
            }
        }
    }
}