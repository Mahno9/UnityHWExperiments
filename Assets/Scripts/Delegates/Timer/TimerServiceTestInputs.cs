using Navigation.Controllers;

using UnityEngine;

namespace Delegates.Timer
{
    public class TimerServiceTestInputs : IUpdatable
    {
        private readonly TimerService _service;

        public TimerServiceTestInputs(TimerService service)
        {
            _service = service;
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                int timerTime = Random.Range(5, 10);
                _service.StartTimer(timerTime, true);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _service.StopTimer();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_service.IsStarted)
                    _service.PauseTimer();
                else
                    _service.ResumeTimer();
            }
        }
    }
}