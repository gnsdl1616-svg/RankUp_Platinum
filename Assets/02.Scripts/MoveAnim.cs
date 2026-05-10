using UnityEngine;

public class MoveAnim : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerInputReader playerInputReader;
    [SerializeField]
    private PlayerGroundCheker groundCheker;
    [SerializeField]
    private Rigidbody rb;

    [Header("AnimParameter")]
    [SerializeField]
    private string isMovingParameterName = "IsMoving";
    [SerializeField]
    private string jumpTriggerParameterName = "Jump";
    [SerializeField]
    private string isFallingParameterName = "IsFalling";
    [SerializeField]
    private string landTriggerParameterName = "Land";

    [Header("Tuning")]
    [Tooltip("이 값보다 작은 입력은 정지로 간주한다")]
    [SerializeField] private float inputDeadZone = 0.1f;
    [Tooltip("낙하 상태 체크용")]
    [SerializeField] private float fallingVelocityThreshold = -0.1f;

    [Header("상태 확인")]
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isFalling;
    [SerializeField]
    private bool wasGrounded;
    
    private int isMovingeHash;
    private int jumpHash;
    private int isFallingHash;
    private int landHash;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (playerInputReader == null)
        {
            playerInputReader = GetComponent<PlayerInputReader>();
        }

        if (groundCheker == null)
        {
            groundCheker = GetComponent<PlayerGroundCheker>();
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        isMovingeHash = Animator.StringToHash(isMovingParameterName);
        jumpHash = Animator.StringToHash(jumpTriggerParameterName);
        isFallingHash = Animator.StringToHash(isFallingParameterName);
        landHash = Animator.StringToHash(landTriggerParameterName);
    }

    private void Start()
    {
        if (groundCheker != null)
        {
            wasGrounded = groundCheker.IsGrounded;
        }
    }

    private void Update()
    {
        UpdateMoveAnimation();
        UpdateAirAimation();
    }

    private void UpdateMoveAnimation()
    {
        if (playerInputReader == null || animator == null)
        {
            return;
        }

        Vector3 rawInput = playerInputReader.MoveInput;

        isMoving = rawInput.sqrMagnitude >= inputDeadZone * inputDeadZone;

        animator.SetBool(isMovingeHash, isMoving);
    }

    private void UpdateAirAimation()
    {
        if (animator  == null || groundCheker == null || rb == null)
        {
            return;
        }

        bool isGrounded = groundCheker.IsGrounded;
        float verticalVelocity = rb.linearVelocity.y;

        // 지상 -> 공중으로 바뀌는 순간
        if (wasGrounded && !isGrounded && verticalVelocity > 0f)
        {
            animator.SetTrigger(jumpHash);
        }

        // 공중에서 아래로 떨어지면 떨어지는 falling anim 실행
        isFalling = !isGrounded && verticalVelocity < fallingVelocityThreshold;
        animator.SetBool(isFallingHash, isFalling);

        // 공중 -> 지상(착지)으로 바뀌는 순간
        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger(landHash);
        }

        // 착지까지 애니메이션이 끝나면 wasGrounded 초기화

        wasGrounded = isGrounded;
    }

}
