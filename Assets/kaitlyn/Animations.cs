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
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
        else if (movementDirection.x < 0) //left
        {
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }

        if (movementDirection.y > 0) //up
        {
            animator.SetBool("isWalkingUp", true);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        else if(movementDirection.y < 0) //down
        {
            animator.SetBool("isWalkingDown", true);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }

        if(movementDirection.x == 0 && movementDirection.y == 0)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

    }
}
