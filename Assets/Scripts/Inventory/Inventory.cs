using Obstacles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
            //DevVersion
            foreach (var item in _ctx.obstacles)
            {
                _inventory.Add(item, 10);
            }
            //ProdVersion
            /*
            List<int> added = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                int temp = Random.Range(0, _ctx.obstacles.Count);
                while (added.Contains(temp))
                {
                    temp = Random.Range(0, _ctx.obstacles.Count);
                }
                added.Add(temp);
                _inventory.Add(_ctx.obstacles[temp], 3);
            }
            */
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
