using System;
using UnityEngine;

public class SwipeAtPlayerState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private float spawnRate;

    public float GetSpawnRate() => spawnRate;

    public void Act() { }

    public void Enter()
    {
        //Debug.Log("MoveRandomDirectionState");

        float time = randomSpeed.GetTime(transform.position, Player.GetInstance().transform.position);

        transform.LeanMove(Player.GetInstance().transform.position, time).setEaseInExpo().setOnComplete(() =>
        {
            if (onComplete != null)
            {
                onComplete();
            }
        });
    }
}
