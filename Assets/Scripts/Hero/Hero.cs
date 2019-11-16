using System;
using System.Collections;
using System.Collections.Generic;

namespace HeroSpace
{
    public class Hero
    {
        public struct Ctx
        {
            public HeroView mainView;
            public ObstacleDetector detector;
            public NearDetector nearDetector;
        }

        private Ctx _ctx;
        private BehaviourController _behaviourController;


        public Hero(Ctx ctx)
        {
            _ctx = ctx;
            if (_ctx.detector != null)
            {
                _ctx.detector.OnObstacleDetected += PrepareForObstacle;
                _ctx.detector.OnObstacleDetected1 += GetObstacle;
            }
            BehaviourController.Ctx behCtx = new BehaviourController.Ctx
            {
                jump = _ctx.mainView.Jump,
                runForward = _ctx.mainView.RunForward,
                climb = _ctx.mainView.Climb,
                wait = () => { },
                getDelayTime = GetDelayTime,
            };
            _behaviourController = new BehaviourController(behCtx);

            if (_ctx.nearDetector != null)
            {
                _ctx.nearDetector.OnTouchWall += _behaviourController.TouchWall;
                _ctx.nearDetector.OnClimbEnd += _behaviourController.EndClimb;
            }
        }

        private void PrepareForObstacle(ObstacleType type)
        {
            _behaviourController.ObstacleHandle(type);
        }

        private void GetObstacle(ObstacleBase obstacle)
        {
            _behaviourController.CurrentObstacle = obstacle;
        }

        public float GetDelayTime()
        {
            float delay;
            delay = (_ctx.detector.ColliderRadius / _ctx.mainView.GetSpeed()) * 0.15f;
            return delay;
        }
    }
}
