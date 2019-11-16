using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _castPoint;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Animator _anim;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
    }

    public void RunForward()
    {
        SetFullGravity();
        _transform.position += Vector3.right * _speed   * Time.deltaTime;
        _anim.SetBool("Climb", false);
    }

    private bool CheckGroundedState()
    {
        if (Physics2D.Raycast(_castPoint.position, -transform.up, 0.35f, ~LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Walk()
    {
        SetFullGravity();
            _transform.position += Vector3.right * _speed * 0.5f * Time.deltaTime;
        _anim.SetBool("Climb", false);
    }

    public void Jump()
    {
        SetFullGravity();
        if (CheckGroundedState())
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        _anim.SetBool("Climb", false);
    }

    public void Climb()
    {
        SetZeroGravity();
        _transform.position += Vector3.up * _climbSpeed * Time.deltaTime;
        _anim.SetBool("Climb", true);
    }

    private void SetZeroGravity()
    {
        if (_rigidbody2D.gravityScale != 0)
            _rigidbody2D.gravityScale = 0;
    }

    private void SetFullGravity()
    {
        if (_rigidbody2D.gravityScale != 1)
            _rigidbody2D.gravityScale = 1;
    }
}
