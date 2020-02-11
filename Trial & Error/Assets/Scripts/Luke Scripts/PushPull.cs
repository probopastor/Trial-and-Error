using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public bool isWalls;
    public bool isObjects;

    public LayerMask wallLayer;
    public LayerMask objectLayer;

    public Rigidbody rb;

    public float speed = 60.0f;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
            {
                // show reticle that wall layer 

                target = hit.transform;
                //Debug.Log(transform.position);
                // Move our position a step closer to the target.

                //transform.position = target.position;
                //target = null;
                //transform.position = target.position;

                if (target != null)
                {
                    float step = speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                    //rb.AddForce();
                }

            }

        }






        /////TRY THIS OUT LATER
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, wallLayer))
        //{
        //    // show reticle that wall layer 

        //   if(Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        //grab object
        //    }
        //}
        /////////////////////////////////////////////////






        /*
         * reticle creates raycast to determine if object can be interacted with
         * if the reticle hits set a bool to true to indicate the player can interact
         * 
         * */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*if the bool for walls is true, then 
             *  if the raycast is above a certain range, then 
             *      fly the player towards the raycasted obj
             *      at the position of the raycast
             *  if the raycast is below a certain range OR if the player is on a wall, then
             *      push the player in the opposite direction of the raycast
             * 
             * 
            */
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            /*if the bool for interactables is true, then 
             *  if the game object for item in hand is null, then
             *      set the game object for the raycasted item to in hand
             *     if (Input.GetKeyUp(KeyCode.Mouse1))
             *     {
             *          throw it in the raycasted direction and set item in hand to null
             *     }
             * 
             */
        }
    }
}
