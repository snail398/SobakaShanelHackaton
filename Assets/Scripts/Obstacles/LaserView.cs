using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Obstacles
{
    public class LaserView : ObstacleBase
    {
        [SerializeField] private float _period = 3;
        [SerializeField] private SpriteRenderer _ray;
        private bool _active;

        public bool Active => _active;

        private void Awake()
        {
            SetActive();
            Observable.Timer(System.TimeSpan.FromSeconds(_period)).Repeat().Subscribe(_ => ChangeState()).AddTo(this);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player" && Active)
            {
                collision.GetComponentInChildren<HeroDeath>().Die();
            }
        }

        private void ChangeState()
        {
            if (_active)
                SetInActive();
            else
                SetActive();
        }

        private void SetActive()
        {
            _active = true;
            _ray.color = new Color(_ray.color.r, _ray.color.g, _ray.color.b, 1);
        }

        private void SetInActive()
        {
            _active = false;
            _ray.color = new Color(_ray.color.r, _ray.color.g, _ray.color.b, 0.2f);
        }
    }
}
