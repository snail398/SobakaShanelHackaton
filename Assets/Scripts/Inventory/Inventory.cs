using Obstacles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySpace
{
    public class Inventory 
    {
        public struct Ctx
        {
            public InventoryView view;
            public List<ObstacleBase> obstacles;
        }

        private Ctx _ctx;
        private List<ObstacleBase> _obstacleList = new List<ObstacleBase>();

        public Inventory(Ctx ctx)
        {
            _ctx = ctx;
            FillInventory();
        }

        private void FillInventory()
        {
            foreach (var item in _ctx.obstacles)
            {
                _obstacleList.Add(item);
            }
            DrawInventory();
        }

        public void DrawInventory()
        {
            _ctx.view.DrawInventory(_obstacleList);
        }
    }
}
