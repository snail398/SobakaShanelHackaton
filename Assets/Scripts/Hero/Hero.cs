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
        }

        private Ctx _ctx;
        private BehaviourController _behaviourController;


        public Hero(Ctx ctx)
        {
            _ctx = ctx;
            if (_ctx.detector != null)
            {
                _ctx.detector.OnOnstacleDetected += PrepareForObstacle;
            }
            BehaviourController.Ctx behCtx = new BehaviourController.Ctx
            {
                jump = _ctx.mainView.Jump,
                runForward = _ctx.mainView.RunForward,
            };
            _behaviourController = new BehaviourController(behCtx);
        }

        private void PrepareForObstacle(ObstacleType type)
        {
            _behaviourController.ObstacleHandle(type);
        }
    }
}
