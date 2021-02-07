using System;
using UnityEngine;

public class MoveAwayFromPlayerState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private CursorHelper.RandomTime randomTime;
    [SerializeField] private float spawnRate = 0.3f;

    private float timer;
    private float speed;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        Vector3 directionToPlayer = (transform.position - Player.GetInstance().transform.position).normalized;

        transform.Translate(directionToPlayer * Time.deltaTime * speed);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (onComplete != null)
            {
                onComplete();
            }
        }
    }

    public void Enter()
    {
        //Debug.Log("MoveAwayFromPlayerState");

        timer = randomTime.randomTime;
        speed = randomSpeed.randomSpeed;
    }
}
