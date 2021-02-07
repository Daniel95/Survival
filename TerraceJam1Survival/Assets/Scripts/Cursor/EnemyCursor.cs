using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCursor : MonoBehaviour
{
    [SerializeField] [Tag] private string playerTag;

    private IEnemyCursorState[] states;
    private IEnemyCursorState activeState;

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

        StartRandomState();
    }

    private void StartRandomState()
    {
        if(activeState != null)
        {
            activeState.OnComplete = null;
        }

        int randomIndex = UnityEngine.Random.Range(0, states.Length);
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
