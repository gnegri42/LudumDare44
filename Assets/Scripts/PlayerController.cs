using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float        speed;

    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // getting Rigidbody
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get keyboard directions and move rigidbody accordingly
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        moveVelocity = movement * speed;
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}
