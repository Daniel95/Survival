﻿using System;
using UnityEngine;

public class MoveRandomPointsElasticState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private CursorHelper.RandomCount randomCount;
    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float maxDistanceFromPlayer = 5;

    private int countRemaining;

    public float GetSpawnRate() => spawnRate;

    public void Act() { }

    private void MoveToPointRecusive()
    {
        if (countRemaining <= 0)
        {
            if (onComplete != null)
            {
                onComplete();
            }

            return;
        }

        countRemaining--;

        Vector2 playerPosition = GetTargetAroundPlayer();
        float time = randomSpeed.GetTime(transform.position, playerPosition);

        transform.LeanMove(playerPosition, time).setEaseInElastic().setOnComplete(MoveToPointRecusive);
        //transform.LeanMove(playerPosition, 1).setEaseInElastic().setOnComplete(MoveToPointRecusive);
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsElasticState");

        countRemaining = randomCount.randomCount;

        MoveToPointRecusive();
    }

    private Vector2 GetTargetAroundPlayer()
    {
        Vector2 playerPosition = Player.GetInstance().transform.position;

        Vector2 randomPosition = playerPosition + UnityEngine.Random.insideUnitCircle * maxDistanceFromPlayer;

        return randomPosition;
    }
}
