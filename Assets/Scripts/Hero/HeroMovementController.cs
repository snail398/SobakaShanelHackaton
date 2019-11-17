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
    [SerializeField] private HeroDeath _heroDeath;

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
        if (!_heroDeath.IsDead)
        {
            SetFullGravity();
            //_transform.position += Vector3.right * _speed * Time.deltaTime;
            _rigidbody2D.velocity = new Vector2(_speed * 1.0f * Time.deltaTime, _rigidbody2D.velocity.y);
            _anim.SetTrigger("Run");
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Slide()
    {
        if (!_heroDeath.IsDead)
        {
            SetFullGravity();
            //_transform.position += Vector3.right * _speed * 1.5f * Time.deltaTime;
            _rigidbody2D.velocity = new Vector2(_speed * 1.5f * Time.deltaTime, _rigidbody2D.velocity.y);
            _anim.SetTrigger("Glide");
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Walk()
    {
        if (!_heroDeath.IsDead)
        {
            SetFullGravity();
            //_transform.position += Vector3.right * _speed * 0.5f * Time.deltaTime;
            _rigidbody2D.velocity = new Vector2(_speed * 0.5f * Time.deltaTime, _rigidbody2D.velocity.y);
            _anim.SetTrigger("Run");
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        if (!_heroDeath.IsDead)
        {
            SetFullGravity();
            if (CheckGroundedState())
                _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _anim.SetBool("Climb", false);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
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

    public void Climb()
    {
        if (!_heroDeath.IsDead)
        {
            SetZeroGravity();
            //_transform.position += Vector3.up * _climbSpeed * Time.deltaTime;
            _rigidbody2D.velocity = new Vector2(0, _climbSpeed * Time.deltaTime);
            _anim.SetTrigger("Climb");
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Wait()
    {
        if (!_heroDeath.IsDead)
        {
            SetFullGravity();
            //_transform.position += Vector3.up * _climbSpeed * Time.deltaTime;
            _rigidbody2D.velocity = Vector2.zero;
            _anim.SetTrigger("Idle");
        }
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
