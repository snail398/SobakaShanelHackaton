using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDetector : MonoBehaviour
{
    public event Action OnTouchWall;

    public event Action OnClimbEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            OnTouchWall?.Invoke();
        }
        if (collision.tag == "Kitty")
        {
            OnTouchWall?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            OnClimbEnd?.Invoke();
        }
    }
}
