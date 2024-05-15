using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController1 : MonoBehaviour
{
    Rigidbody _rigidbody;

    public float walkSpeed = 6;
    public float runSpeed = 9;

    public float currentSpeed;

    public bool canRun;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(canRun && Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        float deltaX = Input.GetAxis("Horizontal") * currentSpeed;
        float deltaZ = Input.GetAxis("Vertical") * currentSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, currentSpeed);
        movement.y = _rigidbody.velocity.y;
        //print(movement);
        _rigidbody.velocity = transform.rotation  * movement;
    }
}
