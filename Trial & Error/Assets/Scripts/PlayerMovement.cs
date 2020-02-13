using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CinemachineVirtualCamera _playerVirtualCamera;

    [SerializeField, Tooltip("Max speed the player can move at")]
    private float maxPlayerSpeed = 8f;
    private Vector2 _movementDir = Vector2.zero;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        var velocity = _rigidbody.velocity;
        //TODO _movementDir *= pvc cinemachine transform forward 
        velocity += new Vector3(_movementDir.x, 0f, _movementDir.y);
        velocity = Vector3.ClampMagnitude(velocity, maxPlayerSpeed);
        _rigidbody.velocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDir = context.performed ? context.ReadValue<Vector2>() : Vector2.zero;
    }
}
