using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Obstacles;

namespace InventorySpace
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text _count;
        [SerializeField] private Text _hintText;
        [SerializeField] private Transform _hintPanel;

        private ObstacleBase _content;

        public event Action<ObstacleBase> OnObstaclePlaced;
        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }
                return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowHint();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideHint();
        }

        public void ShowHint()
        {
            _hintPanel.gameObject.SetActive(true);
        }

        public void HideHint()
        {
            _hintPanel.gameObject.SetActive(false);
        }

        public void UseSlot()
        {
            OnObstaclePlaced?.Invoke(_content);
        }

        public void InitSlot(int count,ObstacleBase content, string initText)
        {
            _content = content;
            _count.text = count.ToString();
            _hintText.text = initText;
        }
    }
}
