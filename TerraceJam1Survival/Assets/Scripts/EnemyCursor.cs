using UnityEngine;

public class EnemyCursor : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private Transform playerTransform;

    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        transform.Translate(directionToPlayer * Time.deltaTime);        
    }

    private void Awake()
    {
        playerTransform = PlayerMovement.GetInstance().transform;
    }
}
