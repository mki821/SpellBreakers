using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineCamera _cinemachineCamera;

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void SetTarget(Transform transform)
    {
        _cinemachineCamera.Target.TrackingTarget = transform;
    }
}
