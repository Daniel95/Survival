using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCursor : MonoBehaviour
{
    [SerializeField] [Tag] private string playerTag;
    [SerializeField] private Component[] randomCursorStates;

    private IEnemyCursorState[] states;
    private float[] stateSpawnRegion;
    private IEnemyCursorState activeState;
    private int lastStateIndex;
    private float totalSpawnRate;
    private IEnemyCursorState pickupPlayerState;

    private void Update()
    {
        if(activeState != null)
        {
            activeState.Act();
        }
    }

    private void Awake()
    {
        pickupPlayerState = GetComponent<PickupPlayerState>();

        states = new IEnemyCursorState[randomCursorStates.Length];

        for (int i = 0; i < randomCursorStates.Length; i++)
        {
            states[i] = (IEnemyCursorState)randomCursorStates[i];
        }

        stateSpawnRegion = new float[states.Length];

        for (int i = 0; i < stateSpawnRegion.Length; i++)
        {
            totalSpawnRate += states[i].GetSpawnRate();
            stateSpawnRegion[i] = totalSpawnRate;
        }

        StartRandomState();
    }

    private void StartRandomState()
    {
        if(activeState != null)
        {
            activeState.onComplete = null;
        }

        int randomIndex = lastStateIndex;

        while (randomIndex == lastStateIndex)
        {
            float randomValue = Random.Range(0.0f, totalSpawnRate);
            
            for (int i = 0; i < stateSpawnRegion.Length; i++)
            {
                if(stateSpawnRegion[i] >= randomValue)
                {
                    randomIndex = i;

                    break;
                }
            }
        }

        lastStateIndex = randomIndex;
        StartState(states[randomIndex]);
    }

    private void StartPickupPlayerState()
    {
        StartState(pickupPlayerState);
    }

    private void StartState(IEnemyCursorState enemyCursorState)
    {
        if (activeState != null)
        {
            activeState.onComplete = null;
        }

        activeState = enemyCursorState;
        activeState.onComplete += StartRandomState;
        activeState.Enter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag) && activeState != pickupPlayerState)
        {
            StartPickupPlayerState();
        }
    }
}
