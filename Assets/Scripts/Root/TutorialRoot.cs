        using HeroSpace;
using Obstacles;
using InventorySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RootSpace
{
    public class TutorialRoot : MonoBehaviour
    {

        [SerializeField] private HeroView _heroPrefab;
        [SerializeField] private CameraController _camera;
        [SerializeField] private InventoryView _inventoryView;

        [Header("Start Settings")]
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private List<ObstacleBase> _obstacles;

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
                inventoryView = _inventoryView,
                obstacles = _obstacles,
            };
            _hero = new Hero(heroCtx);
            _camera.Hero = view.transform;
            OnInitializeReady?.Invoke();
        }

        private void StartRun()
        {
            //_hero.BehaviourController.CurrentBehaviour = BehaviourType.Wait;
        }
        void Update()
        {
          // if (Input.anyKey && _hero.BehaviourController.CurrentBehaviour == BehaviourType.Wait)
        //       _hero.BehaviourController.CurrentBehaviour = BehaviourType.NormalRun;
        }

    }
}
