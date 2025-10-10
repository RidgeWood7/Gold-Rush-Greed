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

        if(movementDirection.x > 0)
        {
            animator.SetBool("isWlkingRight", true);
            animator.SetBool("isWalkingLeft", false);
        }
        else if (movementDirection.x < 0)
        {
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWlkingRight", false);
        }

        if (movementDirection.y > 0)
        {
            animator.SetBool("isWalkingUp", true);
            animator.SetBool("isWalkingDown", false);
        }
        else if(movementDirection.y < 0)
        {
            animator.SetBool("isWalkingDown", true);
            animator.SetBool("isWalkingUp", false);
        }

        if(movementDirection.x == 0)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWlkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        if(movementDirection.y == 0)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
    }
}
