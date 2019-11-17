using InventorySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    private Slot _currentSlot;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 100, ~LayerMask.NameToLayer("Slot"));
        if (!hit) return;
        var slot = hit.collider.GetComponent<Slot>();
        if (slot)
        {
            _currentSlot = slot;
            _currentSlot.ShowHint();
        }
        else
        {
            if (_currentSlot != null)
            {
                _currentSlot.HideHint();
                _currentSlot = null;
            }
        }
    }
}
