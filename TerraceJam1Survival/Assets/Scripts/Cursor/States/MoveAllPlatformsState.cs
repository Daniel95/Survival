using NaughtyAttributes;
using System;
using UnityEngine;

public class MoveAllPlatformsState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] [Tag] private string platformTag;
    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float maxPlatformMoveDistance = 5;

    private Transform targetTransform;
    private bool pickedUp;
    private GameObject[] platforms = null;
    private int platformIndex;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        if (pickedUp)
        {
            targetTransform.transform.position = transform.position;
        }
    }

    private void MovePlatformsRecursive()
    {
        if(platformIndex >= platforms.Length || platforms[platformIndex] == null)
        {
            if (onComplete != null)
            {
                onComplete();
            }

            return;
        }

        targetTransform = platforms[platformIndex].transform;

        float time = randomSpeed.GetTime(transform.position, targetTransform.transform.position);

        transform.LeanMove(targetTransform.position, time).setEaseOutBack().setOnComplete(() =>
        {
            pickedUp = true;

            Vector2 randomPosition = (Vector2)targetTransform.position + UnityEngine.Random.insideUnitCircle * maxPlatformMoveDistance;

            transform.LeanMove(randomPosition, time).setEaseInElastic().setOnComplete(() =>
            {
                pickedUp = false;
                targetTransform.GetComponent<Renderer>().material.SetColor("_Color", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f)));

                platformIndex++;

                MovePlatformsRecursive();
            });
        });
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsState");
        platforms = GameObject.FindGameObjectsWithTag(platformTag);

        platformIndex = 0;
        MovePlatformsRecursive();

        //for (int i = 0; i < platforms.Length; i++)
        //{
        //    platforms[i].GetComponent<BoxCollider2D>().enabled = false;
        //}



        //GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);

        //int randomPlatformIndex = UnityEngine.Random.Range(0, platforms.Length);

        //targetTransform = platforms[randomPlatformIndex].transform;

        //float time = randomSpeed.GetTime(transform.position, targetTransform.transform.position);

        //transform.LeanMove(targetTransform.position, time).setEaseInOutBack().setOnComplete(() =>
        //{
        //    pickedUp = true;
        //    Vector2 playerPosition = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer);

        //    transform.LeanMove(playerPosition, time).setEaseInOutBack().setOnComplete(() =>
        //    {
        //        pickedUp = false;
        //        targetTransform.GetComponent<Renderer>().material.SetColor("_Color", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f)));

        //        if (onComplete != null)
        //        {
        //            onComplete();
        //        }
        //    });
        //});
    }
}
