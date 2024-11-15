using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput.Normalize();
        rb.velocity = moveInput * movementSpeed;
    }
}
