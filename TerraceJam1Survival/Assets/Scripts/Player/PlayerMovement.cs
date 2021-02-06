using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    public static PlayerMovement GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerMovement>();
        }
        return instance;
    }

    private static PlayerMovement instance;
    #endregion

    [SerializeField] private float runSpeed = 40f;

    private CharacterController2D controller;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

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