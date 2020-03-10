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
    private Camera _camera;
    
    [SerializeField]
    private GameObject pullPos;

    private ParticleSystem pullParticle;
    [SerializeField]
    private float startPullParticleSpeed = 5;
    [SerializeField]
    private float pullingParticleSpeed = 50;
    public float pullSpeed = 60f;
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
        _camera = Camera.main;
        playerCam.m_Lens.FieldOfView = _startCamFOV;
        pullParticle = pullPos.GetComponentInChildren<ParticleSystem>();
        pullParticle.Stop();
    }

   
    void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var pullParticleMain = pullParticle.main;
        if (Physics.Raycast(ray, out RaycastHit particleHit, pullMaxDistance, wallLayer))
        {
            pullPos.transform.position = particleHit.point;
            pullPos.transform.LookAt(transform.position);
            pullParticle.Play();
        }
        else
        {
            pullParticle.Stop();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit pullHit, pullMaxDistance, wallLayer))
            {
                pullPos.transform.position = pullHit.point;
                pullPos.transform.parent = pullHit.transform;
                pullParticleMain.startSpeed = pullingParticleSpeed;
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

        if (Input.GetMouseButtonUp(0))
        {
            pullParticleMain.startSpeed = startPullParticleSpeed;
        }


        //push mechanic
        if (Input.GetMouseButtonDown(1))
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
