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
            trans.sizeDelta = HardCodeSize(item); // custom size

            Image image = imgObject.AddComponent<Image>();
            image.sprite = item.ObstacleSprite;
            image.color = item.ObstacleColor;
            slot.InitSlot(count, item, HardCodeHint(item));
            handler.OnObstaclePlaced += _ => slot.UseSlot();
        }

        private Vector2 HardCodeSize(ObstacleBase item)
        {
            switch (item)
            {
                case WallView _:
                    return new Vector2(30, 150);
                case LaserView _:
                    return new Vector2(30, 150);
                case AnvilView _:
                    return new Vector2(130, 80);
                case IceView _:
                    return new Vector2(150, 150);
                case GhostView _:
                    return new Vector2(150, 150);
                default:
                    return new Vector2(100,100);
            }
        }

        private string HardCodeHint(ObstacleBase item)
        {
            switch (item)
            {
                case WallView _:
                    return "Сможешь перелезть?";
                case LaserView _:
                    return "Жди или беги!";
                case AnvilView _:
                    return "Подними голову!";
                case IceView _:
                    return "Скользко!";
                case GhostView _:
                    return "Обернись...\nБуу!";
                case KittyView _:
                    return "Погладь меня!";
                case PitView _:
                    return "Не упади вниз!";
                case SpikeView _:
                    return "Какие они острые!";

                default:
                    return "Хер моржовый";
            }
        }

        private void ClearInventory()
        {
            foreach (var slot in _slots)
            {
                if (slot.transform.childCount > 2)
                    Destroy(slot.transform.GetChild(2).gameObject);
            }
        }

    }
}
