using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PushPull : MonoBehaviour
{
    public LayerMask wallLayer;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Camera _camera;
    
    [SerializeField]
    private GameObject pullPos;
    public float pullSpeed = 60f;
    private bool onWall = false;
    public float pushSpeed = 60f;

    [SerializeField]
    private float pullMaxDistance = 10f;
    [SerializeField]
    private float pushMaxDistance = 5f;

    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    private float _startCamFOV = 60;
    [SerializeField]
    private float pullCamFov = 90;
    [SerializeField]
    private float pushCamFov = 90;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main;
        playerCam.m_Lens.FieldOfView = _startCamFOV;
    }

   
    void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, pullMaxDistance, wallLayer))
            {
                pullPos.transform.position = hit.point;
                pullPos.transform.parent = hit.transform;
            }
            else
            {
                pullPos.transform.parent = transform;
                pullPos.transform.position = transform.position;
            }
        }
        
        //pull mechanic
        if (Input.GetMouseButton(0))
        {
            if (pullPos.transform.parent != transform) //&& Physics.Raycast(ray, out RaycastHit hit, pullMaxDistance, wallLayer))
            {
                //_rigidbody.position = Vector3.Lerp(_rigidbody.position, pullPos.transform.position, Time.deltaTime * pullSpeed);

                var forceDir = pullPos.transform.position - transform.position;
                _rigidbody.AddForce(forceDir * pullSpeed, ForceMode.Acceleration);
                
                if (playerCam.m_Lens.FieldOfView < pullCamFov)
                {
                    playerCam.m_Lens.FieldOfView += Time.deltaTime * 30f;
                }
            }
            else if (playerCam.m_Lens.FieldOfView > _startCamFOV)
            {
                playerCam.m_Lens.FieldOfView -= Time.deltaTime * 50f;
            }
        }
        else if (playerCam.m_Lens.FieldOfView > _startCamFOV)
        {
            playerCam.m_Lens.FieldOfView -= Time.deltaTime * 50f;
        }


        //push mechanic
        if (Input.GetMouseButtonDown(1))
        {
            if (onWall)
            {
                var reverseRay = new Ray(ray.origin, -ray.direction);
                //shoots a raycast out at X and checks if it hits something on the wallLayer
                if (Physics.Raycast(reverseRay, out RaycastHit hit, pushMaxDistance, wallLayer))
                {
                    //pushes the player in the opposite direction of the hit Raycast
                    _rigidbody.AddForce(ray.direction * pushSpeed, ForceMode.Impulse);
                    playerCam.m_Lens.FieldOfView = pushCamFov;
                }
            }
            else
            {
                //shoots a raycast out at X and checks if it hits something on the wallLayer
                if (Physics.Raycast(ray, out RaycastHit hit, pushMaxDistance, wallLayer))
                {
                    //pushes the player in the opposite direction of the hit Raycast
                    _rigidbody.AddForce(-ray.direction * pushSpeed, ForceMode.Impulse);
                    playerCam.m_Lens.FieldOfView = pushCamFov;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        onWall = true;
    }

    private void OnCollisionExit(Collision other)
    {
        onWall = false;
    }
}
