using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Obstacles;

namespace HeroSpace
{
    public class ObstacleDetector : MonoBehaviour
    {
        public struct Ctx
        {
            public Func<bool> isFreezed;
            public Action<bool> setFreezeState;
        }

        private Ctx _ctx;

        [SerializeField] private float _colliderRadius;

        public float ColliderRadius => _colliderRadius;

        public event Action<ObstacleType> OnObstacleDetected;

        public event Action<ObstacleBase> OnObstacleDetected1;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        private void Awake()
        {
            GetComponent<CircleCollider2D>().radius = _colliderRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_ctx.isFreezed.Invoke()) return;
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
            if (collision.tag == "Ice")
            {
                _ctx.setFreezeState?.Invoke(true);
                OnObstacleDetected1?.Invoke(collision.GetComponent<ObstacleBase>());
                OnObstacleDetected?.Invoke(ObstacleType.Ice);
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
        Ice,
    }
}
