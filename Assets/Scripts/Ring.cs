using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int speed;
    public GameObject ballPrefab;
    public Rigidbody2D rb;
    
    private float horizontal;

    void Update()
    {
        ScreenThreshold();
        HorizontalMovement();
    }

    void ScreenThreshold()
    {
        if(transform.position.x > 8f)
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }

        if(transform.position.x < -8f)
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
    }

    void HorizontalMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed; // returns 1 if we are pressing D or -1 if we are pressing A

        if (horizontal < 0.0f) // if we are moving left
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f) // if we are moving right
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, 0); // update the position
    }
}
