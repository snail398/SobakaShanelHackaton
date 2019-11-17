using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

public class GhostView : ObstacleBase
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Transform hero = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector2(hero.position.x - 2.5f , transform.position.y);
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_speed  * Time.deltaTime, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponentInChildren<HeroDeath>().Die();
        else
            if (collision.tag != "Detector")
                Destroy(gameObject);
    }
}
