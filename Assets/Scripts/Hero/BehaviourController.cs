using Obstacles;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace HeroSpace
{
    public class BehaviourController
    {
        public struct Ctx
        {
            public HeroView container;
            public Action runForward;
            public Action jump;
            public Action wait;
            public Action climb;
            public Action walk;
            public Func<float> getDelayTime;
        }

        private Ctx _ctx;
        private BehaviourType _currentBehaviour;
        private IDisposable _prepareForJumpHandler;
        private IDisposable _waitKittyHandler;
        private ObstacleBase _currentObstacle;

        public ObstacleBase CurrentObstacle
        {
            get => _currentObstacle;
            set => _currentObstacle = value;
        }
        public BehaviourType CurrentBehaviour
        {
            get
            {
                return _currentBehaviour;
            }
            set
            {
                if (_currentBehaviour != value)
                    _currentBehaviour = value;
            }
        }

        public BehaviourController(Ctx ctx)
        {
            _ctx = ctx;
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_ctx.container);
            _currentBehaviour = BehaviourType.NormalRun;
        }

        private void Update()
        {
            switch (_currentBehaviour)
            {
                case BehaviourType.NormalRun:
                    _ctx.runForward?.Invoke();
                    break;
                case BehaviourType.PrepareForJump:
                    _prepareForJumpHandler = Observable.Timer(System.TimeSpan.FromSeconds((double)_ctx.getDelayTime?.Invoke()))
                        .Subscribe(_ =>
                        {
                            CurrentBehaviour = BehaviourType.Jump;
                            _prepareForJumpHandler?.Dispose();
                        });
                    CurrentBehaviour = BehaviourType.NormalRun;
                    break;
                case BehaviourType.Jump:
                    _ctx.jump?.Invoke();
                    CurrentBehaviour = BehaviourType.NormalRun;
                    break;
                case BehaviourType.PrepareForClimb:
                    _currentBehaviour = BehaviourType.Jump;
                    break;
                case BehaviourType.Climb:
                    _ctx.climb?.Invoke();
                    break;
                case BehaviourType.Walk:
                    _ctx.walk?.Invoke();
                    break;
                case BehaviourType.Wait:
                    _ctx.wait?.Invoke();
                    if (CheckObstacle())
                        CurrentBehaviour = BehaviourType.NormalRun;
                    break;
            }
        }

        private bool CheckObstacle()
        {
            if (_currentObstacle is LaserView laser)
            {
                return !laser.Active;
            }
            if (_currentObstacle is KittyView)
                return false;
            return true;
        }

        public void ObstacleHandle(ObstacleType type)
        {
            switch (type)
            {
                case ObstacleType.Pit:
                    CurrentBehaviour = BehaviourType.PrepareForJump;
                    break;
                case ObstacleType.Spike:
                    CurrentBehaviour = BehaviourType.Jump;
                    break;
                case ObstacleType.Wall:
                    CurrentBehaviour = BehaviourType.PrepareForClimb;
                    break;
                case ObstacleType.Laser:
                    CurrentBehaviour = BehaviourType.Wait;
                    break;
                case ObstacleType.Kitty:
                    CurrentBehaviour = BehaviourType.Walk;
                    break;
            }
        }

        public void TouchWall()
        {
            if (_currentObstacle is KittyView)
            {
                CurrentBehaviour = BehaviourType.Wait;
                _waitKittyHandler = Observable.Timer(System.TimeSpan.FromSeconds(2)).Subscribe(_ =>
                 {
                     CurrentBehaviour = BehaviourType.NormalRun;
                     _waitKittyHandler?.Dispose();
                 });
            }
            else
            CurrentBehaviour = BehaviourType.Climb;
        }

        public void EndClimb()
        {
            CurrentBehaviour = BehaviourType.NormalRun;
        }
    }

    public enum BehaviourType
    {
        NormalRun,
        PrepareForJump,
        Jump,
        PrepareForClimb,
        Climb,
        Wait,
        Walk,
    }
}
