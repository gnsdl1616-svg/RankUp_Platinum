using System;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField]
    private Transform target;

    [Header("Camera Distance")]
    [SerializeField]
    private float distance = 5f;

    [SerializeField]
    private float height = 1.5f;

    [Header("Smooth")]
    [SerializeField]
    private float followSmoothTime = 0.05f;

    private float yaw;
    private float pitch;

    private Vector3 followVelocity;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("[ThirdPersonCamera] target이 연결되지 않았습니다.");
            return;
        }

        Vector3 currentEuler = target.eulerAngles;
        yaw = currentEuler.y;
        pitch = currentEuler.x;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        FollowTarget();
    }

    private void FollowTarget()
    {
        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition = target.position + Vector3.up * height;
        Vector3 cameraOffset = cameraRotation * new Vector3(0f, 0f, -distance);

        Vector3 desiredPosition = targetPosition + cameraOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref followVelocity,
            followSmoothTime
            );

        transform.LookAt(targetPosition);
    }
}
