using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _mainCameraRotation;

    [SerializeField]
    private float playerSpeed = 1f;
    [SerializeField, Tooltip("Max speed the player can move at")]
    private float maxPlayerSpeed = 8f;
    private Vector2 _movementDir = Vector2.zero;
    private int collectiblesCollected = 0;
    public TextMeshProUGUI sphereText;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCameraRotation = FindObjectOfType<CinemachineBrain>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sphereText.text = "Collected Spheres: " + collectiblesCollected;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 velocity = _rigidbody.velocity;
        var camRot = _mainCameraRotation.rotation;
        camRot.eulerAngles = new Vector3(0, camRot.eulerAngles.y, 0);
        velocity += camRot * Vector3.ClampMagnitude(new Vector3(_movementDir.x, 0, _movementDir.y) * playerSpeed, maxPlayerSpeed);
        velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        _rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            Debug.Log("Got it!");
            collectiblesCollected++;
            other.gameObject.SetActive(false);
            sphereText.text = "Collected Spheres: " + collectiblesCollected;

        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDir = context.performed ? context.ReadValue<Vector2>() : Vector2.zero;
    }
}
