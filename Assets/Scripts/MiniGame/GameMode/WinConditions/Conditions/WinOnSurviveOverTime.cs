using Delegates.Timer;

namespace MiniGame.WinConditions
{
    public class WinOnSurviveOverTime : WinConditionBase
    {
        private readonly TimerService _surviveTimer;

        public WinOnSurviveOverTime(float surviveTime)
        {
            _surviveTimer = new TimerService();
            _surviveTimer.OnTimerStopped += TriggerWin;
            _surviveTimer.StartTimer(surviveTime, true);
        }

        public override void Update(float deltaTime)
        {
            _surviveTimer.Update(deltaTime);
        }
    }
}