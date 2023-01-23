using System;
using Controllers;
using Installers;
using Interfaces;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class GameManager : MonoBehaviour, IBindable
    {
        private const string PlayerPath = "Prefabs/Player";

        [SerializeField] 
        private Button _startButton;
        [SerializeField] 
        private MoveForwardController _moveForwardContainer;
        
        private PlayerMoveController _playerMoveController;
        private SaveManager _saveManager;
        private EndScreenController _endScreenController;
        private ProgressManager _progressManager;

        public void Initialize(ScriptBinder container)
        {
            _saveManager = container.Get<SaveManager>();
            _progressManager = container.Get<ProgressManager>();
            
            _endScreenController = container.Get<EndScreenController>();
            _endScreenController.CloseEvent += Reset;

            var collisionController = container.Get<CollisionController>();
            collisionController.PlayerBarrierCollision.Subscribe(EndSession).AddTo(this);
        }

        private void Awake()
        {
            if (_startButton == null
                || _moveForwardContainer == null)
            {
                throw new NullReferenceException();
            }
            
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            var playerGo = Resources.Load<GameObject>(PlayerPath);

            if (playerGo == null)
            {
                throw new NullReferenceException($"Player prefab not found in {PlayerPath}");
            }

            Instantiate(playerGo, _moveForwardContainer.transform);

            _playerMoveController = playerGo.GetComponent<PlayerMoveController>();

            if (_playerMoveController == null)
            {
                throw new NullReferenceException();
            }
                
            Reset();
        }

        private void Reset()
        {
            _moveForwardContainer.Reset();
            _playerMoveController.ResetSession();
            _progressManager.StartSession();
        }

        private void EndSession(bool value)
        {
            if (!value)
            {
                return;
            }
            
            _saveManager.IncrementSessionCount();
            
            _endScreenController.Show();
        }

        private void OnDestroy()
        {
            _endScreenController.CloseEvent -= Reset;
        }
    }
}