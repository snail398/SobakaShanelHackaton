using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HeroSpace
{
    public class ObstacleDetector : MonoBehaviour
    {
        public event Action<ObstacleType> OnOnstacleDetected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Pit")
            {
                Destroy(collision.gameObject);
                OnOnstacleDetected?.Invoke(ObstacleType.Pit);
            }
        }
    }

    public enum ObstacleType
    {
        Pit,
    }
}
