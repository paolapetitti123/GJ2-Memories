using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;


    /* Update is called once per frame so we need to check when the player hits certain keys
     * the movement for those keys will be handled in FixedUpdate
     */
    void Update()
    {
        // gives value between -1 and 1 depending on horizontal input, i.e pressing the Left arrow key give -1 and Right gives 1
        // (WASD + arrow keys work)
        movement.x = Input.GetAxisRaw("Horizontal");

        // gives value between -1 and 1 depending on vertical input (WASD + arrow keys work)
        movement.y = Input.GetAxisRaw("Vertical");


        /* I created seperate animations for walking up, down, left, right, I use a blend tree called Movement in the 
         * Main Character Animator that essentially puts them all together, this way when the character sprite sheet is ready 
         * all I have to do is modify the animation to use the sprites instead of color changes.
         * 
         * These 3 lines allow for the character to move around the screen
         */
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    /* Need to use Fixed Update for movement since the framerate of the game can change
     * per computer making manipulating the physics of the character unpredictable. Fixed
     * Update allows for a set fixed frame rate making it much more manageable.
     */
    void FixedUpdate()
    {
        /* To make sure the movement speed will always stay the same "Time.fixedDeltaTime" is super important as
         * no matter how many times the fixed update function is called, fixedDeltaTime is the amount of time that has
         * elapsed since the last time the function was called and the result of that is a constant movement speed.
         */
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
