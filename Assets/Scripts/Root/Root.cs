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
            };
            _hero = new Hero(heroCtx);
            OnInitializeReady?.Invoke();
        }

        private void StartRun()
        {

        }
    }
}
