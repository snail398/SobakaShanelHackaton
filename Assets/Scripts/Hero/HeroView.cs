using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroSpace
{
    public class HeroView : MonoBehaviour
    {
        private HeroMovementController _movementController;

        public event Action OnTick;

        private void Awake()
        {
            _movementController = GetComponent<HeroMovementController>();
        }

        private void Update()
        {
            OnTick?.Invoke();
        }

        public void Jump()
        {
            _movementController.Jump();
        }

        public void RunForward()
        {
            _movementController.RunForward();
        }

        public float GetSpeed()
        {
            return _movementController.Speed;
        }

        public void Climb()
        {
            _movementController.Climb();
        }

        public void Walk()
        {
            _movementController.Walk();
        }
    }
}
