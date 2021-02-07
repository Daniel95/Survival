using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed { get; private set; }
    public float jumpForce { get; private set; }

    [SerializeField] private float startSpeed = 40;
    [SerializeField] private float minSpeed = 20;
    [SerializeField] private float maxSpeed = 80;

    [SerializeField] private float startJumpForce = 800;
    [SerializeField] private float minJumpForce = 400;
    [SerializeField] private float maxJumpForce = 1600;

    private new Renderer renderer;

    public void Randomize()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        jumpForce = UnityEngine.Random.Range(minJumpForce, maxJumpForce);

        renderer.material.SetColor("_Color", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
    }

    private void Awake()
    {
        speed = startSpeed;
        jumpForce = startJumpForce;
        renderer = GetComponent<Renderer>();
    }
}
