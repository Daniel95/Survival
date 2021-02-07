using System;
using UnityEngine;

public class MoveRandomDirectionState : MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private HelperValues.RandomTime randomTime;
    [SerializeField] private float spawnRate;

    private float timer;
    private float speed;
    private Vector3 randomDirection;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        transform.Translate(randomDirection * Time.deltaTime * speed);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnComplete();
        }
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomDirectionState");

        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        timer = randomTime.randomTime;
        speed = randomSpeed.randomSpeed;
    }
}
