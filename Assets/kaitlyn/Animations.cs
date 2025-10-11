using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Animations : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Animator animator;

    private Vector2 movementDirection;


    void Start()
    {
        
    }

    
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movementDirection.x > 0) //right
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
        else if (movementDirection.x < 0) //left
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }

        if (movementDirection.y > 0) //up
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalkingUp", true);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        else if(movementDirection.y < 0) //down
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalkingDown", true);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }

        if(movementDirection.x == 0)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
        if(movementDirection.y == 0)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
    }
}
