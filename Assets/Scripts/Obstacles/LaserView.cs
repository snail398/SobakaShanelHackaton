﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Obstacles
{
    public class LaserView : ObstacleBase
    {
        [SerializeField] private float _period = 3;

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
                collision.GetComponent<HeroDeath>().Die();
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
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1);
        }

        private void SetInActive()
        {
            _active = false;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0.2f);
        }
    }
}
