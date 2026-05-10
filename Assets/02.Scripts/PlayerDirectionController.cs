using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class PlayerDirectionController : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private PlayerInputReader playerInputReader;

    [Header("상태 확인")]
    [SerializeField]
    private MoveDirection currentDirection;
    
    public MoveDirection CurrentDirection => currentDirection;


    private void Awake()
    {
        if (playerInputReader == null)
        {
            playerInputReader = GetComponent<PlayerInputReader>();
        }
    }

    private void LateUpdate()
    {
        UpdateDirection();
    }

    // 현재 필요성 검토 필요
    private void UpdateDirection()
    {
        Vector3 inputDirection = playerInputReader.MoveInput;

        int x = 0;
        int z = 0;

        if (inputDirection.x > 0.1f)
        {
            x = 1;
        }
        else if (inputDirection.x < -0.1f)
        {
            x = -1;
        }

        if (inputDirection.z > 0.1f)
        {
            z = 1;
        }
        else if (inputDirection.z < -0.1f)
        {
            z = -1;
        }

        switch ((x, z))
        {
            case (0, 1):
                currentDirection = MoveDirection.Front;
                break;
            case (0, -1):
                currentDirection = MoveDirection.Back;
                break;
            case (1, 0):
                currentDirection = MoveDirection.Right;
                break;
            case (-1, 0):
                currentDirection = MoveDirection.Left;
                break;
            case (1, 1):
                currentDirection = MoveDirection.RightFront;
                break;
            case (1, -1):
                currentDirection = MoveDirection.RightBack;
                break;
            case (-1, 1):
                currentDirection = MoveDirection.LeftFront;
                break;
            case (-1, -1):
                currentDirection = MoveDirection.LeftBack;
                break;
            case (0, 0):
                currentDirection = MoveDirection.None;
                break;
        }
    }
}
