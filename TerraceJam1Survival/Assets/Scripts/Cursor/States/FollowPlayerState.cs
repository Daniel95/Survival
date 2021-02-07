using System;
using UnityEngine;

public class FollowPlayerState : MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private HelperValues.RandomTime randomTime;
    [SerializeField] private float spawnRate = 1f;

    private float timer;
    private float speed;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        Vector3 directionToPlayer = (Player.GetInstance().transform.position - transform.position).normalized;

        transform.Translate(directionToPlayer * Time.deltaTime * speed);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnComplete();
        }
    }

    public void Enter()
    {
        Debug.Log("FollowPlayerState");

        timer = randomTime.randomTime;
        speed = randomSpeed.randomSpeed;
    }
}
