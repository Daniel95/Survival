using System;
using UnityEngine;

public class MoveRandomDirectionState: MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private HelperValues.RandomTime randomTime;

    private float timer;
    private float speed;

    public void Act()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;

        Vector3 directionToPlayer = (transform.position - Player.GetInstance().transform.position).normalized;

        transform.Translate(directionToPlayer * Time.deltaTime * speed);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnComplete();
        }

    }

    public void Enter()
    {
        timer = randomTime.randomTime;
        speed = randomSpeed.randomSpeed;
    }
}
