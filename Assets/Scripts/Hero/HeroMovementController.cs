using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }
    
    public void RunForward()
    {
        _transform.position += Vector3.right * _speed   * Time.deltaTime;
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
