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
        private Dictionary<ObstacleBase, int> _inventory = new Dictionary<ObstacleBase, int>();

        public Inventory(Ctx ctx)
        {
            _ctx = ctx;
            FillInventory();
            foreach (var slot in _ctx.view.Slots)
            {
                slot.OnObstaclePlaced += UseObstacle;
            }
        }

        private void FillInventory()
        {
            foreach (var item in _ctx.obstacles)
            {
                _inventory.Add(item, 3);
            }
            DrawInventory();
        }

        public void DrawInventory()
        {
            _ctx.view.DrawInventory(_inventory);
        }

        private void UseObstacle(ObstacleBase item)
        {
            if (_inventory.ContainsKey(item))
                _inventory[item]--;
            DrawInventory();
        }
    }
}
