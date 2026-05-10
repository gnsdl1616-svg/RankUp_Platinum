using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerDirectionController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private PlayerInputReader playerInputReader;

    // 이동
    // 현재위치 + 방향 * 힘(속도) * 시간
    [Header("이동속도")]
    [SerializeField]
    private float moveSpeed = 2.0f;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

   

    private void Move()
    {
        if (rb == null || playerInputReader == null)
        {
            Debug.LogWarning("rb or playerInputReader = null referance");
            return;
        }

        Vector3 inputDirection = playerInputReader.MoveInput;

        // 리지드보디의 물리현상을 이용한 이동
        // linearVelocity -> 리지드보디가 현재 어느 방향으로, 얼마나 빠르게 움직이고 있는지 나타내는 값
        // y 속도는 0이아닌 값으로 유지해야 중력과 점프가 자연스럽게 동작
        Vector3 currentVelocity = rb.linearVelocity;

        Vector3 moveVelocity = new Vector3(
            inputDirection.x * moveSpeed,
            currentVelocity.y,
            inputDirection.z * moveSpeed
            );

        rb.linearVelocity = moveVelocity;

        /*// 위치를 직접 바꾸는 방식
        Vector3 newPosition = rb.position + inputDirection.normalized * MoveSpeed * Time.fixedDeltaTime;
        rb.position = newPosition;*/
    }

   
}
