using System;
using UnityEngine;

public class MoveRandomPointsState : MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private int count;

    private float speed;

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
