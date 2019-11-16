using InventorySpace;
using Obstacles;
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
            public InventoryView inventoryView;
            public List<ObstacleBase> obstacles;
        }

        private Ctx _ctx;
        private BehaviourController _behaviourController;
        private Inventory _inventory;

        public Hero(Ctx ctx)
        {
            _ctx = ctx;
            CreateBehaviourController();
            CreateInventory();
            if (_ctx.detector != null)
            {
                _ctx.detector.OnObstacleDetected += _behaviourController.ObstacleHandle;
                _ctx.detector.OnObstacleDetected1 += (obstacle) => _behaviourController.CurrentObstacle = obstacle;
            }
            if (_ctx.nearDetector != null)
            {
                _ctx.nearDetector.OnTouchWall += _behaviourController.TouchWall;
                _ctx.nearDetector.OnClimbEnd += _behaviourController.EndClimb;
            }
        }

        private void CreateBehaviourController()
        {
            BehaviourController.Ctx behCtx = new BehaviourController.Ctx
            {
                setwait = _ctx.mainView.SetWaitT,
                setwaitf = _ctx.mainView.SetWaitF,
                container = _ctx.mainView,
                jump = _ctx.mainView.Jump,
                runForward = _ctx.mainView.RunForward,
                climb = _ctx.mainView.Climb,
                wait = () => { },
                walk = _ctx.mainView.Walk,
                getDelayTime = () => (_ctx.detector.ColliderRadius / _ctx.mainView.GetSpeed()) * 0.15f,
            };
            _behaviourController = new BehaviourController(behCtx);
        }

        private void CreateInventory()
        {
            Inventory.Ctx inventoryCtx = new Inventory.Ctx
            {
                obstacles = _ctx.obstacles,
                view = _ctx.inventoryView,
            };
            _inventory = new Inventory(inventoryCtx);
        }


        public float GetDelayTime()
        {
            float delay;
            delay = (_ctx.detector.ColliderRadius / _ctx.mainView.GetSpeed()) * 0.15f;
            return delay;
        }
    }
}
