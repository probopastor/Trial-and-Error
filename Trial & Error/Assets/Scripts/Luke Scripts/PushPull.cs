using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
