using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private GameObject fpsCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpDownRotation = 0f;
    private float CurrentCameraUpDownRotation = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 위치계산
        var _xMovement = Input.GetAxis("Horizontal");
        var _zMovement = Input.GetAxis("Vertical");

        // 좌우
        var _movementHorizontal = transform.right * _xMovement;
        // 위아래
        var _movementVertical = transform.forward * _zMovement;
        
        // 최종 벡터
        var _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;

        Move(_movementVelocity);
        
        // 회전계산
        var _yRotation = Input.GetAxis("Mouse X");
        var _rotationVector = new Vector3(0, _yRotation, 0) * lookSensitivity;

        Rotate(_rotationVector);
        
        // 카메라 회전계산
        var _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

        RotateCamera(_cameraUpDownRotation);
    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            CurrentCameraUpDownRotation -= CameraUpDownRotation;
            CurrentCameraUpDownRotation = Mathf.Clamp(CurrentCameraUpDownRotation, -85, 85);

            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpDownRotation, 0, 0);
        }
    }

    private void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    private void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }
    
    private void RotateCamera(float cameraUpDownRotation)
    {
        CameraUpDownRotation = cameraUpDownRotation;
    }
}
