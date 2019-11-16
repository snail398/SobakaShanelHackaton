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
    public class Slot : MonoBehaviour//,IEndDragHandler
    {
        [SerializeField] private Text _count;

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

        public void UseSlot()
        {
            OnObstaclePlaced?.Invoke(_content);
        }

        public void InitSlot(int count,ObstacleBase content)
        {
            _content = content;
            _count.text = count.ToString(); 
        }

        #region IDropHandler implementation
     //   public void OnDrop(PointerEventData eventData)
     //   {
      //      OnObstaclePlaced?.Invoke(_content);
     //   }
        #endregion
    }
}
