using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ObstacleBase _prefab;
        Vector3 startPosition;
        Transform startParent;

        public void SetPrefab(ObstacleBase obstacle)
        {
            _prefab = obstacle;
        }

        #region IBeginDragHandler implementation

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            startParent = transform.parent;
        }

        #endregion

        #region IDragHandler implementation

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        #endregion

        #region IEndDragHandler implementation

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector3 her = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector3 instPos = new Vector3(her.x, her.y, 0);
            Instantiate(_prefab, instPos, Quaternion.identity);
            if (transform.parent == startParent)
            {
                transform.position = startPosition;
            }
        }

        #endregion
    }
}
