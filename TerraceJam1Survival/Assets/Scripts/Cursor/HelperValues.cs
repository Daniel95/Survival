using System;
using UnityEngine;

public class HelperValues
{
    [Serializable]
    public struct RandomTime
    {
        public float randomTime => UnityEngine.Random.Range(minTime, maxTime);

        [SerializeField] [Range(0.1f, 5)] private float minTime;
        [SerializeField] [Range(1.0f, 15)] private float maxTime;
    }

    [Serializable]
    public struct RandomSpeed
    {
        public float randomSpeed => UnityEngine.Random.Range(minSpeed, maxSpeed);

        [SerializeField] [Range(1, 5)] private float minSpeed;
        [SerializeField] [Range(1, 5)] private float maxSpeed;
    }
}