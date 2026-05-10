using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    [Header("상태확인")]
    [SerializeField]
    private Vector3 moveInput;

    [SerializeField]
    private bool jumPressed;

    public Vector3 MoveInput => moveInput;
    public bool JumPressed => jumPressed;

    void Update()
    {
        ReadMoveInput();
        ReadJumpInput();
    }

    private void ReadMoveInput()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            z += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            z -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            x += 1f;
        }

        moveInput = new Vector3(x, 0f, z).normalized;
    }

    private void ReadJumpInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumPressed = true;
        }
    }

    // 점프 입력을 한 번 사용하면 false 로 되돌려
    // 꾹누르거나 연속으로 누를때 계속 점프되는 문제 방지
    public bool ConsumeJumpPressed()
    {
        if (!jumPressed)
        {
            return false;
        }

        jumPressed = false;
        return true;
    }
}
