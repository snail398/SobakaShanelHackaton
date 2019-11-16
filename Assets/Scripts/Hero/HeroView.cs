using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroSpace
{
    public class HeroView : MonoBehaviour
    {
        private HeroMovementController _movementController;
        private Animator _anim;

        public event Action OnTick;

        private void Awake()
        {
            _movementController = GetComponent<HeroMovementController>();
            _anim = GetComponent<Animator>();
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

        public void SetWaitT()
        {
            _anim.SetBool("Idle", true);
        }

        public void SetWaitF()
        {
            _anim.SetBool("Idle", false);
        }
    }
}
