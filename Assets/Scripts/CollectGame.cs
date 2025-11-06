using System;
using UnityEngine;

public class CollectGame : MonoBehaviour
{
    [SerializeField] private Collector collector;
    [SerializeField] private CollectGameStatusUpdater statusUpdater;
    [SerializeField] private float gameTime = 60f;

    private int _totalCollectablesCount;

    private int UncollectedCount => collector.GetUncollected().Length;

    private void Start()
    {
        _totalCollectablesCount = UncollectedCount;
    }

    private void Update()
    {
        CountdownTimer();
        UpdateTimer();
        UpdateStatus();
    }

    private void CountdownTimer()
    {
        gameTime -= Time.deltaTime;
    }

    private void UpdateTimer()
    {
        statusUpdater.UpdateTimer(gameTime);
    }

    private void UpdateStatus()
    {
        if (IsLose())
        {
            statusUpdater.UpdateStatusLose();
            return;
        }

        if (IsWin())
        {
            statusUpdater.UpdateStatusWin();
            statusUpdater.UpdateTimer(0);
            return;
        }

        statusUpdater.UpdateStatusWithCount(UncollectedCount, _totalCollectablesCount);
    }

    private bool IsWin()
    {
        return UncollectedCount == 0;
    }

    private bool IsLose()
    {
        return gameTime <= 0;
    }
}
