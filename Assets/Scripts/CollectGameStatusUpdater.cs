using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectGameStatusUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private string progressStatus = "Остаось собрать: {0} / {1}";
    [SerializeField] private string winStatus = "Поздравляю! Всё собрано!";
    [SerializeField] private string loseStatus = "Время вышло! Ты рассыпаешься...";
    [SerializeField] private string timeFormat = "Осталось: {0:F0} сек.";

    public void UpdateStatusWithCount(int collected, int collectableCount)
    {
        statusText.text = string.Format(progressStatus, collected, collectableCount);
    }

    public void UpdateStatusWin()
    {
        statusText.text = winStatus;
    }

    public void UpdateStatusLose()
    {
        statusText.text = loseStatus;
    }

    public void UpdateTimer(float timeElapsed)
    {
        if (timeElapsed <= 0)
            timerText.gameObject.SetActive(false);
        else
            timerText.text = string.Format(timeFormat, timeElapsed);
    }
}
