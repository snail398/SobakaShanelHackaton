using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleBase : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _sprite;
        [SerializeField] protected float _ySpawnPos;
        public Sprite ObstacleSprite => _sprite.sprite;
        public Color ObstacleColor => _sprite.color;
        public float YSpawnPos => _ySpawnPos;
    }
}
