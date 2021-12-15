using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatisticsSystem : MonoBehaviour
{
    //TODO change this class to handle statistics properly
    private Dictionary<StatisticType, float> statistics = new Dictionary<StatisticType, float>();

    public event EventHandler<(StatisticType type, double newValue)> StatisticUpdated;
    void Awake()
    {
        foreach(StatisticType type in Enum.GetValues(typeof(StatisticType)))
        {
            statistics.Add(type, Utils.FloatBetween(1, 100));
            StatisticUpdated?.Invoke(this, (type, statistics[type]));
        }
    }

    public void SetStatistic(StatisticType type, float newValue)
    {
        statistics[type] = newValue;
        StatisticUpdated?.Invoke(this, (type, newValue));
    }

    public IEnumerable<(StatisticType type, float value)> GetStatistics()
    {
        return statistics.Select(stat => (stat.Key, stat.Value));
    }
}
