using System;
using UnityEngine;

public class MoveRandomPointsState : MonoBehaviour, IEnemyCursorState
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
        if(countRemaining <= 0)
        {
            if (onComplete != null)
            {
                onComplete();
            }

            return;
        }

        countRemaining--;

        Vector2 playerPosition = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer);

        float time = randomSpeed.GetTime(transform.position, playerPosition);

        transform.LeanMove(playerPosition, time).setEaseInOutBack().setOnComplete(MoveToPointRecusive);
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsState");

        countRemaining = randomCount.randomCount;

        MoveToPointRecusive();
    }
}
