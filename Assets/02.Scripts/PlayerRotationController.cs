using System;
using UnityEngine;

[RequireComponent(typeof(PlayerDirectionController))]
public class PlayerRotationController : MonoBehaviour
{
    [Header("Ref")]
    private PlayerDirectionController directionController;

    [Tooltip("실제 회전시킬 대상")]
    [SerializeField]
    private Transform rotateTarget;

    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed = 10f;

    private void Awake()
    {
        if (directionController == null)
        {
            directionController = GetComponent<PlayerDirectionController>();
        }

        if (rotateTarget == null)
        {
            {
                rotateTarget = transform;
            }
        }
    }

    private void Update()
    {
        RotateToMoveDirection();
    }

    private void RotateToMoveDirection()
    {
        if (directionController == null || rotateTarget == null)
        {
            return;
        }

        Vector3 moveDirection = Direction8.ToVector3(directionController.CurrentDirection);

        if (moveDirection.sqrMagnitude < 0.01f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        rotateTarget.rotation = Quaternion.Slerp(
            rotateTarget.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
            );
    }
}
