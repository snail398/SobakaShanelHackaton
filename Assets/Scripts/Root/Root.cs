using HeroSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RootSpace
{
    public class Root : MonoBehaviour
    {

        [SerializeField] private HeroView _heroPrefab;
        [SerializeField] private CameraController _camera;

        [Header("Start Settings")]
        [SerializeField] private Vector2 _startPos;

        private Hero _hero;

        public event Action OnInitializeReady;

        private void Awake()
        {
            OnInitializeReady += StartRun;
            Initialize();
        }

        private void Initialize()
        {
            //Create Hero
            HeroView view = Instantiate(_heroPrefab, _startPos, Quaternion.identity);
            Hero.Ctx heroCtx = new Hero.Ctx
            {
                mainView = view,
                detector = view.GetComponentInChildren<ObstacleDetector>(),
                nearDetector = view.GetComponentInChildren<NearDetector>(),
            };
            _hero = new Hero(heroCtx);
            _camera.Hero = view.transform;
            OnInitializeReady?.Invoke();
        }

        private void StartRun()
        {

        }
    }
}
