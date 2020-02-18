using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public bool isWalls;
    public bool isObjects;

    public LayerMask wallLayer;
    public LayerMask objectLayer;

    private Rigidbody rb;

    public float pullSpeed = 60.0f;
    public float pushSpeed = 60f;

    public GameObject magnetizedObject;

    Transform target;

    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    private float _startCamFOV = 60;
    [SerializeField]
    private float _pullCamFOV = 90;
    [SerializeField]
    private float _pushCamFOV = 90;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerCam.m_Lens.FieldOfView = _startCamFOV;
    }

   
    void Update()
    {

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //push/pull mechanic
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
            {
                rb.AddForce(ray.direction * pullSpeed);
                if (playerCam.m_Lens.FieldOfView < _pullCamFOV)
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
            //shoots a raycast out at X and checks if it hits something on the wallLayer
            if (Physics.Raycast(ray, out hit, 5f, wallLayer))
            {
                //pushes the player in the opposite direction of the hit Raycast
               //rb.AddForceAtPosition(-ray.direction * pushSpeed, hit.point, ForceMode.Impulse);
               rb.AddForce(-ray.direction * pushSpeed, ForceMode.Impulse);
               playerCam.m_Lens.FieldOfView = _pushCamFOV;
            }
        }

        //USE THE CODE BELOW FOR THE MAGNETIZED MECHANIC (assigns the Raycasthit object to a variable)
        //magnetizedObject = hit.collider.gameObject;
    }
}
