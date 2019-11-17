using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform _hero;
    private float _startY;
    public Transform Hero
    {
        get
        {
            return _hero;
        }
        set
        {
            _hero = value;
            _startY = _hero.position.y;
        }
    }

    private void Update()
    {
        if (_hero != null)
        {
            transform.position = new Vector3(_hero.position.x, _startY, transform.position.z) + offset;
        }
    }

    public void RestartScene()
    {
        StartCoroutine(RestScene());
    }

    IEnumerator RestScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
