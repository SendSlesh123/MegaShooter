using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float jumpStrength = 2;
    public bool isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.15f);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * 100f * jumpStrength);
        }
    }
}
