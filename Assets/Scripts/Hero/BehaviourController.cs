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
            public Action runForward;
            public Action jump;
        }

        private Ctx _ctx;
        private BehaviourType _currentBehaviour;
        private IDisposable _prepareForJumpHandler;

        private BehaviourType CurrentBehaviour
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
            Observable.EveryUpdate().Subscribe(_ => Update());
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
                    _prepareForJumpHandler = Observable.Timer(System.TimeSpan.FromSeconds(1))
                        .Subscribe(_ =>
                        {
                            CurrentBehaviour = BehaviourType.Jump;
                            _prepareForJumpHandler.Dispose();
                        });
                    CurrentBehaviour = BehaviourType.NormalRun;
                    break;
                case BehaviourType.Jump:
                    _ctx.jump?.Invoke();
                    CurrentBehaviour = BehaviourType.NormalRun;
                    break;
            }
        }

        public void ObstacleHandle(ObstacleType type)
        {
            switch (type)
            {
                case ObstacleType.Pit:
                    CurrentBehaviour = BehaviourType.PrepareForJump;
                    break;
            }
        }
    }


    public enum BehaviourType
    {
        NormalRun,
        PrepareForJump,
        Jump,
    }
}
