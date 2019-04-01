using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class CharacterControllerScript : MonoBehaviour
{
    //handling input and moving the player
    //Changing some variables of the animator

    public float maxSpeed = 10f;
    public bool facingRight = true;

    Rigidbody2D r2d;
    Animator anim;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
    }

    private void FixedUpdate()
    {
        float moveVertical = Input.GetAxisRaw("Vertical");
        float moveHorizontal = Input.GetAxisRaw("Horizontal");      

        anim.SetFloat("HorizontalSpeed", Mathf.Abs(moveHorizontal));
        anim.SetFloat("VerticalSpeed", Mathf.Abs(moveVertical));

        r2d.velocity = new Vector2(moveHorizontal * maxSpeed, moveVertical * maxSpeed);

        if (moveHorizontal > 0 && !facingRight)
        {
            FlipHorizontal();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            FlipHorizontal();
        }

        if (moveVertical > 0 && anim.GetBool("FacingDown"))
        {
            FlipVertical();
        }
        else if (moveVertical < 0 && !anim.GetBool("FacingDown"))
        {
            FlipVertical();
        }
    }

    private void FlipHorizontal()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FlipVertical()
    {
        anim.SetBool("FacingDown", !anim.GetBool("FacingDown"));
    }
}
