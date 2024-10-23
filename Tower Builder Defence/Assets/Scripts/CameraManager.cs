using System;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    private float horizontal, vertical;
    [SerializeField] private float cameraMoveSpeed=25f;
    [SerializeField] private float zoomSpeed=20f;

    [SerializeField] private float 
        minScrollLimit=10f,
        maxScrollLimit=30f;
    
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float ortographicSize,targetOrtographicSize;
    
    private void Start()
    {
        ortographicSize = virtualCamera.m_Lens.OrthographicSize;
        targetOrtographicSize = ortographicSize;
    }

    void Update()
    {
        HandleCameraMovement();
        CameraZoom();
    }

    private void CameraZoom()
    {  
        //mouse scroll inputuna göre virtual camerayı uzaklaştırıp yakınlaştırma.
        targetOrtographicSize += Input.mouseScrollDelta.y*zoomSpeed;
        targetOrtographicSize = Mathf.Clamp(targetOrtographicSize, minScrollLimit, maxScrollLimit);
        float smoothSpeed = 5f;
        ortographicSize = Mathf.Lerp(ortographicSize, targetOrtographicSize, Time.deltaTime * smoothSpeed);
        virtualCamera.m_Lens.OrthographicSize = ortographicSize;
    }

    private void HandleCameraMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(horizontal, vertical).normalized;
        transform.position += moveDir * (cameraMoveSpeed * Time.deltaTime);

    }
}
