using System;
using UnityEngine;

public class MoveAwayFromPlayerState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private CursorHelper.RandomTime randomTime;
    [SerializeField] private float spawnRate = 0.3f;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        //transform.Translate(directionFromPlayer * Time.deltaTime * speed);

        //timer -= Time.deltaTime;
        //if (timer <= 0)
        //{
        //    if (onComplete != null)
        //    {
        //        onComplete();
        //    }
        //}
    }

    public void Enter()
    {
        //Debug.Log("MoveAwayFromPlayerState");

        var timer = randomTime.randomTime;
        var speed = randomSpeed.randomSpeed;

        Vector3 directionFromPlayer = (transform.position - Player.GetInstance().transform.position).normalized;

        Vector3 randomPosition = directionFromPlayer * randomTime.randomTime;

        float time = randomSpeed.GetTime(transform.position, randomPosition);
        transform.LeanMove(randomPosition, time).setEaseInOutBack().setOnComplete(() =>
        {
            if (onComplete != null)
            {
                onComplete();
            }
        });
    }
}
