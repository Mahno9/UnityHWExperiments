using UnityEngine;
using UnityEngine.UI;

namespace Delegates.Timer
{
    public class TimerServiceInitializer : MonoBehaviour
    {
        [SerializeField] private float _timerTime = 10f;

        [SerializeField] private Slider _timerSlier;

        [SerializeField] private GameObject            _statusIconPrefab;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;

        private HorizontalDiscreteStatusView _discreteView;

        private TimerService           _timerService;
        private TimerServiceTestInputs _serviceTestInputs;

        private void Awake()
        {
            _discreteView = new HorizontalDiscreteStatusView(_horizontalLayoutGroup, _statusIconPrefab);
            _discreteView.InitMaxValue((int)_timerTime);

            _timerSlier.minValue = 0f;

            _timerService = new TimerService();
            _timerService.OnTimerStarted += OnTimerStarted;
            _timerService.OnTimerUpdated.Changed += OnTimerUpdated;
            _timerService.OnTimerStopped += OnTimerStopped;

            _serviceTestInputs = new TimerServiceTestInputs(_timerService);
            OnTimerStopped();
        }


        private void OnDestroy()
        {
            _timerService.OnTimerStarted -= OnTimerStarted;
            _timerService.OnTimerUpdated.Changed -= OnTimerUpdated;
            _timerService.OnTimerStopped -= OnTimerStopped;
        }

        private void Update()
        {
            _timerService.Update(Time.deltaTime);
            _serviceTestInputs.Update(Time.deltaTime);
        }

        private void OnTimerStarted(float timeTotal)
        {
            _timerSlier.gameObject.SetActive(true);
            _timerSlier.maxValue = timeTotal;

            _discreteView.SetActive(true);
        }

        private void OnTimerUpdated(float timeOld, float timeCurrent)
        {
            _timerSlier.value = timeCurrent;

            _discreteView.UpdateStatus(timeCurrent);
        }

        private void OnTimerStopped()
        {
            _timerSlier.gameObject.SetActive(false);
            _discreteView.SetActive(false);
        }
    }
}