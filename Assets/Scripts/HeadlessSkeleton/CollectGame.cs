using System;
using UnityEngine;

public class CollectGame : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private CollectGameStatusUpdater _statusUpdater;
    [SerializeField] private float _gameTime = 60f;

    private int _totalCollectablesCount;

    private int UncollectedCount => _collector.GetUncollected().Length;

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
        _gameTime -= Time.deltaTime;
    }

    private void UpdateTimer()
    {
        _statusUpdater.UpdateTimer(_gameTime);
    }

    private void UpdateStatus()
    {
        if (IsLose())
        {
            _statusUpdater.UpdateStatusLose();
            return;
        }

        if (IsWin())
        {
            _statusUpdater.UpdateStatusWin();
            _statusUpdater.UpdateTimer(0);
            return;
        }

        _statusUpdater.UpdateStatusWithCount(UncollectedCount, _totalCollectablesCount);
    }

    private bool IsWin()
    {
        return UncollectedCount == 0;
    }

    private bool IsLose()
    {
        return _gameTime <= 0;
    }
}
