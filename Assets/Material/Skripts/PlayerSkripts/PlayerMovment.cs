using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{
    [Header("Параметры движения")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float jumpOffset;

    [Header("Настройки")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private LayerMask groundMask;

    [Header("Кристалы")]
    public int Cristal;

    public float hangTime = 0.5f;
    private float hangCounter;


    private Animator animator;
    private Rigidbody2D rb;
    public bool faceRight = true;

    private EventInstance musicEventMove;
    private bool isPlaying = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        musicEventMove = RuntimeManager.CreateInstance("event:/Move");
        Vector3 position = transform.position;
        musicEventMove.set3DAttributes(RuntimeUtils.To3DAttributes(position));
    }
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        bool isJumpButtonPressed = Input.GetButtonDown("Jump");
        Move(direction, isJumpButtonPressed);
        Reflect(direction);
        UpdateSound(direction);
    }
    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        if (isGrounded)
        {
            ResetJump();
        }

    }
    public void Move(float derection, bool isJampButtonPressed)
    {
        if (isJampButtonPressed && hangCounter > 0f)
        {
            jump();
            animator.SetBool("jump", true);
        }
        if (Mathf.Abs(derection) > 0.01f)
        {
            animator.SetFloat("moveX", Mathf.Abs(derection));
            HorizontalMovment(derection);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetBool("idle", true);
        }
    }
    private void jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
   
    private void HorizontalMovment(float derection)
    {
        rb.linearVelocity = new Vector2(curve.Evaluate(derection), rb.linearVelocity.y);
    }
    public void Reflect(float direction)
    {
        if ((direction > 0 && !faceRight) || (direction < 0 && faceRight))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            faceRight = !faceRight;
        }
    }
    public void UpdateSound(float derection)
    {
        if (Mathf.Abs(derection) >= 0.01f && isGrounded && gameObject.GetComponent<Health>().isAlive == true)
        {
            if (!isPlaying)
            {
                musicEventMove.start();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying || gameObject.GetComponent<Health>().isAlive == false)
            {
                StopSoundMove();
                isPlaying = false;
            }
        }
    }
    public void StopSoundMove()
    {
        musicEventMove.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void ResetJump()
    {
        animator.SetBool("jump", false);
    }
}
