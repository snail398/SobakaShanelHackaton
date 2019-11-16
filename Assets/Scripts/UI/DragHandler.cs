using Obstacles;
using System;
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
        public event Action<ObstacleBase> OnObstaclePlaced;
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
            OnObstaclePlaced?.Invoke(_prefab);
            Vector3 her = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector3 instPos = new Vector3(her.x, _prefab.YSpawnPos, 0);
            if (_prefab is PitView)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, 100, ~LayerMask.NameToLayer("Ground"));
                if (hit)
                {
                    Destroy(hit.collider.gameObject);
                    instPos = hit.collider.transform.position;
                }
            }
            Instantiate(_prefab, instPos, Quaternion.identity);
            if (transform.parent == startParent)
            {
                transform.position = startPosition;
            }
        }

        #endregion
    }
}
