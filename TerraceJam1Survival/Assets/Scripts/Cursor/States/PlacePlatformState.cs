using NaughtyAttributes;
using System;
using UnityEngine;

public class PlacePlatformState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private GameObject platformPrefab = null;
    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float maxDistanceFromPlayer = 10;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
    }

    public void Enter()
    {
        Vector2 randomPosition = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer);

        float time = randomSpeed.GetTime(transform.position, randomPosition);

        transform.LeanMove(randomPosition, time).setEaseInOutBack().setOnComplete(() =>
        {
            GameObject platform = Instantiate(platformPrefab, transform.position, Quaternion.identity);

            platform.GetComponent<Renderer>().material.SetColor("_Color", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f)));

            if (onComplete != null)
            {
                onComplete();
            }
        });
    }
}
