using System;
using UnityEngine;

public class MoveRandomPointsState : MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private int count = 4;
    [SerializeField] private float spawnRate = 0.3f;

    private float speed;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        OnComplete();
    }

    public void Enter()
    {
        Debug.Log("MoveRandomPointsState");

        speed = randomSpeed.randomSpeed;
    }
}
