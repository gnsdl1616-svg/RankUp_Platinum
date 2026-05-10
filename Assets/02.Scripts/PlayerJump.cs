using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerGroundCheker))]
public class PlayerJump : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private PlayerInputReader playerInputReader;
    [SerializeField]
    private PlayerGroundCheker groundCheker;

    [Header("점프 파워")]
    [SerializeField]
    private float jumpPower = 5.0f;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (playerInputReader == null)
        {
            playerInputReader = GetComponent<PlayerInputReader>();
        }

        if (groundCheker == null)
        {
            groundCheker = GetComponent<PlayerGroundCheker>();
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (rb == null || playerInputReader == null || groundCheker == null)
        {
            Debug.LogWarning("rb or playerInputReader or groundCheker = null");
            return;
        }

        bool jumpPressed = playerInputReader.ConsumeJumpPressed();

        if(!jumpPressed)
        {
            return;
        }

        if(!groundCheker.IsGrounded)
        {
            return;
        }

        Vector3 currentVelocity = rb.linearVelocity;

        Vector3 jumpVelocity = new Vector3(
            currentVelocity.x,
            jumpPower,
            currentVelocity.z
            );

        rb.linearVelocity = jumpVelocity;

        /*if (!playerDirectionController.GroundCheck)
        {
            return;
        }

        if (playerDirectionController.GroundCheck)
        {
            Vector3 inputDirection = playerInputReader.InputDirection;

            Vector3 currentVelocity = rb.linearVelocity;

            Vector3 jumpVelocity = new Vector3(
                currentVelocity.x,
                inputDirection.y * jumpPower,
                currentVelocity.z
                );

            rb.linearVelocity = jumpVelocity;
        }*/
    }
}
