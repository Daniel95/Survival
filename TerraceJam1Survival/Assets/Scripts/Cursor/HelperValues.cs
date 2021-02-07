using System;
using UnityEngine;

public class HelperValues
{
    [Serializable]
    public struct RandomTime
    {
        public float randomTime => UnityEngine.Random.Range(minTime, maxTime);

        [SerializeField] [Range(0.1f, 10)] private float minTime;
        [SerializeField] [Range(1.0f, 10)] private float maxTime;
    }

    [Serializable]
    public struct RandomSpeed
    {
        public float randomSpeed => UnityEngine.Random.Range(minSpeed, maxSpeed);

        [SerializeField] [Range(1, 10)] private float minSpeed;
        [SerializeField] [Range(1, 10)] private float maxSpeed;
    }

    [Serializable]
    public struct RandomCount
    {
        public int randomCount => UnityEngine.Random.Range(minCount, maxCount);

        [SerializeField] [Range(1, 7)] private int minCount;
        [SerializeField] [Range(1, 7)] private int maxCount;
    }
}