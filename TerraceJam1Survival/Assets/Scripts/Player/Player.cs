using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    #region Singleton
    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Player>();
        }
        return instance;
    }

    private static Player instance;
    #endregion

    [SerializeField] [Tag] private string enemyCursorTag;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float yDeadZone = -10;

    private CharacterController2D controller;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= yDeadZone)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        //jump = false;

        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }
}