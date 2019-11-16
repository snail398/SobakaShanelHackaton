using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Obstacles;

namespace HeroSpace
{
    public class ObstacleDetector : MonoBehaviour
    {
        [SerializeField] private float _colliderRadius;

        public float ColliderRadius => _colliderRadius;

        public event Action<ObstacleType> OnObstacleDetected;

        public event Action<ObstacleBase> OnObstacleDetected1;

        private void Awake()
        {
            GetComponent<CircleCollider2D>().radius = _colliderRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Pit")
            {
                Destroy(collision.gameObject);
                OnObstacleDetected?.Invoke(ObstacleType.Pit);
            }
            if (collision.tag == "Wall")
            {
                OnObstacleDetected?.Invoke(ObstacleType.Wall);
            }
            if (collision.tag == "Spike")
            {
                OnObstacleDetected?.Invoke(ObstacleType.Spike);
            }
            if (collision.tag == "Kitty")
            {
                OnObstacleDetected1?.Invoke(collision.GetComponent<ObstacleBase>());
                OnObstacleDetected?.Invoke(ObstacleType.Kitty);
            }
            if (collision.tag == "Laser")
            {
                OnObstacleDetected1?.Invoke(collision.GetComponent<ObstacleBase>());
                OnObstacleDetected?.Invoke(ObstacleType.Laser);
            }
        }
    }

    public enum ObstacleType
    {
        Pit,
        Wall,
        Laser,
        Spike,
        Kitty,

    }
}
