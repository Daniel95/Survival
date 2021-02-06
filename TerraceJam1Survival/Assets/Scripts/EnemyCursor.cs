using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCursor : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] [Tag] private string playerTag;

    private Transform playerTransform;

    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        transform.Translate(directionToPlayer * Time.deltaTime * speed);        
    }

    private void Awake()
    {
        playerTransform = Player.GetInstance().transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
