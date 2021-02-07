using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCursor : MonoBehaviour
{
    [SerializeField] [Tag] private string playerTag;

    private IEnemyCursorState[] states;
    private float[] stateSpawnRegion;
    private IEnemyCursorState activeState;
    private int lastStateIndex;
    private float totalSpawnRate;

    private void Update()
    {
        if(activeState != null)
        {
            activeState.Act();
        }
    }

    private void Awake()
    {
        states = GetComponents<IEnemyCursorState>();
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
            activeState.OnComplete = null;
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

        activeState = states[randomIndex];
        activeState.OnComplete += StartRandomState;
        activeState.Enter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
