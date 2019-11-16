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

        public void DrawInventory(List<ObstacleBase> inventory)
        {
            Vector2 pos = Vector2.zero;
            int i = 0;
            foreach (var item in inventory)
            {
                DrawItem(item, pos, _slots[i++]);
            }
        }

        private void DrawItem(ObstacleBase item, Vector2 pos, Slot slot)
        {
            GameObject imgObject = new GameObject(item.name);
            imgObject.AddComponent<DragHandler>().SetPrefab(item);
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.transform.SetParent(slot.transform); // setting parent
            trans.localScale = Vector3.one;
            trans.anchoredPosition = pos; // setting position, will be on center
            trans.sizeDelta = new Vector2(100, 100); // custom size

            Image image = imgObject.AddComponent<Image>();
            image.sprite = item.ObstacleSprite;
            image.color = item.ObstacleColor;
        }

    }
}
