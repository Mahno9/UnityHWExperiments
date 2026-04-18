using Delegates.Timer;

using UnityEngine;

namespace MiniGame.WinConditions
{
    public class WinOnSurviveOverTime : WinConditionBase
    {
        [SerializeField] private float _surviveTime;

        private readonly TimerService _surviveTimer;

        public WinOnSurviveOverTime()
        {
            _surviveTimer = new TimerService();
            _surviveTimer.OnTimerStopped += TriggerWin;
        }

        public override void Init(WinInitData data)
        {
            base.Init(data);
            _surviveTimer.StartTimer(_surviveTime, true);
        }

        public override void Update(float deltaTime)
        {
            _surviveTimer.Update(deltaTime);
        }
    }
}