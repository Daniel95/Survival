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

        float time = randomSpeed.GetTime(transform.position, targetTransform.transform.position);

        transform.LeanMove(targetTransform.position, time).setEaseInOutBack().setOnComplete(() =>
        {
            pickedUp = true;
            Vector2 positionAroundPlayer = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer * (pickupCount + 1));
            if(positionAroundPlayer.y < 0)
            {
                positionAroundPlayer.y *= -1;
            }

            transform.LeanMove((Vector2)targetTransform.position + positionAroundPlayer, 3).setEaseInOutBack().setOnComplete(() =>
            {
                pickedUp = false;
                targetTransform.GetComponent<PlayerStats>().Randomize();
                targetTransform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                pickupCount++;

                transform.LeanMove(Vector3.zero, 3).setEaseInOutBack().setOnComplete(() =>
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
            });
        });
    }
}
