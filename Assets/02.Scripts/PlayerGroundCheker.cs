using UnityEngine;

public class PlayerGroundCheker : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private Transform groundCheckPoint;

    [SerializeField]
    private float groundCheckDistance = 0.5f;

    [SerializeField]
    private LayerMask groundLayer;

    [Header("상태 확인")]
    [SerializeField]
    private bool isGrounded;

    [Header("Debuging")]
    [SerializeField]
    private bool isDrawGizmos = true;

    public bool IsGrounded => isGrounded;

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void CheckGround()
    {
        if (groundCheckPoint == null)
        {
            Debug.Log("groundCheckPoint == null");
            isGrounded = false;
            return;
        }
        // groundCheckPoint에서 Vector3.down(0,-1,0) 방향으로 groundCheckDistance 만큼 ray를 쏴서,
        // groundLayer 가 포함 된 Collider가 감지 되는가 ?
        if (Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!isDrawGizmos)
        {
            return;
        }

        if (groundCheckPoint == null)
        {
            return;
        }

        Vector3 start = groundCheckPoint.position;
        Vector3 end = start + Vector3.down * groundCheckDistance;

        Gizmos.DrawLine(start, end);

    }
}
