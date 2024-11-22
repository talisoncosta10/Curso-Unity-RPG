using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public CharacterController characterController;
    public Animator animator;
    public float playerSpeed;
    private Vector3 direcao;

    [Header("Pulo")]
    public float heightJump;
    public float gravity = -19.62f;
    private Vector3 streghtY;
    public Transform foot;
    public LayerMask layerFloor;
    public bool isGround;

    [Header("Camera")]
    public Transform camera;
    public float suavizarRotacao;
    private float rotationSpeed;
    float anglos;
    float vision;

    private void FixedUpdate()
    {
        isGround = Physics.CheckSphere(foot.position, 0.3f, layerFloor);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direcao = new Vector3(horizontal, 0f, vertical);

        vision = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        anglos = Mathf.SmoothDampAngle(transform.eulerAngles.y, vision, ref rotationSpeed, suavizarRotacao);

        streghtY.y += gravity * Time.deltaTime;
        characterController.Move(streghtY * Time.deltaTime);

        Walking();
        Run();
        Jump();
    }

    public void Walking()
    {
        if (direcao.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, anglos, 0f);
            Vector3 novaDirecao = Quaternion.Euler(0f, vision, 0f) * Vector3.forward;

            characterController.Move(novaDirecao * playerSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }



    }

    public void Run()
    {
            if (Input.GetKey(KeyCode.LeftShift) && direcao.magnitude > 0.5f)
            {
                transform.rotation = Quaternion.Euler(0f, anglos, 0f);
                Vector3 novaDirecao = Quaternion.Euler(0f, vision, 0f) * Vector3.forward;

                characterController.Move(novaDirecao * playerSpeed * Time.deltaTime);
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetBool("isJumping", true);
            streghtY.y = Mathf.Sqrt(heightJump * -2 * gravity);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

    }
}
