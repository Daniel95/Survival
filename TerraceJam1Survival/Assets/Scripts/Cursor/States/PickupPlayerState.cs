using System;
using UnityEngine;

public class PickupPlayerState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private float maxDistanceFromPlayer = 15;

    private Transform targetTransform;
    private bool pickedUp;
    private int pickupCount;

    public float GetSpawnRate() => 0;

    public void Act()
    {
        if (pickedUp)
        {
            targetTransform.transform.position = transform.position;
        }
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsState");

        targetTransform = Player.GetInstance().transform;

        float time = Vector2.Distance(transform.position, targetTransform.transform.position) / randomSpeed.randomSpeed;

        transform.LeanMove(targetTransform.position, time).setEaseInOutBack().setOnComplete(() =>
        {
            pickedUp = true;
            Vector2 positionAroundPlayer = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer * (pickupCount + 1));

            transform.LeanMove(positionAroundPlayer, 3).setEaseInOutBack().setOnComplete(() =>
            {
                pickedUp = false;

                transform.LeanMove(new Vector3(0, 0, 0), 3).setEaseInOutBack().setOnComplete(() =>
                {
                    pickupCount++;
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
            });
        });
    }
}
