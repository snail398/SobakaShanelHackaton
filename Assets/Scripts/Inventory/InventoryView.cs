using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySpace
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private List<Slot> _slots;

        public List<Slot> Slots => _slots;

        public void DrawInventory(Dictionary<ObstacleBase, int> inventory)
        {
            ClearInventory();
            Vector2 pos = Vector2.zero;
            int i = 0;
            foreach (var item in inventory)
            {
                if (item.Value > 0)
                    DrawItem(item.Key, item.Value, pos, _slots[i++]);
            }
        }

        private void DrawItem(ObstacleBase item, int count, Vector2 pos, Slot slot)
        {
            GameObject imgObject = new GameObject(item.name);
            DragHandler handler = imgObject.AddComponent<DragHandler>();
            handler.SetPrefab(item);
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.transform.SetParent(slot.transform); // setting parent
            trans.localScale = Vector3.one;
            trans.anchoredPosition = pos; // setting position, will be on center
            //trans.sizeDelta = new Vector2(50, 50); // custom size

            Image image = imgObject.AddComponent<Image>();
            image.sprite = item.ObstacleSprite;
            image.color = item.ObstacleColor;
            slot.InitSlot(count, item);
            handler.OnObstaclePlaced += _ => slot.UseSlot();
        }

        private void ClearInventory()
        {
            foreach (var slot in _slots)
            {
                if (slot.transform.childCount > 1)
                    Destroy(slot.transform.GetChild(1).gameObject);
            }
        }

    }
}
