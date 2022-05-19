using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public Animator animator;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
            animator.SetBool("IsMoving", success);
        }else
            animator.SetBool("IsMoving", false);
    }
    
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    moveSpeed
                        * Time.fixedDeltaTime
                        + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(
                    rb.position
                    + direction
                    * moveSpeed
                    * Time.fixedDeltaTime);
                return true;
            }
            else return false;
        }
        else return false;
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
