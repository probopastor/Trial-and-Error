using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Camera _playerCam;

    
    private Quaternion characterRot;
    private Quaternion cameraRot;
    [SerializeField]
    private float camMinClamp = -90f;
    [SerializeField]
    private float camMaxClamp = 90f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCam = GetComponentInChildren<Camera>();
        
        
        characterRot = transform.localRotation;
        cameraRot = _playerCam.transform.localRotation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var value = context.ReadValue<Vector2>();
        }
        else
        {
            
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        var lookValue = context.ReadValue<Vector2>();
        
        
        characterRot = Quaternion.Euler (0f, lookValue.x, 0f);
        cameraRot = Quaternion.Euler (-lookValue.y, 0f, 0f);

        cameraRot = ClampRotationAroundXAxis (cameraRot);

        //_rigidbody.MoveRotation(Quaternion.Slerp (transform.localRotation, characterRot, 5 * Time.deltaTime));
        //_playerCam.transform.localRotation = Quaternion.Slerp (_playerCam.transform.localRotation, cameraRot, 5 * Time.deltaTime);
        
        _rigidbody.MoveRotation(characterRot);
        _playerCam.transform.localRotation = cameraRot;
    }
    
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, camMinClamp, camMaxClamp);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
