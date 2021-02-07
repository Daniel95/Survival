using NaughtyAttributes;
using UnityEngine;

public class EnemyCursor : MonoBehaviour
{
    public static float speedFactor = 1;

    [SerializeField] [Tag] private string playerTag;
    [SerializeField] private Component[] randomCursorStates;
    [SerializeField] private float speedIncreaseFactorOnPickup = 0.3f;

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
        Cursor.visible = false;

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

    private void OnDestroy()
    {
        speedFactor = 1;
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

        speedFactor += speedIncreaseFactorOnPickup;
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
