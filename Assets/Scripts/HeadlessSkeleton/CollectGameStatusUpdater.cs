using TMPro;

using UnityEngine;

public class CollectGameStatusUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private string _progressStatus = "Остаось собрать: {0} / {1}";
    [SerializeField] private string _winStatus = "Поздравляю! Всё собрано!";
    [SerializeField] private string _loseStatus = "Время вышло! Ты рассыпаешься...";
    [SerializeField] private string _timeFormat = "Осталось: {0:F0} сек.";

    public void UpdateStatusWithCount(int collected, int collectableCount)
    {
        _statusText.text = string.Format(_progressStatus, collected, collectableCount);
    }

    public void UpdateStatusWin()
    {
        _statusText.text = _winStatus;
    }

    public void UpdateStatusLose()
    {
        _statusText.text = _loseStatus;
    }

    public void UpdateTimer(float timeElapsed)
    {
        if (timeElapsed <= 0)
            _timerText.gameObject.SetActive(false);
        else
            _timerText.text = string.Format(_timeFormat, timeElapsed);
    }
}
