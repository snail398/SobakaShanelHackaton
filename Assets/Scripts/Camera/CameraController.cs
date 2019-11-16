using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _hero;

    public Transform Hero
    {
        get
        {
            return _hero;
        }
        set
        {
            _hero = value;
        }
    }

    private void Update()
    {
        if (_hero != null)
        {
            transform.position = new Vector3(_hero.position.x, _hero.position.y, transform.position.z);
        }
    }
}
