using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpForceModifire;

    [SerializeField] private bool _isGrounded;

    private Rigidbody _rigidbody;
    private float _jumpForceTMP;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out JumpAmplifier jumpAmplifier))
        {
            _jumpForceTMP = _jumpForce;
            _jumpForce = _jumpForce + _jumpForceModifire * jumpAmplifier.GetJumpModifire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _jumpForce = _jumpForceTMP;
    }
}
